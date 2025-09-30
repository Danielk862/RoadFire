using AutoMapper;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Constants;
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
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public StockServices(IGenericRepository<Stock> genericRepository, IGenericRepository<Product> productgeneric, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _productgeneric = productgeneric;
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
                var result = await _genericRepository.Get(x => x.ProductId == productId);
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

        public async Task<ResponseDto<StockDto>> StockValidate(StockDto stockDto, string type)
        {
            ResponseDto<StockDto> response = new ResponseDto<StockDto>();

            try
            { 
                var isExists = await _genericRepository.Get(x => x.ProductId == stockDto.ProductId);
                var product = await _productgeneric.Get(x => x.Id == stockDto.ProductId);

                if (isExists == null)
                {
                    if (type.Equals(TypeTransactionConstants.Input) || type.Equals(TypeTransactionConstants.Purchase))
                    {
                        var data = _mapper.Map<Stock>(stockDto);
                        data.Product = null;
                        var save = await _genericRepository.AddAsync(data);
                        response.Data = stockDto;
                        return response;
                    }
                    else
                    {
                        response.Code = HttpStatusCode.BadRequest;
                        response.Messages = $"El producto {product.Description} no tiene stock suficiente.";
                    }
                }
                else
                {
                    if (type.Equals(TypeTransactionConstants.Output) || type.Equals(TypeTransactionConstants.Sales))
                    {
                        if (isExists.Quantity < stockDto.Quantity)
                        {
                            response.Code = HttpStatusCode.BadRequest;
                            response.Messages = $"El producto {product.Description} no tiene stock suficiente.";
                        }
                        else if (isExists.Quantity > 0)
                        {
                            isExists.Quantity = isExists.Quantity - stockDto.Quantity;
                            var update = await _genericRepository.UpdateAsync(isExists);
                            response.Data = _mapper.Map<StockDto>(update);
                        }
                    }

                    if (type.Equals(TypeTransactionConstants.Input) || type.Equals(TypeTransactionConstants.Purchase))
                    {
                        isExists.Quantity = isExists.Quantity + stockDto.Quantity;
                        var update = await _genericRepository.UpdateAsync(isExists);
                        response.Data = _mapper.Map<StockDto>(update);
                    }
                }
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
