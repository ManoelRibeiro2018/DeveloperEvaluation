namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<SaleItem> SaleItens { get; set; } = [];
        public bool IsCanceled { get; set; }

        public void CalculateTotalAmount()
        {
            TotalSaleAmount = SaleItens.Sum(e => e.TotalPrice);
        }
    }
}
