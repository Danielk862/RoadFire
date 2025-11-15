using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface IPurchaseService
    {
        Task<ResponseDto<List<PurchaseDto>>> GetAllAsync();
        Task<ResponseDto<PurchaseDto>> AddAsync(PurchaseDto purchaseDto);
        Task<ResponseDto<PurchaseDto>> GetAsync(int id);
        Task<ResponseDto<List<PurchaseDto>>> GetPaginationAsync(PaginationDTO paginationDTO);
        Task<ResponseDto<int>> GetTotalRecordsAsync(PaginationDTO paginationDTO);
    }
}
