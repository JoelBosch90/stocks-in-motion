using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
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
            return Summaries;
        }

        [HttpGet("{symbol}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Stock))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Stock>> Get(string symbol)
        {
            if (Array.IndexOf(Summaries, symbol) < 0) return NotFound();

            using StocksContext context = new();

            Stock? stock = context.Stocks.SingleOrDefault(stock => stock.Symbol == symbol);

            if(stock == null)
            {
                stock = await RetrieveStock(symbol);

                if (stock == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

                StoreStock(stock);
            }
            return Ok(stock);
        }

        protected async Task<Stock?> RetrieveStock(string symbol)
        {
            using StocksContext context = new();
            using HttpClient httpClient = new();
            using HttpRequestMessage request = new (new HttpMethod("GET"), $"https://financialmodelingprep.com/api/v3/historical-price-full/{symbol}?apikey=***REMOVED***");

            request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode) return null;

            string responseBody = await response.Content.ReadAsStringAsync();

            JsonNode? json = JsonNode.Parse(responseBody);

            if (json is null) return null;

            var stock1 = json["historical"][0];

            DateTime added = new DateTime(DateTime.Parse((string)stock1["date"]).Ticks, DateTimeKind.Utc);

            return new Stock
            {
                Added = added,
                Symbol = (string)json["symbol"],
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