using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface IEmployeeServices
    {
        Task<ResponseDto<List<EmployeeDto>>> GetAllAsync();
        Task<ResponseDto<EmployeeDto>> GetAsync(int id);
        Task<ResponseDto<EmployeeDto>> AddAsync(EmployeeDto model);
        Task<ResponseDto<EmployeeDto>> UpdateAsync(EmployeeDto model);
        Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
