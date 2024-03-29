using DataAccessLayer.Models;
using DataAccessLayer.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly ILogger<StocksController> _logger;

        public StocksController(ILogger<StocksController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "StockPrices")]
        public IEnumerable<string> Get()
        {
            return StockFetcher.limitedList;
        }

        [HttpGet("{symbol}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StockPrice>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockPrice>>> Get(string symbol, string? first, string? last)
        {
            string sanitizedSymbol = SanitizeInput(symbol);

            DateTime utcLast = last == null ? DateTime.UtcNow : Convert.ToDateTime(last).ToUniversalTime();
            DateTime utcFirst = first == null ? utcLast.AddMonths(-1) : Convert.ToDateTime(first).ToUniversalTime();

            Stock? stock = StockFetcher.Fetch(sanitizedSymbol);
            if (stock == null) return NotFound();

            StockPriceFetcher fetcher = new();
            List<StockPrice> stockPrices = await fetcher.Fetch(stock, utcFirst.Date, utcLast.Date);
            if (stockPrices.Count == 0) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(stockPrices);
        }

        private static string SanitizeInput(string input)
        {
            Regex regex = new("[^a-zA-Z0-9._-]");

            return regex.Replace(input, "");
        }
    }
}