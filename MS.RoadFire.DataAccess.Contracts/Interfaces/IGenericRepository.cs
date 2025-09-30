using MS.RoadFire.Common.Helpers;
using System.Linq.Expressions;

namespace MS.RoadFire.DataAccess.Contracts.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> Get(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAll(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T model);
        Task<T> UpdateAsync(T model);
        Task<bool> DeleteAsync(int id);
    }
}
