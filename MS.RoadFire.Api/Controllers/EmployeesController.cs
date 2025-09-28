using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeesController : Controller
    {
        #region Internals
        private readonly IEmployeeServices _employeeServices;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _employeeServices.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _employeeServices.GetAsync(id);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(EmployeeDto model)
        {
            var result = await _employeeServices.AddAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(EmployeeDto model)
        {
            var result = await _employeeServices.UpdateAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _employeeServices.DeleteAsync(id);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
