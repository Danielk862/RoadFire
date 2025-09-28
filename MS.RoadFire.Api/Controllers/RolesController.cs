using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RolesController : Controller
    {
        #region Internals
        private readonly IRoleServices _roleServices;
        #endregion

        #region Constructor
        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _roleServices.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _roleServices.GetAsync(id);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(RoleDto model)
        {
            var result = await _roleServices.AddAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(RoleDto model)
        {
            var result = await _roleServices.UpdateAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _roleServices.DeleteAsync(id);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
