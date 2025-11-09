using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Application.Services;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SupplierController : Controller
    {
        #region Internals
        private readonly ISupplierService _supplierService;
        private readonly IGenericServices<Supplier, SupplierDto> _genericServices;
        #endregion

        #region Constructor
        public SupplierController(ISupplierService supplierService, IGenericServices<Supplier, SupplierDto> genericServices)
        {
            _supplierService = supplierService;
            _genericServices = genericServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _supplierService.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetComboAsync()
        {
            var result = await _supplierService.GetAllAsync();
            result.Data = result.Data!.OrderBy(x => x.Name).ToList();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _supplierService.GetAsync(id);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(SupplierDto model)
        {
            var result = await _supplierService.AddAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(SupplierDto model)
        {
            var result = await _supplierService.UpdateAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _supplierService.DeleteAsync(id);
            return StatusCode((int)result.Code, result);
        }


        [HttpGet("paginated")]
        public virtual async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var result = await _supplierService.GetPaginationAsync(pagination);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("totalRecords")]
        public virtual async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
        {
            var result = await _supplierService.GetTotalRecordsAsync(pagination);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
