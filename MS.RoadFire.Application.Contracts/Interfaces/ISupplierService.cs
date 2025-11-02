using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface ISupplierService
    {
        Task<ResponseDto<List<SupplierDto>>> GetAllAsync();
        Task<ResponseDto<SupplierDto>> GetAsync(int id);
        Task<ResponseDto<SupplierDto>> AddAsync(SupplierDto model);
        Task<ResponseDto<SupplierDto>> UpdateAsync(SupplierDto model);
        Task<ResponseDto<bool>> DeleteAsync(int id);
        Task<ResponseDto<List<SupplierDto>>> GetPaginationAsync(PaginationDTO paginationDTO);
        Task<ResponseDto<int>> GetTotalRecordsAsync(PaginationDTO paginationDTO);
    }
}
