using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface IStockServices
    {
        Task<ResponseDto<List<StockDto>>> GetAllAsync();
        Task<ResponseDto<StockDto>> GetAsync(int productId);
    }
}
