using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface IRoleServices
    {
        Task<ResponseDto<List<RoleDto>>> GetAllAsync();
        Task<ResponseDto<RoleDto>> GetAsync(int id);
        Task<ResponseDto<RoleDto>> AddAsync(RoleDto model);
        Task<ResponseDto<RoleDto>> UpdateAsync(RoleDto model);
        Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
