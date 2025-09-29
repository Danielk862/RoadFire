using AutoMapper;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;
using System.Net;

namespace MS.RoadFire.Application.Services
{
    public class StockServices : IStockServices
    {
        #region Internals
        private readonly IGenericRepository<Stock> _genericRepository;
        private readonly IGenericRepository<Product> _productgeneric;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public StockServices(IGenericRepository<Stock> genericRepository, IGenericRepository<Product> productgeneric, IStockRepository stockRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _productgeneric = productgeneric;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<List<StockDto>>> GetAllAsync()
        {
            ResponseDto<List<StockDto>> response = new ResponseDto<List<StockDto>>();

            try
            {
                var result = await _genericRepository.GetAllAsync();                
                var listStock = _mapper.Map<List<StockDto>>(result);

                foreach (var item in listStock)
                {
                    var product = await _productgeneric.GetAsync(item.ProductId);
                    item.ProductDescription = product.Description;
                    item.Total = item.Quantity * item.ValueUnit;
                }
                response.Data = listStock;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<StockDto>> GetAsync(int productId)
        {
            ResponseDto<StockDto> response = new ResponseDto<StockDto>();

            try
            {
                var result = await _stockRepository.GetAsync(productId);
                var stock = _mapper.Map<StockDto>(result);
                var product = await _productgeneric.GetAsync(stock.ProductId);
                stock.ProductDescription = product.Description;
                stock.Total = stock.Quantity * stock.ValueUnit;
                response.Data = stock;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }
        #endregion
    }
}
