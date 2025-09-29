using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        #region Internals
        private readonly IProductServices _productServices;
        #endregion

        #region Constructor
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productServices.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _productServices.GetAsync(id);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductDto model)
        {
            var result = await _productServices.AddAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProductDto model)
        {
            var result = await _productServices.UpdateAsync(model);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productServices.DeleteAsync(id);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
