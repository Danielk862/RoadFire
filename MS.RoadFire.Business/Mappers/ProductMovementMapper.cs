using MS.RoadFire.Business.Models;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.Business.Mappers
{
    public static class ProductMovementMapper
    {
        public static ProductMovement Map(this PurchaseDetails model) => new ProductMovement
        {
            Date = model.Purchase!.Date,
            ProductId = model.ProductId,
            Quantity = model.Quantity,
            TotalCost = (model.Quantity * model.UnitValue),
            Type = model.Purchase.Type,
            UnitCost = model.UnitValue
        };

        public static ProductMovement Map(this SaleDetails model) => new ProductMovement
        {
            Date = model.Sale!.Date,
            ProductId = model.ProductId,
            Quantity = model.Quantity,
            TotalCost = (model.Quantity * model.UnitValue),
            Type = model.Sale.Type,
            UnitCost = model.UnitValue
        };

        public static ProductMovement Map(this TransactionDetail model) => new ProductMovement
        {
            Date = model.Transaction!.Date,
            ProductId = model.ProductId,
            Quantity = model.Quantity,
            Type = model.Transaction.Type,
            TotalCost = (model.Quantity * model.UnitValue),
            UnitCost = model.UnitValue
        };
    }
}
