using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RolesController : Controller
    {
        #region Internals

        private readonly IGenericServices<Role, RoleDto> _genericServices;

        #endregion Internals

        #region Constructor

        public RolesController(IGenericServices<Role, RoleDto> genericServices)
        {
            _genericServices = genericServices;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _genericServices.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _genericServices.GetAsync(id);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(RoleDto model)
        {
            var result = await _genericServices.AddAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(RoleDto model)
        {
            Console.WriteLine($"📨 UpdateAsync recibido: Id={model.Id}, Nombre={model.Name}, IsActive={model.IsActive}");
            var result = await _genericServices.UpdateAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _genericServices.DeleteAsync(id);
            return StatusCode((int)result.Code, result);
        }

        #endregion Methods
    }
}