using Microsoft.AspNetCore.Mvc;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransactionController : Controller
    {
        #region Internals
        private readonly ITransactionServices _transactionServices;
        private readonly IGenericServices<Transaction, TransactionDto> _genericServices;
        #endregion

        #region Constructor
        public TransactionController(ITransactionServices transactionServices, IGenericServices<Transaction, TransactionDto> genericServices)
        {
            _transactionServices = transactionServices;
            _genericServices = genericServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _transactionServices.GetAllAsync();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{transactionId}")]
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
