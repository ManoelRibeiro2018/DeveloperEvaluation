using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public DateTime Date { get; set; }
        public List<SaleItem> SaleItens { get; set; } = [];
        public bool IsCanceled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void CalculateTotalAmount()
        {
            TotalSaleAmount = SaleItens.Sum(e => e.TotalPrice);
        }

        public void SetDateTime()
        {
            Date = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).ToUniversalTime();
            CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).ToUniversalTime();
        }

        public void Update(Sale sale)
        {
            sale.Date = DateTime.SpecifyKind(sale.Date, DateTimeKind.Utc).ToUniversalTime();
            sale.CreatedAt = DateTime.SpecifyKind(sale.CreatedAt, DateTimeKind.Utc).ToUniversalTime();
            DateTime data = (DateTime)(sale.UpdatedAt == null ? DateTime.Now : sale.UpdatedAt);
            sale.UpdatedAt = DateTime.SpecifyKind(data, DateTimeKind.Utc).ToUniversalTime();           
        }
    }
}
