﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItem
{
    public class CreateSaleItemResponse
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
