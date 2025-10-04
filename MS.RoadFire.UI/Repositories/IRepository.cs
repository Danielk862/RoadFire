using MS.RoadFire.Common.Helpers;
using System.Threading.Tasks;

namespace MS.RoadFire.UI.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url);
        Task<HttpResponseWrapper<T>> PostAsync<T>(string url, T model);
        Task<HttpResponseWrapper<T>> DeleteAsync<T>(string url);
        Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model);
        Task<HttpResponseWrapper<T>> PutAsync<T, TActionResponse>(string url, T model);
    }
}