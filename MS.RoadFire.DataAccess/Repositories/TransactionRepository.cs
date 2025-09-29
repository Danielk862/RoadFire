using Microsoft.EntityFrameworkCore;
using MS.RoadFire.DataAccess.Context;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;

namespace MS.RoadFire.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        #region
        private readonly DbRoadFireContext _dbroadFireContext;
        #endregion

        #region Constructor
        public TransactionRepository(DbRoadFireContext dbroadFireContext)
        {
            _dbroadFireContext = dbroadFireContext;
        }
        #endregion

        #region Methods
        public async Task<List<TransactionDetail>> GetTransactionAsync(int transactionId)
        {
            return await _dbroadFireContext.TransactionDetails.AsNoTracking().Where(x => x.TransactionId == transactionId).ToListAsync();
        }
        #endregion
    }
}
