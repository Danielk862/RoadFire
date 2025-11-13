using AutoMapper;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.Common.Resource;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;
using System.Net;

namespace MS.RoadFire.Application.Services
{
    public class ProductServices : IProductServices
    {
        #region Internals
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IMapper _mapper; 
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Supplier> _supplierRepository;
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructor
        public ProductServices(IGenericRepository<Product> genericRepository, IMapper mapper,
            IGenericRepository<Category> categoryRepository, IGenericRepository<Supplier> supplierRepository, 
            IProductRepository productRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _productRepository = productRepository;
        }
        #endregion

        #region methods
        public async Task<ResponseDto<ProductDto>> AddAsync(ProductDto model)
        {
            ResponseDto<ProductDto> response = new ResponseDto<ProductDto>();

            try
            {
                var request = _mapper.Map<Product>(model);
                request.RegistrationDate = DateTime.Now;
                var validCategory = await _categoryRepository.GetAsync(model.CategoryId);

                if (validCategory == null)
                {
                    response.Messages = MessagesResource.CategoryInvalid;
                    response.Code = HttpStatusCode.BadRequest;
                    return response;
                }

                if (!validCategory.IsActive)
                    request.IsActive = false;

                request.Category = null;
                request.Supplier = null;
                var result = await _genericRepository.AddAsync(request);
                var register = _mapper.Map<ProductDto>(result);
                register.CategoryName = validCategory.Name;
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

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            ResponseDto<List<ProductDto>> response = new ResponseDto<List<ProductDto>>();

            try
            {
                var result = await _genericRepository.GetAllAsync();
                response.Data = _mapper.Map<List<ProductDto>>(result);

                foreach (var item in response.Data)
                {
                    var category = await _categoryRepository.GetAsync(item.CategoryId);
                    item.CategoryName = category.Name;
                }
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<ProductDto>> GetAsync(int id)
        {
            ResponseDto<ProductDto> response = new ResponseDto<ProductDto>();

            try
            {
                var product = await _genericRepository.GetAsync(id);

                if (product != null)
                {
                    var data = _mapper.Map<ProductDto>(product);
                    var category = await _categoryRepository.GetAsync(data.CategoryId);
                    var supplier = await _supplierRepository.GetAsync(data.SupplierId);
                    data.CategoryName = category.Name;
                    data.SupplierName = supplier.Name;
                    response.Data = data;
                }
            }
            catch (Exception ex)
            {
                response.Code = HttpStatusCode.InternalServerError;
                response.Messages = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<ProductDto>> UpdateAsync(ProductDto model)
        {
            ResponseDto<ProductDto> response = new ResponseDto<ProductDto>();

            try
            {
                var validCategory = await _categoryRepository.GetAsync(model.CategoryId);
                var product = await _genericRepository.GetAsync(model.Id);
                product.Description = model.Description;
                product.Price = model.Price;
                product.CategoryId = model.CategoryId;
                product.IsActive = model.IsActive;
                product.UpdateDate = DateTime.Now;
                product.SupplierId = model.SupplierId;

                var validate = await ValidData(product, model);

                if (!validate.Item1)
                {
                    response.Code = HttpStatusCode.BadRequest;
                    response.Messages = validate.Item2;
                    return response;
                }

                if (!validCategory.IsActive)
                    product.IsActive = false;

                var result = await _genericRepository.UpdateAsync(product);
                var register = _mapper.Map<ProductDto>(result);
                register.CategoryName = validCategory.Name;
                response.Data = register;
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
        private async Task<(bool, string)> ValidData(Product product, ProductDto model)
        {
            await Task.CompletedTask;
            if (product.Sku != model.Sku)
                return (false, "El Sku no se puede modificar");
            else
                return (true, string.Empty);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetPaginationAsync(PaginationDTO paginationDTO)
        {
            ResponseDto<List<ProductDto>> response = new ResponseDto<List<ProductDto>>();

            try
            {
                var request = await _productRepository.GetPaginationAsync(paginationDTO);
                var result = _mapper.Map<List<ProductDto>>(request);

                foreach (var item in result)
                {
                    var category = await _categoryRepository.GetAsync(item.CategoryId);
                    var supplier = await _supplierRepository.GetAsync(item.SupplierId);
                    item.CategoryName = category.Name;
                    item.SupplierName = supplier.Name;
                }

                response.Data = result.OrderBy(x => x.Description).ToList();
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
                var request = await _productRepository.GetTotalRecordsAsync(paginationDTO);
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
