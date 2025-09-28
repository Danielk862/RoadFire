using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface ISecurityServices
    {
        Task<ResponseDto<UserDto>> Login(string username, string password);
    }
}
