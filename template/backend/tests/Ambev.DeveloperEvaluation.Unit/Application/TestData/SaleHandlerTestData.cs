using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Util;
using SaleEntity = Ambev.DeveloperEvaluation.Domain.Entities.Sale;


namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class SaleHandlerTestData
    {
        public static CreateSaleCommand CreateSaleHandlerCommand = new()
        {
            BranchId = Guid.NewGuid(),
            TotalSaleAmount = 10,
            Date = DateTime.Now,
            Id = Guid.NewGuid(),
            IsCanceled = false,
            Number = DateTime.Now.Millisecond.ToString().GenerateOrderNumber(),
            UserId = Guid.NewGuid(),
            SaleItens =
            [
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Vinho",
                    Quantity = 1,
                    UnitPrice = 10,
                }
            ]
        };

        public static UpdateSaleCommand UpdateSaleHandlerCommand = new()
        {
            BranchId = Guid.NewGuid(),
            TotalSaleAmount = 10,
            Id = Guid.NewGuid(),
            IsCanceled = false,
            SaleItens =
            [
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Beer",
                    Quantity = 14,
                    UnitPrice = 19,
                }
            ]
        };

        public static SaleEntity CreateSale = new()
        {
            BranchId = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            TotalSaleAmount = 10,
            Date = DateTime.Now,
            Id = Guid.NewGuid(),
            IsCanceled = false,
            Number = DateTime.Now.Millisecond.ToString().GenerateOrderNumber(),
            UserId = Guid.NewGuid(),
            SaleItens = new()
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Vinho",
                    Quantity = 1,
                    UnitPrice = 10,
                }
            }
        };

        public static SaleEntity UpdateSale = new()
        {
            BranchId = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            TotalSaleAmount = 10,
            Date = DateTime.Now,
            Id = Guid.NewGuid(),
            IsCanceled = false,
            Number = DateTime.Now.Millisecond.ToString().GenerateOrderNumber(),
            UserId = Guid.NewGuid(),
            SaleItens = new()
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Vinho",
                    Quantity = 1,
                    UnitPrice = 10,
                }
            }
        };

    }
}
