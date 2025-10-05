using AutoMapper;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.Common.Resource;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;
using System.Net;

namespace MS.RoadFire.Application.Services
{
    public class SaleService : ISaleService
    {
        #region Internals
        private readonly IGenericRepository<Sale> _genericRepository;
        private readonly IGenericRepository<SaleDetails> _genericSaleDetailsRepository;
        private readonly IGenericRepository<Product> _genericProductRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IStockServices _stockServices;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public SaleService(IGenericRepository<Sale> genericRepository, IGenericRepository<User> userRepository,
            IGenericRepository<SaleDetails> genericSaleDetailsRepository, IGenericRepository<Product> genericProductRepository,
            IStockServices stockServices, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _genericSaleDetailsRepository = genericSaleDetailsRepository;
            _genericProductRepository = genericProductRepository;
            _userRepository = userRepository;
            _stockServices = stockServices;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<SaleDto>> AddAsync(SaleDto saleDto)
        {
            ResponseDto<SaleDto> response = new ResponseDto<SaleDto>();

            try
            {
                SaleDto result = new SaleDto();
                List<SaleDetailsDto> listSaleDetail = new List<SaleDetailsDto>();
                var isExist = await _userRepository.GetAsync(saleDto.UserId);

                if (isExist == null)
                {
                    response.Code = HttpStatusCode.BadRequest;
                    response.Messages = MessagesResource.UserNotExist;
                    return response;
                }

                foreach (var item in saleDto.SaleDetailsDtos!)
                {
                    var stock = new StockDto()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ValueUnit = item.UnitValue
                    };

                    var resultStock = await _stockServices.StockValidate(stock, saleDto.Type);

                    if (resultStock.Code != HttpStatusCode.OK)
                    {
                        response.Code = resultStock.Code;
                        response.Messages = resultStock.Messages;
                        return response;
                    }
                }

                saleDto.Date = DateTime.Now;
                var data = await _genericRepository.AddAsync(_mapper.Map<Sale>(saleDto));

                foreach (var item in saleDto.SaleDetailsDtos!)
                {
                    var price = await _genericProductRepository.GetAsync(item.ProductId);
                    item.SaleId = data.Id;
                    item.UnitValue = price.Price;
                    var detail = _mapper.Map<SaleDetails>(item);
                    detail.Product = price;
                    var saveDetail = await _genericSaleDetailsRepository.AddAsync(detail);
                    var saleDetailDto = _mapper.Map<SaleDetailsDto>(saveDetail);
                    saleDetailDto.Total = saleDetailDto.Quantity * saleDetailDto.UnitValue;
                    saleDetailDto.ProductDescription = price.Description;
                    result = _mapper.Map<SaleDto>(data);
                    listSaleDetail.Add(saleDetailDto);
                }
                result.SaleDetailsDtos!.AddRange(listSaleDetail);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<List<SaleDto>>> GetAllAsync()
        {
            ResponseDto<List<SaleDto>> response = new ResponseDto<List<SaleDto>>();

            try
            {
                SaleDto sale = new SaleDto();
                List<SaleDto> listSale = new List<SaleDto>();

                var data = await _genericRepository.GetAllAsync();

                foreach (var item in data)
                {
                    var saleDetails = await _genericSaleDetailsRepository.GetAll(x => x.SaleId == item.Id);
                    sale = _mapper.Map<SaleDto>(item);
                    List<SaleDetailsDto> saleDetailsDto = new List<SaleDetailsDto>();

                    foreach (var detail in saleDetails)
                    {
                        var saleDetail = _mapper.Map<SaleDetailsDto>(detail);
                        var price = await _genericProductRepository.GetAsync(detail.ProductId);
                        saleDetail.UnitValue = price.Price;
                        saleDetail.Total = saleDetail.Quantity * saleDetail.UnitValue;
                        saleDetail.ProductDescription = price.Description;
                        saleDetailsDto.Add(saleDetail);
                    }
                    sale.SaleDetailsDtos!.AddRange(saleDetailsDto);
                    listSale.Add(sale);
                }
                response.Data = listSale;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<SaleDto>> GetAsync(int id)
        {
            ResponseDto<SaleDto> response = new ResponseDto<SaleDto>();

            try
            {
                SaleDto result = new SaleDto();
                List<SaleDetailsDto> saleDetailsDto = new List<SaleDetailsDto>();

                var data = await _genericRepository.GetAsync(id);
                var saleDetail = await _genericSaleDetailsRepository.GetAll(x => x.SaleId == id);

                foreach (var item in saleDetail)
                {
                    var saleDetails = _mapper.Map<SaleDetailsDto>(item);
                    var price = await _genericProductRepository.GetAsync(item.ProductId);
                    saleDetails.UnitValue = price.Price;
                    saleDetails.Total = saleDetails.Quantity * saleDetails.UnitValue;
                    saleDetails.ProductDescription = price.Description;
                    result = _mapper.Map<SaleDto>(data);
                    saleDetailsDto.Add(saleDetails);
                }
                result.SaleDetailsDtos!.AddRange(saleDetailsDto);
                response.Data = result;
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
