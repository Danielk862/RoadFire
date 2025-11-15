using MS.RoadFire.Common.External;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.DataAccess.Contracts.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetPaginationAsync(PaginationDTO pagination);
        Task<int> GetTotalRecordsAsync(PaginationDTO pagination);
    }
}
