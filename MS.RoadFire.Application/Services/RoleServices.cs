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
    public class RoleServices : IRoleServices
    {
        #region Internals
        private readonly IGenericRepository<Role> _genericRepository;
        #endregion

        #region Constructor
        public RoleServices(IGenericRepository<Role> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<RoleDto>> AddAsync(RoleDto model)
        {
            ResponseDto<RoleDto> response = new ResponseDto<RoleDto>();

            try
            {
                var request = RoleMapper.Map(model);
                var result = await _genericRepository.AddAsync(request);
                
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

        public async Task<ResponseDto<List<RoleDto>>> GetAllAsync()
        {
            ResponseDto<List<RoleDto>> response = new ResponseDto<List<RoleDto>>();

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

        public async Task<ResponseDto<RoleDto>> GetAsync(int id)
        {
            ResponseDto<RoleDto> response = new ResponseDto<RoleDto>();

            try
            {
                var rol = await _genericRepository.GetAsync(id);

                if (rol != null)
                    response.Data = RoleMapper.Map(rol);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<RoleDto>> UpdateAsync(RoleDto model)
        {
            ResponseDto<RoleDto> response = new ResponseDto<RoleDto>();

            try
            {
                var request = RoleMapper.Map(model);
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
