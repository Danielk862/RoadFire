using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;

namespace MS.RoadFire.Application.Contracts.Interfaces
{
    public interface IProductMovementService
    {
        Task<ResponseDto<List<ProductMovement>>> GetAll(int productId);
    }
}
