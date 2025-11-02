using AutoMapper;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.DataAccess.Contracts.Interfaces;
using System.Net;

namespace MS.RoadFire.Application.Services
{
    public class RoleService : IRoleService
    {
        #region Internals
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task<ResponseDto<List<RoleDto>>> GetPaginationAsync(PaginationDTO paginationDTO)
        {
            ResponseDto<List<RoleDto>> response = new ResponseDto<List<RoleDto>>();

            try
            {
                var request = await _roleRepository.GetPaginationAsync(paginationDTO);
                response.Data = _mapper.Map<List<RoleDto>>(request);
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
                var request = await _roleRepository.GetTotalRecordsAsync(paginationDTO);
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
