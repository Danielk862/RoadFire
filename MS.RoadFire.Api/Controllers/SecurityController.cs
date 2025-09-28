using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SecurityController : Controller
    {
        #region Internals
        private readonly ISecurityServices _securityServices;
        #endregion

        #region Constructor
        public SecurityController(ISecurityServices securityServices)
        {
            _securityServices = securityServices;
        }
        #endregion

        #region Methods
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _securityServices.Login(username, password);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
