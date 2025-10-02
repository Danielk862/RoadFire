using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.UI.Repositories
{
    public class EmployeesRepository
    {
        private readonly IRepository _repository;

        public EmployeesRepository(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<HttpResponseWrapper<ResponseDto<List<EmployeeDto>>>> GetAllAsync()
        {
            return await _repository.GetAsync<ResponseDto<List<EmployeeDto>>>("api/Employees/GetAll");
        }

        public async Task<HttpResponseWrapper<ResponseDto<EmployeeDto>>> GetByIdAsync(int id)
        {
            return await _repository.GetAsync<ResponseDto<EmployeeDto>>($"api/Employees/Get?id={id}");
        }

        public async Task<HttpResponseWrapper<ResponseDto<EmployeeDto>>> AddAsync(EmployeeDto employee)
        {
            return await _repository.PostAsync<EmployeeDto, ResponseDto<EmployeeDto>>("api/Employees/Add", employee);
        }

        public async Task<HttpResponseWrapper<ResponseDto<EmployeeDto>>> UpdateAsync(EmployeeDto employee)
        {
            return await _repository.PutAsync<EmployeeDto, ResponseDto<EmployeeDto>>("api/Employees/Update", employee);
        }

        public async Task<HttpResponseWrapper<object>> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync($"api/Employees/Delete?id={id}");
        }
    }
}