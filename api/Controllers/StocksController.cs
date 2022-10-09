using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

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
        public Stock? Get(string symbol)
        {
            using var context = new StocksContext();

            return context.Stocks.FirstOrDefault(stock => stock.Ticker == symbol);
        }
    }
}