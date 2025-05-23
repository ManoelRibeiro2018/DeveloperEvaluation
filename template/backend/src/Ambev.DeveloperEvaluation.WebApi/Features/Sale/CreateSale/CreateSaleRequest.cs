﻿using Ambev.DeveloperEvaluation.Domain.Dtos;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class CreateSaleRequest
    {
        public Guid UserID { get; set; } 
        public Guid BranchId { get; set; }
        public List<CreateSaleItemRequest> SaleItens { get; set; } = [];
    }
}
