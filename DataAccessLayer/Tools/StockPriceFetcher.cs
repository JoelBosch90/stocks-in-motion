using DataAccessLayer.Models;
using DataAccessLayer.Tools.DataSources;

namespace DataAccessLayer.Tools
{
    public class StockPriceFetcher
    {
        private readonly DataSource source;

        public StockPriceFetcher()
        {
            using StocksContext context = new();

            // @TODO: Temporarily hardcoded until we have more than 1 data source.
            long dataSourceId = 11;
            DataSource? dataSource = context.DataSources.SingleOrDefault(source => source.Id == dataSourceId);
            source = dataSource ?? new DataSource();
        }

        public async Task<List<StockPrice>> Fetch(Stock stock, DateTime first, DateTime last)
        {
            StockPrice? newestStockPrice = FetchLastFromDatabase(stock);

            if (newestStockPrice == null || newestStockPrice.Moment.Date < last.Date)
            {
                if (!RecentlyFetched(stock)) await RequestStockPriceData(stock, newestStockPrice?.Moment.AddDays(1));
            }

            return FetchFromDatabase(stock, first, last);
        }

        protected bool RecentlyFetched(Stock stock)
        {
            using StocksContext context = new();

            DataRequest? lastRequest = context.DataRequests
                .Where(request => request.DataSourceId == source.Id && request.StockId == stock.Id)
                .OrderByDescending(request => request.Added)
                .FirstOrDefault();

            if (lastRequest == null) return false;

            // For now, we want to simply check only once a day.
            return lastRequest.Added.Date == DateTime.UtcNow.Date;
        }

        protected static StockPrice? FetchLastFromDatabase(Stock stock)
        {
            using StocksContext context = new();

            List<StockPrice> stockPrices = context.StockPrices.Where(stockPrice => stockPrice.StockId == stock.Id)
                .OrderByDescending(stockPrice => stockPrice.Moment).ToList();

            return stockPrices.Count > 0 ? stockPrices.First() : null;
        }

        protected static List<StockPrice> FetchFromDatabase(Stock stock, DateTime? first, DateTime? last)
        {
            using StocksContext context = new();

            return context.StockPrices.Where(stockPrice =>
                stockPrice.StockId == stock.Id &&
                (first == null || stockPrice.Moment >= first) &&
                (last == null || stockPrice.Moment <= last)
            ).OrderByDescending(stockPrice => stockPrice.Moment).ToList();
        }

        protected async Task RequestStockPriceData(Stock stock, DateTime? first)
        {
            // This is a temporary solution. Ideally, this information should ideally come from the database,
            // but before we start storing it there, we should make sure that we can properly encrypt the keys.
            string? apiKey = Environment.GetEnvironmentVariable("DATASOURCE_KEY");
            string? apiUrl = Environment.GetEnvironmentVariable("DATASOURCE_URL");
            if (apiKey == null || apiUrl == null) return;

            string requestString = $"{apiUrl}/{stock.Symbol}?apikey={apiKey}";
            if (first != null) requestString += $"&from={first:yyyy-MM-dd}";

            using HttpClient httpClient = new();
            using HttpRequestMessage request = new(new HttpMethod("GET"), requestString);
            request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            HttpResponseMessage response = await httpClient.SendAsync(request);

            string safeRequestString = requestString.Replace(apiKey, "DATASOURCE_KEY");
            DataRequest dataRequest = StoreDataRequest(stock, safeRequestString, (int) response.StatusCode);
            if (!response.IsSuccessStatusCode) return;

            string dataString = await response.Content.ReadAsStringAsync();
            StoreStockPrices(FinancialModelingPrep.HistoricalPriceFullToStockPriceList(dataString, stock, dataRequest.DataSourceId));
        }

        protected DataRequest StoreDataRequest(Stock stock, string requestString, int responseCode)
        {
            using StocksContext context = new();
            DataRequest dataRequest = new DataRequest {
                Added = DateTime.UtcNow,
                RequestString = requestString,
                ResponseCode = responseCode,
                DataSourceId = source.Id,
                StockId = stock.Id,
            };
            context.DataRequests.Add(dataRequest);
            context.SaveChanges();

            return dataRequest;
        }

        protected static void StoreStockPrices(List<StockPrice> stockPrices)
        {
            using StocksContext context = new();
            List<StockPrice> orderedByLatestLast = stockPrices.OrderByDescending(stockPrice => stockPrice.Moment).ToList();
            context.StockPrices.AddRange(orderedByLatestLast);
            context.SaveChanges();
        }
    }
}
