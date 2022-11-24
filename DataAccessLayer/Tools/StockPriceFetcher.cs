using DataAccessLayer.Models;
using DataAccessLayer.Tools.DataSources;

namespace DataAccessLayer.Tools
{
    public class StockPriceFetcher
    {
        public static async Task<List<StockPrice>> Fetch(Stock stock, DateTime first, DateTime last)
        {
            List<StockPrice> stockPrices = FetchFromDatabase(stock, first, last);

            // @TODO: Currently, we fetch data if we're not up to date. Sadly, this means that we fetch data on every
            // request on weekends and bank holidays. We should come up with a solution for this.
            DateTime? lastStockPriceDate = stockPrices.Count > 1 ? stockPrices.Max(stockPrice => stockPrice.Moment).Date : null;
            if (lastStockPriceDate == DateTime.Today) return stockPrices;

            string? data = await RequestStockPriceData(stock, lastStockPriceDate?.AddDays(1));
            if (data != null) StoreStockPrices(FinancialModelingPrep.HistoricalPriceFullToStockPriceList(data, stock));

            stockPrices = FetchFromDatabase(stock, first, last);
            if (stockPrices.Count > 1) stockPrices = stockPrices.OrderBy(stockPrice => stockPrice.Moment).ToList();

            return stockPrices;
        }

        protected static List<StockPrice> FetchFromDatabase(Stock stock, DateTime first, DateTime last)
        {
            using StocksContext context = new();

            return context.StockPrices.Where(stockPrice => stockPrice.StockId == stock.Id && stockPrice.Moment >= first && stockPrice.Moment <= last).ToList();
        }

        protected static async Task<string?> RequestStockPriceData(Stock stock, DateTime? first)
        {
            using HttpClient httpClient = new();

            // This is a temporary solution. Ideally, this information should ideally come from the database,
            // but before we start storing it there, we should make sure that we can properly encrypt the keys.
            string? apiKey = Environment.GetEnvironmentVariable("DATASOURCE_KEY");
            string? apiUrl = Environment.GetEnvironmentVariable("DATASOURCE_URL");
            if (apiKey == null || apiUrl == null) return null;

            string requestString = $"{apiUrl}/{stock.Symbol}?apikey={apiKey}";
            if (first != null) requestString += $"&from={first:yyyy-MM-dd}";
            using HttpRequestMessage request = new(new HttpMethod("GET"), requestString);

            request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync();
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
