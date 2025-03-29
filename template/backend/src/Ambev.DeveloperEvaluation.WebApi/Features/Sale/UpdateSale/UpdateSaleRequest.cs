using Ambev.DeveloperEvaluation.Domain.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.UpdateSale
{
    public class UpdateSaleRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public List<CreateSaleItemRequest> SaleItemDtos { get; set; } = [];
        public bool IsCanceled { get; set; }
    }
}
