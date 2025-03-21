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
        public List<SaleITem> Products { get; set; } = [];
        public bool IsCanceled { get; set; }
    }
}
