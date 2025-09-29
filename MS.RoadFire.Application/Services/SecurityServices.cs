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
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public SecurityServices(IGenericServices<Role, RoleDto> genericServices,
            IGenericRepository<User> genericUser, IMapper mapper)
        {
            _genericServices = genericServices;
            _genericUser = genericUser;
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
                userLogin.RoleName = rol.Data!.Name;
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
