using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Mappers;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.Common.Resource;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;
using System.Net;

namespace MS.RoadFire.Application.Services
{
    public class UserServices : IUserServices
    {
        #region Internals
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IEmployeeServices _employeeServices;
        private readonly IRoleServices _roleServices;
        #endregion

        #region Constructor
        public UserServices(IGenericRepository<User> genericRepository, IEmployeeServices employeeServices, IRoleServices roleServices)
        {
            _genericRepository = genericRepository;
            _employeeServices = employeeServices;
            _roleServices = roleServices;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<UserDto>> AddAsync(UserDto model)
        {
            ResponseDto<UserDto> response = new ResponseDto<UserDto>();

            try
            {
                var request = UserMapper.Map(model);
                request.CreatedAt = DateTime.Now;
                request.UpdatedAt = DateTime.Now;
                var validEmployee = await _employeeServices.GetAsync(model.EmployeeId);
                var validRol = await _roleServices.GetAsync(model.RoleId);

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

                var result = await _genericRepository.AddAsync(request);
                model.RoleName = validRol.Data.Name;
                response.Data = request.Map();
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
            ResponseDto<List<UserDto>> response = new ResponseDto<List<UserDto>>();

            try
            {
                var result = await _genericRepository.GetAllAsync();
                response.Data = result.Select(x => x.Map()).ToList();
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
            ResponseDto<UserDto> response = new ResponseDto<UserDto>();

            try
            {
                var user = await _genericRepository.GetAsync(id);

                if (user != null)
                    response.Data = UserMapper.Map(user);
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
            ResponseDto<UserDto> response = new ResponseDto<UserDto>();

            try
            {
                var validEmployee = await _employeeServices.GetAsync(model.EmployeeId);
                var validRol = await _roleServices.GetAsync(model.RoleId);
                var user = await _genericRepository.GetAsync(model.Id);
                user.CreatedAt = user.CreatedAt;
                user.UpdatedAt = DateTime.Now;
                user.Password = model.Password;

                if (!validEmployee.Data!.IsActive && validEmployee.Data != null)
                    user.State = false;

                var validate = await ValidData(user, model);

                if (!validate.Item1)
                {
                    response.Code = HttpStatusCode.BadRequest;
                    response.Messages = validate.Item2;
                    return response;
                }

                var result = await _genericRepository.UpdateAsync(user);
                response.Data = user.Map();
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
        private async Task<(bool, string)> ValidData(User user, UserDto model)
        {
            await Task.CompletedTask;
            if (user.Username != model.Username)
                return (false, "El usuario no se puede modificar");
            else if (user.EmployeeId != model.EmployeeId)
                return (false, "El empleado no se puede modificar");
            else if (user.RoleId != model.RoleId)
                return (false, "El rol no se puede modificar");
            else
                return (true, string.Empty);
        }
        #endregion
    }
}
