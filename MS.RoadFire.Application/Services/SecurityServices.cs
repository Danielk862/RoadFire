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
    public class SecurityServices : ISecurityServices
    {
        #region Internals
        private readonly IGenericServices<Role, RoleDto> _genericServices;
        private readonly IGenericRepository<User> _genericUser;
        private readonly IGenericRepository<Employee> _genericEmployee;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public SecurityServices(IGenericServices<Role, RoleDto> genericServices,
            IGenericRepository<User> genericUser, IGenericRepository<Employee> genericEmployee, IMapper mapper)
        {
            _genericServices = genericServices;
            _genericUser = genericUser;
            _genericEmployee = genericEmployee;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<UserDto>> Login(string username, string password)
        {
            ResponseDto<UserDto> response = new ResponseDto<UserDto>();

            try
            {
                var login = await _genericUser.Get(x => x.Username == username && x.Password == password && x.State);

                if (login == null)
                {
                    response.Code = HttpStatusCode.BadRequest;
                    response.Messages = MessagesResource.IncorrectLogin;
                    return response;
                }
                var userLogin = _mapper.Map<UserDto>(login);
                var rol = await _genericServices.GetAsync(login.RoleId);
                var empleyoee = await _genericEmployee.Get(x => x.Id == userLogin.EmployeeId);
                userLogin.RoleName = rol.Data!.Name;
                userLogin.EmployeeName = $"{empleyoee.FirtsName} {empleyoee.Surname}";
                response.Data = userLogin;
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
