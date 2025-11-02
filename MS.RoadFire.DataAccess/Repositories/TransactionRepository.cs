using Microsoft.EntityFrameworkCore;
using MS.RoadFire.Common.External;
using MS.RoadFire.DataAccess.Context;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;

namespace MS.RoadFire.DataAccess.Repositories
{
    public class TransactionRepository : GenericRepository<TransactionDetail>, ITransactionRepository
    {
        #region Internals
        private readonly DbRoadFireContext _context;
        #endregion

        #region Constructor
        public TransactionRepository(DbRoadFireContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public override async Task<IEnumerable<TransactionDetail>> GetPaginationAsync(PaginationDTO pagination)
        {
            await Task.CompletedTask;
            var queryable = _context.TransactionDetails
                .Include(x => x.Transaction)
                .Include(x => x.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
                queryable = queryable.Where(x => x.Product!.Description.ToLower().Contains(pagination.Filter.ToLower()) || x.Transaction!.Description.ToLower().Contains(pagination.Filter.ToLower()));

            return queryable;
        }

        public override async Task<int> GetTotalRecordsAsync(PaginationDTO pagination)
        {
            var queryable = _context.TransactionDetails
                .Include(x => x.Transaction)
                .Include(x => x.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
                queryable = queryable.Where(x => x.Product!.Description.ToLower().Contains(pagination.Filter.ToLower()) || x.Transaction!.Description.ToLower().Contains(pagination.Filter.ToLower()));            

            double count = await queryable.CountAsync();

            return (int)count;
        }
        #endregion
    }
}
