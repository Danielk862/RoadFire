using AutoMapper;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.Common.Resource;
using MS.RoadFire.DataAccess.Contracts.Interfaces;
using System;
using System.Net;

namespace MS.RoadFire.Application.Services
{
    public class GenericServices<TEntity, TDto> : IGenericServices<TEntity, TDto> where TEntity : class where TDto : class
    {
        #region Internals
        private readonly IGenericRepository<TEntity> _genericRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public GenericServices(IGenericRepository<TEntity> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<TDto>> AddAsync(TDto model)
        {
            ResponseDto<TDto> response = new ResponseDto<TDto>();

            try
            {
                var request = _mapper.Map<TEntity>(model);
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
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<List<TDto>>> GetAllAsync()
        {
            ResponseDto<List<TDto>> response = new ResponseDto<List<TDto>>();

            try
            {
                var result = await _genericRepository.GetAllAsync();
                response.Data = _mapper.Map<List<TDto>>(result);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<TDto>> GetAsync(int id)
        {
            ResponseDto<TDto> response = new ResponseDto<TDto>();

            try
            {
                var data = await _genericRepository.GetAsync(id);

                if (data != null)
                    response.Data = _mapper.Map<TDto>(data);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<List<TEntity>>> GetPaginationAsync(PaginationDTO paginationDTO) 
        {
            ResponseDto<List<TEntity>> response = new ResponseDto<List<TEntity>>();

            try
            {
                var págination = await _genericRepository.GetPaginationAsync(paginationDTO);
                response.Data = págination.ToList();
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
                var totalData = await _genericRepository.GetTotalRecordsAsync(paginationDTO);
                response.Data = totalData;
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<TDto>> UpdateAsync(TDto model)
        {
            ResponseDto<TDto> response = new ResponseDto<TDto>();

            try
            {
                var request = _mapper.Map<TEntity>(model);
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
