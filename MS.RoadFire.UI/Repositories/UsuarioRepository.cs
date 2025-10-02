using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using static System.Net.WebRequestMethods;

namespace MS.RoadFire.UI.Repositories
{
    public class UsuariosRepository
    {
        private readonly IRepository _repository;

        public UsuariosRepository(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<HttpResponseWrapper<ResponseDto<List<UserDto>>>> GetAllAsync()
        {
            return await _repository.GetAsync<ResponseDto<List<UserDto>>>("api/Users/GetAll");
        }

        public async Task<HttpResponseWrapper<ResponseDto<UserDto>>> GetByIdAsync(int id)
        {
            return await _repository.GetAsync<ResponseDto<UserDto>>($"api/Users/Get/id?id={id}");
        }

        public async Task<HttpResponseWrapper<ResponseDto<UserDto>>> AddAsync(UserDto user)
        {
            return await _repository.PostAsync<UserDto, ResponseDto<UserDto>>("api/Users/Add", user);
        }

        public async Task<HttpResponseWrapper<ResponseDto<UserDto>>> UpdateAsync(UserDto user)
        {
            return await _repository.PutAsync<UserDto, ResponseDto<UserDto>>("api/Users/Update", user);
        }

        public async Task<HttpResponseWrapper<object>> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync($"api/Users/Delete/id?id={id}");
        }
    }
}