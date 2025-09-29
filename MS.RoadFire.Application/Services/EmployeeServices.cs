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
    public class EmployeeServices : IEmployeeServices
    {
        #region Internals
        private readonly IGenericRepository<Employee> _genericRepository;
        private readonly IGenericRepository<User> _genericUserRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public EmployeeServices(IGenericRepository<Employee> genericRepository, IGenericRepository<User> genericUserRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _genericUserRepository = genericUserRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<EmployeeDto>> AddAsync(EmployeeDto model)
        {
            ResponseDto<EmployeeDto> response = new ResponseDto<EmployeeDto>();

            try
            {
                var valid = await IsFieldLengthValid(model);
                var email = EmailValidator.IsValidEmail(model.Email);

                if (valid.Item1 && email.Item1)
                {
                    var request = _mapper.Map<Employee>(model);
                    var result = await _genericRepository.AddAsync(request);
                    response.Data = _mapper.Map<EmployeeDto>(result);
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

        public async Task<ResponseDto<List<EmployeeDto>>> GetAllAsync()
        {
            ResponseDto<List<EmployeeDto>> response = new ResponseDto<List<EmployeeDto>>();

            try
            {
                var result = await _genericRepository.GetAllAsync();
                response.Data = _mapper.Map<List<EmployeeDto>>(result);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<EmployeeDto>> GetAsync(int id)
        {
            ResponseDto<EmployeeDto> response = new ResponseDto<EmployeeDto>();

            try
            {
                var employee = await _genericRepository.GetAsync(id);

                if (employee != null)
                    response.Data = _mapper.Map<EmployeeDto>(employee);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<EmployeeDto>> UpdateAsync(EmployeeDto model)
        {
            ResponseDto<EmployeeDto> response = new ResponseDto<EmployeeDto>();

            try
            {
                var valid = await IsFieldLengthValid(model);
                var email = EmailValidator.IsValidEmail(model.Email);

                if (valid.Item1 && email.Item1)
                {
                    var request = _mapper.Map<Employee>(model);
                    var result = await _genericRepository.UpdateAsync(request);

                    if (!request.IsActive)
                    {
                        var listUser = await _genericUserRepository.GetAllAsync();
                        var user = listUser.Where(x => x.EmployeeId == request.Id).FirstOrDefault();
                        user!.State = false;
                        _ = await _genericUserRepository.UpdateAsync(user);
                    }

                    response.Data = _mapper.Map<EmployeeDto>(result);
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
        private async Task<(bool, string)> IsFieldLengthValid(EmployeeDto model)
        {
            await Task.CompletedTask;
            if (model.Phone.Length > 10)
                return (false, "El número telefónico debe ser máximo de 10 caracteres");
            else if (model.Mobile.Length != 10)
                return (false, "El número celular debe ser máximo y mínimo de 10 caracteres");
            else if (model.Email.Length > 100)
                return (false, "El email debe ser máximo de 100 caracteres");
            else if (model.SecondName.Length > 50)
                return (false, "El segundo nombre debe ser máximo de 50 caracteres");
            else if (model.SecondSurname.Length > 50)
                return (false, "El segundo apellido debe ser máximo de 50 caracteres");
            else
                return (true, string.Empty);
        }
        #endregion
    }
}
