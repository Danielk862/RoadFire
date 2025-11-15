using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PurchaseController : Controller
    {
        #region Internals
        private readonly IPurchaseService _purchaseService;
        #endregion

        #region Constructor
        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _purchaseService.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetAsync(int transactionId)
        {
            var result = await _purchaseService.GetAsync(transactionId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(PurchaseDto model)
        {
            var result = await _purchaseService.AddAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("paginated")]
        public virtual async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var result = await _purchaseService.GetPaginationAsync(pagination);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("totalRecords")]
        public virtual async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
        {
            var result = await _purchaseService.GetTotalRecordsAsync(pagination);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
