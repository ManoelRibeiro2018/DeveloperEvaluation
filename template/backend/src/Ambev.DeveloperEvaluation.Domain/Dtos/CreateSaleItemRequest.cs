namespace Ambev.DeveloperEvaluation.Domain.Dtos
{
    public class CreateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
