namespace Ambev.DeveloperEvaluation.Domain.Dtos
{
    public class UpdateSaleItemRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
