using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface IGenericServices<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<ResponseDto<List<TDto>>> GetAllAsync();
        Task<ResponseDto<TDto>> GetAsync(int id);
        Task<ResponseDto<TDto>> AddAsync(TDto model);
        Task<ResponseDto<TDto>> UpdateAsync(TDto model);
        Task<ResponseDto<bool>> DeleteAsync(int id);
        Task<ResponseDto<List<TEntity>>> GetPaginationAsync(PaginationDTO paginationDTO);
        Task<ResponseDto<int>> GetTotalRecordsAsync(PaginationDTO paginationDTO);

    }
}
