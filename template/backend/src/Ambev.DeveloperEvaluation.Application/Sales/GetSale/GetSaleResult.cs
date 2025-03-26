using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleResult
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Customer { get; set; } = string.Empty;
        public decimal TotalSaleAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<SaleItem> SaleItems { get; set; } = [];
        public bool IsCanceled { get; set; }
    }

}