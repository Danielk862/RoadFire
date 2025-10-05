using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface ICustomerService
    {
        Task<ResponseDto<List<CustomerDto>>> GetAllAsync();
        Task<ResponseDto<CustomerDto>> GetAsync(int id);
        Task<ResponseDto<CustomerDto>> AddAsync(CustomerDto model);
        Task<ResponseDto<CustomerDto>> UpdateAsync(CustomerDto model);
        Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
