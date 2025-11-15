using Microsoft.EntityFrameworkCore;
using MS.RoadFire.Common.External;
using MS.RoadFire.DataAccess.Context;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;

namespace MS.RoadFire.DataAccess.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        #region
        private readonly DbRoadFireContext _context;
        #endregion

        #region
        public SaleRepository(DbRoadFireContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public override async Task<IEnumerable<Sale>> GetPaginationAsync(PaginationDTO pagination)
        {
            await Task.CompletedTask;
            var queryable = _context.Sales
                .Include(x => x.User)
                .Include(x => x.Customer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));

            return queryable;
        }

        public override async Task<int> GetTotalRecordsAsync(PaginationDTO pagination)
        {
            var queryable = _context.Sales
                .Include(x => x.User)
                .Include(x => x.Customer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));

            double count = await queryable.CountAsync();

            return (int)count;
        }
        #endregion
    }
}
