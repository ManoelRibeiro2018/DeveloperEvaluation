namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleITem
    {
        public Guid Id { get; set; }
        public Guid SaleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
