using Microsoft.EntityFrameworkCore;
using MS.RoadFire.DataAccess.Context;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;

namespace MS.RoadFire.DataAccess.Repositories
{
    public class StockRepository : IStockRepository
    {
        #region internals
        private readonly DbRoadFireContext _context;
        #endregion

        #region Contructor
        public StockRepository(DbRoadFireContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<Stock?> GetAsync(int productId) 
        {   
            return await _context.Stock.AsNoTracking().Where(x => x.ProductId == productId).FirstOrDefaultAsync();
        } 
    }
    #endregion
}
