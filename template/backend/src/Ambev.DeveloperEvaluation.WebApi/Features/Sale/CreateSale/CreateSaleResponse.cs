using Ambev.DeveloperEvaluation.WebApi.Features.SaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class CreateSaleResponse
    {
        public Guid Id { get; set; }        
        public DateTime Date { get; set; }
        public Guid UserID { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public Guid BranchId { get; set; }
        public List<CreateSaleItemResponse> SaleItens { get; set; } = [];
        public bool IsCanceled { get; set; }
    }
}
