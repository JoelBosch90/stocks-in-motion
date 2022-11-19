using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Stock))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Stock>> Get(string symbol)
        {
            if (Array.IndexOf(Some_Stocks, symbol) < 0) return NotFound();

            using StocksContext context = new();

            Stock? stock = context.Stocks.SingleOrDefault(stock => stock.Symbol == symbol);

            if(stock == null)
            {
                stock = await FetchStock(symbol);

                if (stock == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

                StoreStock(stock);
            }
            return Ok(stock);
        }

        protected async Task<Stock?> FetchStock(string symbol)
        {
            // This is a temporary solution. Ideally, this information should ideally come from the database,
            // but before we start storing it there, we should make sure that we can properly encrypt the keys.
            string? apiKey = Environment.GetEnvironmentVariable("DATASOURCE_KEY");
            string? apiUrl = Environment.GetEnvironmentVariable("DATASOURCE_URL");

            if (apiKey == null || apiUrl == null) return null;

            using StocksContext context = new();
            using HttpClient httpClient = new();
            using HttpRequestMessage request = new (new HttpMethod("GET"), $"{apiUrl}/{symbol}?apikey={apiKey}");

            request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode) return null;

            string responseBody = await response.Content.ReadAsStringAsync();

            JsonNode? node = JsonNode.Parse(responseBody);

            if (node is null) return null;

            JsonNode? stock1 = node["historical"][0];

            DateTime added = new(DateTime.Parse((string)stock1["date"]).Ticks, DateTimeKind.Utc);

            return new Stock
            {
                Added = added,
                Symbol = (string)node["symbol"],
                Currency = "USD",
                Open = JsonDollarsToCents(stock1["open"]),
                High = JsonDollarsToCents(stock1["high"]),
                Low = JsonDollarsToCents(stock1["low"]),
                Close = JsonDollarsToCents(stock1["close"]),
                //Source = context.DataSources.Single(source => source.Id == 1),
            };
        }

        protected Int32 JsonDollarsToCents(JsonNode dollars)
        {
            return (Int32)(((decimal)dollars) * 100);
        }

        protected void StoreStock(Stock stock)
        {
            using StocksContext context = new();
            context.Stocks.Add(stock);
            context.SaveChanges();
        }
    }
}