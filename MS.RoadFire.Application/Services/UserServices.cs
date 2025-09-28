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

                var result = await _genericRepository.AddAsync(request);
                model.RoleName = validRol.Data.Name;
                response.Data = model;
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
                var request = UserMapper.Map(model);
                var result = await _genericRepository.UpdateAsync(request);
                response.Data = model;
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
