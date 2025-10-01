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
            return await _repository.GetAsync<ResponseDto<CategoryDto>>($"api/Category/Get?id={id}");
        }

        public async Task<HttpResponseWrapper<ResponseDto<CategoryDto>>> AddAsync(CategoryDto category)
        {
            return await _repository.PostAsync<CategoryDto, ResponseDto<CategoryDto>>("api/Category/Add", category);
        }

        // revisar aca con gomez
        public async Task<HttpResponseWrapper<ResponseDto<CategoryDto>>> UpdateAsync(CategoryDto category)
        {
            // Llamamos a PutAsync, que devuelve HttpResponseWrapper<object>
            var response = await _repository.PutAsync("api/Category/Update", category);

            // Convertimos el "object" a JSON y luego lo deserializamos al tipo esperado
            ResponseDto<CategoryDto>? data = null;
            if (response.Response != null)
            {
                var json = JsonSerializer.Serialize(response.Response);
                data = JsonSerializer.Deserialize<ResponseDto<CategoryDto>>(json);
            }

            // Creamos una nueva respuesta tipada
            return new HttpResponseWrapper<ResponseDto<CategoryDto>>(
             data!,                 // ✅ contenido
             response.Error,        // ✅ bool error
             response.HttpResponseMessage // ✅ HttpResponseMessage
         );
        }

        // ✅ Eliminar categoría por ID
        public async Task<HttpResponseWrapper<ResponseDto<bool>>> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync<ResponseDto<bool>>($"api/Category/Delete?id={id}");
        }
    }
}