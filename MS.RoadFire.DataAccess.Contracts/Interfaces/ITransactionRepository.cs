using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.DataAccess.Contracts.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TransactionDetail>> GetTransactionAsync(int transactionId);
    }
}
