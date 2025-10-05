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
    public class SupplierService : ISupplierService
    {
        #region Internals
        private readonly IGenericRepository<Supplier> _genericRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public SupplierService(IGenericRepository<Supplier> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<SupplierDto>> AddAsync(SupplierDto model)
        {
            ResponseDto<SupplierDto> response = new ResponseDto<SupplierDto>();

            try
            {
                var request = _mapper.Map<Supplier>(model);
                var result = await _genericRepository.AddAsync(request);
                response.Data = _mapper.Map<SupplierDto>(result);

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

        public async Task<ResponseDto<List<SupplierDto>>> GetAllAsync()
        {
            ResponseDto<List<SupplierDto>> response = new ResponseDto<List<SupplierDto>>();

            try
            {
                var result = await _genericRepository.GetAllAsync();
                response.Data = _mapper.Map<List<SupplierDto>>(result);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<SupplierDto>> GetAsync(int id)
        {
            ResponseDto<SupplierDto> response = new ResponseDto<SupplierDto>();

            try
            {
                var supplier = await _genericRepository.GetAsync(id);

                if (supplier != null)
                    response.Data = _mapper.Map<SupplierDto>(supplier);
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<SupplierDto>> UpdateAsync(SupplierDto model)
        {
            ResponseDto<SupplierDto> response = new ResponseDto<SupplierDto>();

            try
            {
                var request = _mapper.Map<Supplier>(model);
                var result = await _genericRepository.UpdateAsync(request);
                response.Data = _mapper.Map<SupplierDto>(result);
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
