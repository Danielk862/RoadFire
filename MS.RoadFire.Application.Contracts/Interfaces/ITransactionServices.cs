using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface ITransactionServices
    {
        Task<ResponseDto<List<TransactionDto>>> GetAllAsync();
        Task<ResponseDto<TransactionDto>> AddAsync(TransactionDto transactionDto);
        Task<ResponseDto<TransactionDto>> GetAsync(int id);
    }
}
