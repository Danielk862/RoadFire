using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface IUserServices
    {
        Task<ResponseDto<List<UserDto>>> GetAllAsync();
        Task<ResponseDto<UserDto>> GetAsync(int id);
        Task<ResponseDto<UserDto>> AddAsync(UserDto model);
        Task<ResponseDto<UserDto>> UpdateAsync(UserDto model);
        Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
