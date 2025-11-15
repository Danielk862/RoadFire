using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SaleController : Controller
    {
        #region Internals
        private readonly ISaleService _saleService;
        #endregion

        #region Constructor
        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _saleService.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetAsync(int transactionId)
        {
            var result = await _saleService.GetAsync(transactionId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(SaleDto model)
        {
            var result = await _saleService.AddAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("paginated")]
        public virtual async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var result = await _saleService.GetPaginationAsync(pagination);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("totalRecords")]
        public virtual async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
        {
            var result = await _saleService.GetTotalRecordsAsync(pagination);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
