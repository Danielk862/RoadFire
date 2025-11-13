using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductMovementController : Controller
    {
        #region Internals
        private readonly IProductMovementService _productMovementService;
        #endregion

        #region Constructor
        public ProductMovementController(IProductMovementService productMovementService)
        {
            _productMovementService = productMovementService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int productId)
        {
            var result = await _productMovementService.GetAll(productId);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
