using DataAccessLayer.Models;
using DataAccessLayer.Tools;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<StockPrice>>> Get(string symbol)
        {
            Stock? stock = StockFetcher.Fetch(symbol);
            if (stock == null) return NotFound();

            List<StockPrice> stockPrices = await StockPriceFetcher.Fetch(stock);
            if (stockPrices.Count == 0) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(stockPrices);
        }
    }
}