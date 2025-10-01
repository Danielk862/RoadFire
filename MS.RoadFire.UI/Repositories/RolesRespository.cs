using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.UI.Repositories
{
    public class RolesRepository
    {
        private readonly IRepository _repository;

        public RolesRepository(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<HttpResponseWrapper<ResponseDto<List<RoleDto>>>> GetAllAsync()
        {
            return await _repository.GetAsync<ResponseDto<List<RoleDto>>>("api/Roles/GetAll");
        }

        public async Task<HttpResponseWrapper<ResponseDto<RoleDto>>> GetByIdAsync(int id)
        {
            return await _repository.GetAsync<ResponseDto<RoleDto>>($"api/Roles/Get/id?id={id}");
        }

        public async Task<HttpResponseWrapper<ResponseDto<RoleDto>>> AddAsync(RoleDto role)
        {
            return await _repository.PostAsync<RoleDto, ResponseDto<RoleDto>>("api/Roles/Add", role);
        }

        public async Task<HttpResponseWrapper<object>> UpdateAsync(RoleDto role)
        {
            return await _repository.PutAsync<RoleDto>("api/Roles/Update", role);
        }

        public async Task<HttpResponseWrapper<T>> DeleteAsync<T>(int id)
        {
            return await _repository.DeleteAsync<T>($"api/Roles/Delete/id?id={id}");
        }
    }
}