using AutoMapper;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.Common.Resource;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace MS.RoadFire.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        #region Internals
        private readonly IGenericRepository<Purchase> _genericRepository;
        private readonly IGenericRepository<PurchaseDetails> _genericPurchaseDetailsRepository;
        private readonly IGenericRepository<Product> _genericProductRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IStockServices _stockServices;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PurchaseService(IGenericRepository<Purchase> genericRepository, IGenericRepository<User> userRepository,
            IGenericRepository<PurchaseDetails> genericPurchaseDetailsRepository, IGenericRepository<Product> genericProductRepository,
            IStockServices stockServices, IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _genericPurchaseDetailsRepository = genericPurchaseDetailsRepository;
            _genericProductRepository = genericProductRepository;
            _userRepository = userRepository;
            _stockServices = stockServices;
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<PurchaseDto>> AddAsync(PurchaseDto purchaseDto)
        {
            ResponseDto<PurchaseDto> response = new ResponseDto<PurchaseDto>();

            try
            {
                PurchaseDto result = new PurchaseDto();
                List<PurchaseDetailsDto> listPurchaseDetail = new List<PurchaseDetailsDto>();
                var isExist = await _userRepository.GetAsync(purchaseDto.UserId);

                if (isExist == null)
                {
                    response.Code = HttpStatusCode.BadRequest;
                    response.Messages = MessagesResource.UserNotExist;
                    return response;
                }

                foreach (var item in purchaseDto.PurchaseDetailsDtos!)
                {
                    var stock = new StockDto()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ValueUnit = item.UnitValue
                    };

                    var resultStock = await _stockServices.StockValidate(stock, purchaseDto.Type);

                    if (resultStock.Code != HttpStatusCode.OK)
                    {
                        response.Code = resultStock.Code;
                        response.Messages = resultStock.Messages;
                        return response;
                    }
                }

                purchaseDto.Date = DateTime.Now;
                var request = _mapper.Map<Purchase>(purchaseDto);
                request.Supplier = null;
                var data = await _genericRepository.AddAsync(request);

                foreach (var item in purchaseDto.PurchaseDetailsDtos!)
                {
                    var price = await _genericProductRepository.GetAsync(item.ProductId);
                    item.PurchaseId = data.Id;
                    item.UnitValue = price.Price;
                    var detail = _mapper.Map<PurchaseDetails>(item);
                    detail.Product = price;
                    var saveDetail = await _genericPurchaseDetailsRepository.AddAsync(detail);
                    var purchaseDetail = _mapper.Map<PurchaseDetailsDto>(saveDetail);
                    purchaseDetail.Total = purchaseDetail.Quantity * purchaseDetail.UnitValue;
                    purchaseDetail.ProductDescription = price.Description;
                    result = _mapper.Map<PurchaseDto>(data);
                    listPurchaseDetail.Add(purchaseDetail);
                }
                result.PurchaseDetailsDtos!.AddRange(listPurchaseDetail);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<List<PurchaseDto>>> GetAllAsync()
        {
            ResponseDto<List<PurchaseDto>> response = new ResponseDto<List<PurchaseDto>>();

            try
            {
                PurchaseDto sale = new PurchaseDto();
                List<PurchaseDto> listPurchase = new List<PurchaseDto>();

                var data = await _genericRepository.GetAllAsync();

                foreach (var item in data)
                {
                    var purchaseDetails = await _genericPurchaseDetailsRepository.GetAll(x => x.PurchaseId == item.Id);
                    sale = _mapper.Map<PurchaseDto>(item);
                    List<PurchaseDetailsDto> listPurchaseDetail = new List<PurchaseDetailsDto>();

                    foreach (var detail in purchaseDetails)
                    {
                        var purchaseDetail = _mapper.Map<PurchaseDetailsDto>(detail);
                        var price = await _genericProductRepository.GetAsync(detail.ProductId);
                        purchaseDetail.UnitValue = price.Price;
                        purchaseDetail.Total = purchaseDetail.Quantity * purchaseDetail.UnitValue;
                        purchaseDetail.ProductDescription = price.Description;
                        listPurchaseDetail.Add(purchaseDetail);
                    }
                    sale.PurchaseDetailsDtos!.AddRange(listPurchaseDetail);
                    listPurchase.Add(sale);
                }
                response.Data = listPurchase;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<PurchaseDto>> GetAsync(int id)
        {
            ResponseDto<PurchaseDto> response = new ResponseDto<PurchaseDto>();

            try
            {
                PurchaseDto result = new PurchaseDto();
                List<PurchaseDetailsDto> purchaseDetailsDto = new List<PurchaseDetailsDto>();

                var data = await _genericRepository.GetAsync(id);
                var purchaseDetail = await _genericPurchaseDetailsRepository.GetAll(x => x.PurchaseId == id);

                foreach (var item in purchaseDetail)
                {
                    var purchaseDetails = _mapper.Map<PurchaseDetailsDto>(item);
                    var price = await _genericProductRepository.GetAsync(item.ProductId);
                    purchaseDetails.UnitValue = price.Price;
                    purchaseDetails.Total = purchaseDetails.Quantity * purchaseDetails.UnitValue;
                    purchaseDetails.ProductDescription = price.Description;
                    result = _mapper.Map<PurchaseDto>(data);
                    purchaseDetailsDto.Add(purchaseDetails);
                }
                result.PurchaseDetailsDtos!.AddRange(purchaseDetailsDto);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<List<PurchaseDto>>> GetPaginationAsync(PaginationDTO paginationDTO)
        {
            ResponseDto<List<PurchaseDto>> response = new ResponseDto<List<PurchaseDto>>();

            try
            {
                var request = await _purchaseRepository.GetPaginationAsync(paginationDTO);
                var result = _mapper.Map<List<PurchaseDto>>(request);

                foreach (var item in result)
                {
                    var detail = await _genericPurchaseDetailsRepository.GetAll(x => x.PurchaseId == item.Id);
                    var purchaseDetails = _mapper.Map<List<PurchaseDetailsDto>>(detail);
                    item.UserName = request.Where(x => x.UserId == item.UserId).Select(x => x.User!.Username).FirstOrDefault()!;
                    item.SupplierName = request.Where(x => x.SupplierId == item.SupplierId).Select(x => x.Supplier!.Name).FirstOrDefault()!;
                    item.PurchaseDetailsDtos.AddRange(purchaseDetails);

                    foreach (var product in item.PurchaseDetailsDtos)
                    {
                        var searchProduct = await _genericProductRepository.GetAsync(product.ProductId);
                        product.ProductDescription = searchProduct.Description;
                    }
                }

                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }


        public async Task<ResponseDto<int>> GetTotalRecordsAsync(PaginationDTO paginationDTO)
        {
            ResponseDto<int> response = new ResponseDto<int>();

            try
            {
                var request = await _purchaseRepository.GetTotalRecordsAsync(paginationDTO);
                response.Data = request;
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
