using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Application.Services;
using MS.RoadFire.Business.Models;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransactionController : Controller
    {
        #region Internals
        private readonly ITransactionServices _transactionServices;
        #endregion

        #region Constructor
        public TransactionController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _transactionServices.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("transactionId")]
        public async Task<IActionResult> GetAsync(int transactionId)
        {
            var result = await _transactionServices.GetAsync(transactionId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(TransactionDto model)
        {
            var result = await _transactionServices.AddAsync(model);
            return StatusCode((int)result.Code, result);
        }
        #endregion
    }
}
