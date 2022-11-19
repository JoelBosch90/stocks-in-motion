using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private static readonly string[] Some_Stocks = new[]
        {
            "AAPL", "MSFT", "AMZN", "GOOGL", "GOOG", "TSLA", "BRK.B", "JNJ", "UNH", "FD", "NVDA", "XOM", "PG", "JPM", "V", "CVX", "HD", "PFE", "MA", "ABBV", "KO", "BAC", "AVGO", "PEP", "LLY"
        };

        private readonly ILogger<StocksController> _logger;

        public StocksController(ILogger<StocksController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Stocks")]
        public IEnumerable<string> Get()
        {
            return Some_Stocks;
        }

        [HttpGet("{symbol}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StockPrice))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockPrice>> Get(string symbol)
        {
            if (Array.IndexOf(Some_Stocks, symbol) < 0) return NotFound();

            using StocksContext context = new();

            Stock? stock = context.Stocks.SingleOrDefault(stock => stock.Symbol == symbol);
            stock ??= StoreStock(new Stock { Symbol = symbol });

            StockPrice? stockPrice = context.StockPrices.SingleOrDefault(stockPrice => stockPrice.StockId == stock.Id);

            if (stockPrice == null)
            {
                stockPrice = await FetchStockPrice(stock);

                if (stockPrice == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

                StoreStockPrice(stockPrice);
            }
            return Ok(stockPrice);
        }

        protected async Task<StockPrice?> FetchStockPrice(Stock stock)
        {
            using StocksContext context = new();
            using HttpClient httpClient = new();

            // This is a temporary solution. Ideally, this information should ideally come from the database,
            // but before we start storing it there, we should make sure that we can properly encrypt the keys.
            string? apiKey = Environment.GetEnvironmentVariable("DATASOURCE_KEY");
            string? apiUrl = Environment.GetEnvironmentVariable("DATASOURCE_URL");
            if (apiKey == null || apiUrl == null) return null;

            using HttpRequestMessage request = new (new HttpMethod("GET"), $"{apiUrl}/{stock.Symbol}?apikey={apiKey}");
            request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            string responseBody = await response.Content.ReadAsStringAsync();
            JsonNode? node = JsonNode.Parse(responseBody);
            if (node is null) return null;

            JsonNode? stock1 = node["historical"][0];
            DateTime added = new(DateTime.Parse((string)stock1["date"]).Ticks, DateTimeKind.Utc);

            string currency = "USD";
            Int32 open = JsonDollarsToCents(stock1["open"]);
            Int32 high = JsonDollarsToCents(stock1["high"]);
            Int32 low = JsonDollarsToCents(stock1["low"]);
            Int32 close = JsonDollarsToCents(stock1["close"]);
            Int64 volume = (Int64)JsonSerializer.Deserialize<decimal>(stock1["unadjustedVolume"]);

            return new StockPrice
            {
                Added = added,
                Currency = "USD",
                Open = open,
                High = high,
                Low = low,
                Close = close,
                Volume = volume,
                StockId = stock.Id,
                DataSourceId = 11, // @TODO: get this from database when we have more than 1 source.
            };
        }

        protected Int32 JsonDollarsToCents(JsonNode dollars)
        {
            return (Int32)JsonSerializer.Deserialize<decimal>(dollars) * 100;
        }

        protected Stock StoreStock(Stock stock)
        {
            using StocksContext context = new();
            context.Stocks.Add(stock);
            context.SaveChanges();

            return stock;
        }

        protected StockPrice StoreStockPrice(StockPrice stockPrice)
        {
            using StocksContext context = new();
            context.StockPrices.Add(stockPrice);
            context.SaveChanges();

            return stockPrice;
        }
    }
}