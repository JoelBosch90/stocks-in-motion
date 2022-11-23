using DataAccessLayer.Models;
using static DataAccessLayer.Tools.DataSources.FinancialModelingPrep;

namespace DataAccessLayer.Tools
{
    public class StockFetcher
    {
        public static readonly string[] limitedList = new[]
        {
            "AAPL", "MSFT", "AMZN", "GOOGL", "GOOG", "TSLA", "BRK.B", "JNJ", "UNH", "FD", "NVDA", "XOM", "PG", "JPM", "V", "CVX", "HD", "PFE", "MA", "ABBV", "KO", "BAC", "AVGO", "PEP", "LLY"
        };

        public static Stock? Fetch(string symbol)
        {
            if (Array.IndexOf(limitedList, symbol) < 0) return null;

            return FetchFromDatabase(symbol);
        }

        protected static Stock FetchFromDatabase(string symbol)
        {
            using StocksContext context = new();
            Stock? stock = context.Stocks.SingleOrDefault(stock => stock.Symbol == symbol);
            DateTime added = new(DateTime.Now.Ticks, DateTimeKind.Utc);
            stock ??= StoreStock(new Stock { Symbol = symbol, Added = added });

            return stock;
        }

        protected static Stock StoreStock(Stock stock)
        {
            using StocksContext context = new();
            context.Stocks.Add(stock);
            context.SaveChanges();

            return stock;
        }
    }
}
