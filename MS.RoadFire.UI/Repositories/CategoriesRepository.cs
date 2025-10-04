using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using System.Text.Json;

namespace MS.RoadFire.UI.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IRepository _repository;

        public CategoriesRepository(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<HttpResponseWrapper<ResponseDto<List<CategoryDto>>>> GetAllAsync()
        {
            return await _repository.GetAsync<ResponseDto<List<CategoryDto>>>("api/Category/GetAll");
        }

        public async Task<HttpResponseWrapper<ResponseDto<CategoryDto>>> GetByIdAsync(int id)
        {
            return await _repository.GetAsync<ResponseDto<CategoryDto>>($"api/Category/Get/id?id={id}");
        }

        public async Task<HttpResponseWrapper<ResponseDto<CategoryDto>>> UpdateAsync(CategoryDto category)
        {
            var response = await _repository.PutAsync("api/Category/Update", category);

            ResponseDto<CategoryDto>? data = null;
            if (response.Response != null)
            {
                var json = JsonSerializer.Serialize(response.Response);
                data = JsonSerializer.Deserialize<ResponseDto<CategoryDto>>(json);
            }

            return new HttpResponseWrapper<ResponseDto<CategoryDto>>(
                data!,                
                response.Error,       
                response.HttpResponseMessage
            );
        }

        // Eliminar categoría por ID
        public async Task<HttpResponseWrapper<ResponseDto<bool>>> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync<ResponseDto<bool>>($"api/Category/Delete/id?id={id}");
        }
    }
}