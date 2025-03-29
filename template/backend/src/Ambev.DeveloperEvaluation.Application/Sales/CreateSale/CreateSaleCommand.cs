using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<ResultResponse<CreateSaleResult>>
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<CreateSaleItemRequest> SaleItens { get; set; } = [];
        public bool IsCanceled { get; set; }
    }
}