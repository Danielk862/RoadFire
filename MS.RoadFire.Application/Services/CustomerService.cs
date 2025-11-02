using AutoMapper;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.Common.Resource;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;
using System.Net;

namespace MS.RoadFire.Application.Services
{
    public class CustomerService : ICustomerService
    {
        #region Internals
        private readonly IGenericRepository<Customer> _genericRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CustomerService(IGenericRepository<Customer> genericRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<CustomerDto>> AddAsync(CustomerDto model)
        {
            ResponseDto<CustomerDto> response = new ResponseDto<CustomerDto>();

            try
            {
                var valid = await IsFieldLengthValid(model);
                var email = EmailValidator.IsValidEmail(model.Email);

                if (valid.Item1 && email.Item1)
                {
                    var request = _mapper.Map<Customer>(model);
                    var result = await _genericRepository.AddAsync(request);
                    response.Data = _mapper.Map<CustomerDto>(result);
                }
                else if (!valid.Item1)
                {
                    response.Messages = valid.Item2;
                    response.Code = HttpStatusCode.BadRequest;
                }
                else
                {
                    response.Messages = email.Item2;
                    response.Code = HttpStatusCode.BadRequest;
                }

            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();

            try
            {
                var result = await _genericRepository.DeleteAsync(id);

                if (!result)
                {

                    response.Code = HttpStatusCode.BadRequest;
                    response.Messages = MessagesResource.NotDeleteData;
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

        public async Task<ResponseDto<List<CustomerDto>>> GetAllAsync()
        {
            ResponseDto<List<CustomerDto>> response = new ResponseDto<List<CustomerDto>>();

            try
            {
                var result = await _genericRepository.GetAllAsync();
                response.Data = _mapper.Map<List<CustomerDto>>(result);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<CustomerDto>> GetAsync(int id)
        {
            ResponseDto<CustomerDto> response = new ResponseDto<CustomerDto>();

            try
            {
                var supplier = await _genericRepository.GetAsync(id);

                if (supplier != null)
                    response.Data = _mapper.Map<CustomerDto>(supplier);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<CustomerDto>> UpdateAsync(CustomerDto model)
        {
            ResponseDto<CustomerDto> response = new ResponseDto<CustomerDto>();

            try
            {
                var valid = await IsFieldLengthValid(model);
                var email = EmailValidator.IsValidEmail(model.Email);

                if (valid.Item1 && email.Item1)
                {
                    var request = _mapper.Map<Customer>(model);
                    var result = await _genericRepository.UpdateAsync(request);
                    response.Data = _mapper.Map<CustomerDto>(result);
                }
                else if (!valid.Item1)
                {
                    response.Messages = valid.Item2;
                    response.Code = HttpStatusCode.BadRequest;
                }
                else
                {
                    response.Messages = email.Item2;
                    response.Code = HttpStatusCode.BadRequest;
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

        #region Private methods
        private async Task<(bool, string)> IsFieldLengthValid(CustomerDto model)
        {
            await Task.CompletedTask;
            if (model.SecondName.Length > 50)
                return (false, "El segundo nombre debe ser máximo de 50 caracteres");
            else if (model.SecondSurname.Length > 50)
                return (false, "El segundo apellido debe ser máximo de 50 caracteres");
            else
                return (true, string.Empty);
        }

        public async Task<ResponseDto<List<CustomerDto>>> GetPaginationAsync(PaginationDTO paginationDTO)
        {
            ResponseDto<List<CustomerDto>> response = new ResponseDto<List<CustomerDto>>();

            try
            {
                var request = await _customerRepository.GetPaginationAsync(paginationDTO);
                response.Data = _mapper.Map<List<CustomerDto>>(request);
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
                var request = await _customerRepository.GetTotalRecordsAsync(paginationDTO);
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
