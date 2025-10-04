using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        #region Internals
        private readonly IUserServices _userServices;
        private readonly IGenericServices<User, UserDto> _genericServices;
        #endregion

        #region Constructor
        public UsersController(IUserServices userServices, IGenericServices<User, UserDto> genericServices)
        {
            _userServices = userServices;
            _genericServices = genericServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userServices.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _userServices.DeleteAsync(id);
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
