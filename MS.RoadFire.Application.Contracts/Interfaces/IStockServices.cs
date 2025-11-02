using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface IStockServices
    {
        Task<ResponseDto<List<StockDto>>> GetAllAsync();
        Task<ResponseDto<StockDto>> GetAsync(int productId);
        Task<ResponseDto<StockDto>> StockValidate(StockDto stockDto, string type);
        Task<ResponseDto<List<StockDto>>> GetPaginationAsync(PaginationDTO paginationDTO);
        Task<ResponseDto<int>> GetTotalRecordsAsync(PaginationDTO paginationDTO);
    }
}
