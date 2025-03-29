using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleResult
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<SaleItem> SaleItens { get; set; } = [];
        public bool IsCanceled { get; set; }
    }
}