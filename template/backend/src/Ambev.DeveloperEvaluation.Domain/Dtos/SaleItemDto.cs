namespace Ambev.DeveloperEvaluation.Domain.Dtos
{
    public class SaleItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
