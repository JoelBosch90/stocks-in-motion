using DataAccessLayer.Models;
using System.Text.Json;

namespace DataAccessLayer.Tools.DataSources
{
    public class FinancialModelingPrep
    {
        public class HistoricalPriceFull
        {
            public string symbol { get; set; }
            public List<HistoricalPrice> historical { get; set; }
        }

        public class HistoricalPrice
        {
            public string date { get; set; }
            public decimal open { get; set; }
            public decimal high { get; set; }
            public decimal low { get; set; }
            public decimal close { get; set; }
            public decimal adjClose { get; set; }
            public decimal volume { get; set; }
            public decimal unadjustedVolume { get; set; }
            public decimal change { get; set; }
            public decimal changePercent { get; set; }
            public decimal vwap { get; set; }
            public string label { get; set; }
            public decimal changeOverTime { get; set; }
        }

        public static List<StockPrice> HistoricalPriceFullToStockPriceList(string data, Stock stock)
        {
            HistoricalPriceFull? historicalPriceFull = JsonSerializer.Deserialize<HistoricalPriceFull>(data);

            if (historicalPriceFull?.historical == null) return new List<StockPrice>();

            return historicalPriceFull.historical.ConvertAll(historicalPrice => HistoricalPriceToStockPrice(historicalPrice, stock));
        }

        public static StockPrice HistoricalPriceToStockPrice(HistoricalPrice historicalPrice, Stock stock)
        {
            return new StockPrice{
                Added = DateTime.UtcNow,

                // Right now, we're only storing information per day. We may eventually want more moments.
                Moment = new(DateTime.Parse(historicalPrice.date).Ticks, DateTimeKind.Utc),

                // We also want to start storing currencies in the database at some point.
                Currency = "USD",
                Open = DollarsToCents(historicalPrice.open),
                High = DollarsToCents(historicalPrice.high),
                Low = DollarsToCents(historicalPrice.low),
                Close = DollarsToCents(historicalPrice.close),
                Volume = (Int64)JsonSerializer.Deserialize<decimal>(historicalPrice.unadjustedVolume),
                StockId = stock.Id,

                // @TODO: get this from database when we have more than 1 source.
                DataSourceId = 11, 
            };
        }

        protected static Int32 DollarsToCents(decimal dollars)
        {
            return (Int32)dollars * 100;
        }
    }
}
