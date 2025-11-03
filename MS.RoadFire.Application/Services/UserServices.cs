using System.Net;
using AutoMapper;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.Common.Resource;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;

namespace MS.RoadFire.Application.Services
{
    public class UserServices : IUserServices
    {
        #region Internals

        private readonly IUserRepository _userRepository;
        private readonly IEmployeeServices _employeeServices;
        private readonly IGenericServices<Role, RoleDto> _genericServices;
        private readonly IMapper _mapper;

        #endregion Internals

        #region Constructor

        public UserServices(
            IUserRepository userRepository,
            IEmployeeServices employeeServices,
            IGenericServices<Role, RoleDto> genericServices,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _employeeServices = employeeServices;
            _genericServices = genericServices;
            _mapper = mapper;
        }

        #endregion Constructor

        #region Methods

        public async Task<ResponseDto<UserDto>> AddAsync(UserDto model)
        {
            ResponseDto<UserDto> response = new();

            try
            {
                var request = _mapper.Map<User>(model);
                request.CreatedAt = DateTime.Now;
                request.UpdatedAt = DateTime.Now;
                request.Role = null;
                request.Employee = null;

                var validEmployee = await _employeeServices.GetAsync(model.EmployeeId);
                var validRol = await _genericServices.GetAsync(model.RoleId);

                if (validEmployee.Data == null)
                {
                    response.Messages = MessagesResource.EmployeeInvalid;
                    response.Code = HttpStatusCode.BadRequest;
                    return response;
                }

                if (validRol.Data == null)
                {
                    response.Messages = MessagesResource.RolInvalid;
                    response.Code = HttpStatusCode.BadRequest;
                    return response;
                }

                if (!validEmployee.Data.IsActive)
                    request.State = false;

                var result = await _userRepository.AddAsync(request);
                var register = _mapper.Map<UserDto>(result);
                register.RoleName = validRol.Data.Name;
                response.Data = register;
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
            ResponseDto<bool> response = new();

            try
            {
                var result = await _userRepository.DeleteAsync(id);

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

        public async Task<ResponseDto<List<UserDto>>> GetAllAsync()
        {
            ResponseDto<List<UserDto>> response = new();

            try
            {
                var result = await _userRepository.GetAllAsync();
                response.Data = _mapper.Map<List<UserDto>>(result);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<UserDto>> GetAsync(int id)
        {
            ResponseDto<UserDto> response = new();

            try
            {
                var user = await _userRepository.GetAsync(id);

                if (user != null)
                    response.Data = _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<UserDto>> UpdateAsync(UserDto model)
        {
            ResponseDto<UserDto> response = new();

            try
            {
                var validEmployee = await _employeeServices.GetAsync(model.EmployeeId);
                var validRol = await _genericServices.GetAsync(model.RoleId);
                var user = await _userRepository.GetAsync(model.Id);

                if (user == null)
                {
                    response.Code = HttpStatusCode.NotFound;
                    response.Messages = "El usuario no fue encontrado.";
                    return response;
                }

                user.UpdatedAt = DateTime.Now;
                user.Password = model.Password;
                user.RoleId = model.RoleId;
                user.State = model.State;

                if (!validEmployee.Data!.IsActive)
                    user.State = false;

                var validate = await ValidData(user, model);

                if (!validate.Item1)
                {
                    response.Code = HttpStatusCode.BadRequest;
                    response.Messages = validate.Item2;
                    return response;
                }

                var result = await _userRepository.UpdateAsync(user);
                var register = _mapper.Map<UserDto>(result);
                register.RoleName = validRol.Data!.Name;
                response.Data = register;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<List<UserDto>>> GetPaginationAsync(PaginationDTO pagination)
        {
            ResponseDto<List<UserDto>> response = new();
            try
            {
                var result = await _userRepository.GetPaginationAsync(pagination);
                response.Data = _mapper.Map<List<UserDto>>(result);
                response.Code = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<int>> GetTotalRecordsAsync(PaginationDTO pagination)
        {
            ResponseDto<int> response = new();
            try
            {
                var result = await _userRepository.GetTotalRecordsAsync(pagination);
                response.Data = result;
                response.Code = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        #endregion Methods

        #region Private methods

        private async Task<(bool, string)> ValidData(User user, UserDto model)
        {
            await Task.CompletedTask;
            if (user.Username != model.Username)
                return (false, "El usuario no se puede modificar");
            else if (user.EmployeeId != model.EmployeeId)
                return (false, "El empleado no se puede modificar");
            else
                return (true, string.Empty);
        }

        #endregion Private methods
    }
}