using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface ISaleService
    {
        Task<ResponseDto<List<SaleDto>>> GetAllAsync();
        Task<ResponseDto<SaleDto>> AddAsync(SaleDto saleDto);
        Task<ResponseDto<SaleDto>> GetAsync(int id);
    }
}
