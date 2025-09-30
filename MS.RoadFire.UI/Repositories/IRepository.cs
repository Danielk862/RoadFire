using System.Net.Http;
using System.Threading.Tasks;

namespace MS.RoadFire.UI.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url);

        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T data);

        Task<HttpResponseWrapper<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest data);

        Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T data);

        Task<HttpResponseWrapper<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest data);

        Task<HttpResponseWrapper<object>> DeleteAsync(string url);
    }
}