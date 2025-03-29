namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid SaleId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public void ApplyDiscount()
        {
            if (Quantity >= 10) Discount = 0.20m;
            else if (Quantity >= 4) Discount = 0.10m;
            else Discount = 0.00m;

            TotalPrice = Quantity * UnitPrice * (1 - Discount);
        }
    }
}
