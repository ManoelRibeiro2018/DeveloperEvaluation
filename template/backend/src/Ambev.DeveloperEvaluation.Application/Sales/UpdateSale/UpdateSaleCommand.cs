using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<ResultResponse<UpdateSaleResult>>
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public List<UpdateSaleItemRequest> SaleItens { get; set; } = [];
        public bool IsCanceled { get; set; }
    }
}