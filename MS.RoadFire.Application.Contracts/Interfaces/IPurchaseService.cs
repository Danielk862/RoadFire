using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface IPurchaseService
    {
        Task<ResponseDto<List<PurchaseDto>>> GetAllAsync();
        Task<ResponseDto<PurchaseDto>> AddAsync(PurchaseDto purchaseDto);
        Task<ResponseDto<PurchaseDto>> GetAsync(int id);
    }
}
