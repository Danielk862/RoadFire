using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        #region Internals
        private readonly IUserServices _userServices;
        #endregion

        #region Constructor
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userServices.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _userServices.GetAsync(id);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(UserDto model)
        {
            var result = await _userServices.AddAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UserDto model)
        {
            var result = await _userServices.UpdateAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _userServices.DeleteAsync(id);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
