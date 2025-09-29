using AutoMapper;
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
    public class SecurityServices : ISecurityServices
    {
        #region Internals
        private readonly ISecurityRepository _securityRepository;
        private readonly IGenericServices<Role, RoleDto> _genericServices;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public SecurityServices(ISecurityRepository securityRepository, IGenericServices<Role, RoleDto> genericServices, IMapper mapper)
        {
            _securityRepository = securityRepository;
            _genericServices = genericServices;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<UserDto>> Login(string username, string password)
        {
            ResponseDto<UserDto> response = new ResponseDto<UserDto>();

            try
            {
                var login = await _securityRepository.Login(username, password);

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
