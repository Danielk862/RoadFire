using MS.RoadFire.Common.External;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.DataAccess.Contracts.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetPaginationAsync(PaginationDTO pagination);
        Task<int> GetTotalRecordsAsync(PaginationDTO pagination);
    }
}
