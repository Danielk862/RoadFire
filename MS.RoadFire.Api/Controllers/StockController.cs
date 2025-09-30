using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StockController : Controller
    {
        #region Internals
        private readonly IStockServices _stockServices;
        #endregion

        #region Constructor
        public StockController(IStockServices stockServices)
        {
            _stockServices = stockServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _stockServices.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("productId")]
        public async Task<IActionResult> GetAsync(int productId)
        {
            var result = await _stockServices.GetAsync(productId);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
