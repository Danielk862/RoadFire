using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Business.Mappers;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;
using System.Net;

namespace MS.RoadFire.Application.Services
{
    public class ProductMovementService : IProductMovementService
    {
        #region Internals
        private readonly IGenericRepository<Product> _genericProduct;
        private readonly IGenericRepository<PurchaseDetails> _genericPurchase;
        private readonly IGenericRepository<SaleDetails> _genericSale;
        private readonly IGenericRepository<TransactionDetail> _genericTransaction;
        #endregion

        #region Constructor
        public ProductMovementService(IGenericRepository<Product> genericProduct, IGenericRepository<PurchaseDetails> genericPurchase,
            IGenericRepository<SaleDetails> genericSale, IGenericRepository<TransactionDetail> genericTransaction)
        {
            _genericProduct = genericProduct;
            _genericPurchase = genericPurchase;
            _genericSale = genericSale;
            _genericTransaction = genericTransaction;
        }
        #endregion

        #region Methods
        public async Task<ResponseDto<List<ProductMovement>>> GetAll(int productId)
        {
            ResponseDto<List<ProductMovement>> response = new ResponseDto<List<ProductMovement>>();

            try
            {
                List<ProductMovement> productMovements = new List<ProductMovement>();
                var listPurchaseDetails = await _genericPurchase.GetAllInclude(x => x.ProductId == productId, x => x.Purchase!);
                var listSalesDetails = await _genericSale.GetAllInclude(x => x.ProductId == productId, x => x.Sale!);
                var listTransactionDetail = await _genericTransaction.GetAllInclude(x => x.ProductId == productId, x => x.Transaction!);
                var product = await _genericProduct.GetAsync(productId);

                productMovements.AddRange(listPurchaseDetails.Select(x => x.Map()).ToList());
                productMovements.AddRange(listSalesDetails.Select(x => x.Map()).ToList());
                productMovements.AddRange(listTransactionDetail.Select(x => x.Map()).ToList());

                foreach (var item in productMovements)
                {
                    item.Description = product.Description;
                }

                response.Data = productMovements.OrderBy(x => x.Date).ToList();
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
