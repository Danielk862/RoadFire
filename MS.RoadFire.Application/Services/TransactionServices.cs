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
    public class TransactionServices : ITransactionServices
    {
        #region Internals
        private readonly IGenericRepository<Transaction> _genericRepository;
        private readonly IGenericRepository<TransactionDetail> _genericTransactionDetailRepository;
        private readonly IGenericRepository<Product> _genericProductRepository;
        private readonly IGenericRepository<User> _userrepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public TransactionServices(IGenericRepository<Transaction> genericRepository, IGenericRepository<User> userrepository,
            IGenericRepository<TransactionDetail> genericTransactionDetailRepository, IGenericRepository<Product> genericProductRepository,
            IMapper mapper)
        {
            _genericRepository = genericRepository;
            _genericTransactionDetailRepository = genericTransactionDetailRepository;
            _genericProductRepository = genericProductRepository;
            _userrepository = userrepository;
            _mapper = mapper;
        }

        #endregion

        #region Methods
        public async Task<ResponseDto<TransactionDto>> AddAsync(TransactionDto transactionDto)
        {
            ResponseDto<TransactionDto> response = new ResponseDto<TransactionDto>();

            try
            {
                TransactionDto result = new TransactionDto();
                List<TransactionDetailDto> transactionDetailDtos = new List<TransactionDetailDto>();
                var isExist = await _userrepository.GetAsync(transactionDto.UserId);

                if (isExist == null)
                {
                    response.Code = HttpStatusCode.BadRequest;
                    response.Messages = MessagesResource.UserNotExist;
                    return response;
                }

                transactionDto.Date = DateTime.Now;
                var data = await _genericRepository.AddAsync(_mapper.Map<Transaction>(transactionDto));

                foreach (var item in transactionDto.TransactionDetailDtos!)
                {
                    var price = await _genericProductRepository.GetAsync(item.ProductId);
                    item.TransactionId = data.Id;
                    item.UnitValue = price.Price;
                    var detail = _mapper.Map<TransactionDetail>(item);
                    detail.Product = price;
                    var saveDetail = await _genericTransactionDetailRepository.AddAsync(detail);
                    var transactionDetailDto = _mapper.Map<TransactionDetailDto>(saveDetail);
                    transactionDetailDto.Total = transactionDetailDto.Quantity * transactionDetailDto.UnitValue;
                    transactionDetailDto.ProductDescription = price.Description;
                    result = _mapper.Map<TransactionDto>(data);
                    transactionDetailDtos.Add(transactionDetailDto);
                }
                result.TransactionDetailDtos!.AddRange(transactionDetailDtos);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<List<TransactionDto>>> GetAllAsync()
        {
            ResponseDto<List<TransactionDto>> response = new ResponseDto<List<TransactionDto>>();

            try
            {
                TransactionDto transactions = new TransactionDto();
                List<TransactionDto> listTransactions = new List<TransactionDto>();

                var data = await _genericRepository.GetAllAsync();

                foreach (var item in data)
                {
                    var transactionDetail = await _genericTransactionDetailRepository.GetAll(x => x.TransactionId ==  item.Id);
                    transactions = _mapper.Map<TransactionDto>(item);
                    List<TransactionDetailDto> transactionDetailDtos = new List<TransactionDetailDto>();

                    foreach (var detail in transactionDetail)
                    {
                        var transactionDetailDto = _mapper.Map<TransactionDetailDto>(detail);
                        var price = await _genericProductRepository.GetAsync(detail.ProductId);
                        transactionDetailDto.UnitValue = price.Price;
                        transactionDetailDto.Total = transactionDetailDto.Quantity * transactionDetailDto.UnitValue;
                        transactionDetailDto.ProductDescription = price.Description;
                        transactionDetailDtos.Add(transactionDetailDto);
                    }
                    transactions.TransactionDetailDtos!.AddRange(transactionDetailDtos);
                    listTransactions.Add(transactions);
                }
                response.Data = listTransactions;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<TransactionDto>> GetAsync(int id)
        {
            ResponseDto<TransactionDto> response = new ResponseDto<TransactionDto>();

            try
            {
                TransactionDto result = new TransactionDto();
                List<TransactionDetailDto> transactionDetailDtos = new List<TransactionDetailDto>();

                var data = await _genericRepository.GetAsync(id);
                var transactionDetail = await _genericTransactionDetailRepository.GetAll(x => x.TransactionId == id);

                foreach (var item in transactionDetail)
                {
                    var transactionDetailDto = _mapper.Map<TransactionDetailDto>(item);
                    var price = await _genericProductRepository.GetAsync(item.ProductId);
                    transactionDetailDto.UnitValue = price.Price;
                    transactionDetailDto.Total = transactionDetailDto.Quantity * transactionDetailDto.UnitValue;
                    transactionDetailDto.ProductDescription = price.Description;
                    result = _mapper.Map<TransactionDto>(data);
                    transactionDetailDtos.Add(transactionDetailDto);
                }
                result.TransactionDetailDtos!.AddRange(transactionDetailDtos);
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
