using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.DataAccess.Contracts.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetAsync(int productId);
    }
}
