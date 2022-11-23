using DataAccessLayer.Models;
using DataAccessLayer.Tools.DataSources;

namespace DataAccessLayer.Tools
{
    public class StockPriceFetcher
    {
        public static async Task<List<StockPrice>> Fetch(Stock stock)
        {
            List<StockPrice> stockPrices = FetchFromDatabase(stock);
            if (stockPrices.Count > 0) return stockPrices;

            string? data = await RequestStockPriceData(stock);
            if (data == null) return new List<StockPrice>();

            stockPrices = FinancialModelingPrep.HistoricalPriceFullToStockPriceList(data, stock);
            if (stockPrices == null) return new List<StockPrice>();

            StoreStockPrices(stockPrices);
            return stockPrices;
        }

        protected static List<StockPrice> FetchFromDatabase(Stock stock)
        {
            using StocksContext context = new();

            return context.StockPrices.Where(stockPrice => stockPrice.StockId == stock.Id).ToList();
        }

        protected static async Task<string?> RequestStockPriceData(Stock stock)
        {
            using HttpClient httpClient = new();

            // This is a temporary solution. Ideally, this information should ideally come from the database,
            // but before we start storing it there, we should make sure that we can properly encrypt the keys.
            string? apiKey = Environment.GetEnvironmentVariable("DATASOURCE_KEY");
            string? apiUrl = Environment.GetEnvironmentVariable("DATASOURCE_URL");
            if (apiKey == null || apiUrl == null) return null;

            using HttpRequestMessage request = new(new HttpMethod("GET"), $"{apiUrl}/{stock.Symbol}?apikey={apiKey}");
            request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync();
        }

        protected static List<StockPrice> StoreStockPrices(List<StockPrice> stockPrices)
        {
            using StocksContext context = new();
            context.StockPrices.AddRange(stockPrices);
            context.SaveChanges();

            return stockPrices;
        }
    }
}
