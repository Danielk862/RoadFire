using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StockController : Controller
    {
        #region Internals
        private readonly IStockServices _stockServices;
        private readonly IGenericServices<Stock, StockDto> _genericServices;
        #endregion

        #region Constructor
        public StockController(IStockServices stockServices, IGenericServices<Stock, StockDto> genericServices)
        {
            _stockServices = stockServices;
            _genericServices = genericServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _stockServices.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetAsync(int productId)
        {
            var result = await _stockServices.GetAsync(productId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("paginated")]
        public virtual async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var result = await _genericServices.GetPaginationAsync(pagination);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("totalRecords")]
        public virtual async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
        {
            var result = await _genericServices.GetTotalRecordsAsync(pagination);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
