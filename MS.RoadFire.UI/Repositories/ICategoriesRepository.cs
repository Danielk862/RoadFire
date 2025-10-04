using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.UI.Repositories
{
    public interface ICategoriesRepository
    {
        Task<HttpResponseWrapper<ResponseDto<List<CategoryDto>>>> GetAllAsync();

        Task<HttpResponseWrapper<ResponseDto<CategoryDto>>> GetByIdAsync(int id);

        Task<HttpResponseWrapper<ResponseDto<CategoryDto>>> UpdateAsync(CategoryDto category);

        Task<HttpResponseWrapper<ResponseDto<bool>>> DeleteAsync(int id);
    }
}