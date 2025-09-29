using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface IProductServices
    {
        Task<ResponseDto<List<ProductDto>>> GetAllAsync();
        Task<ResponseDto<ProductDto>> GetAsync(int id);
        Task<ResponseDto<ProductDto>> AddAsync(ProductDto model);
        Task<ResponseDto<ProductDto>> UpdateAsync(ProductDto model);
        Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
