namespace Ambev.DeveloperEvaluation.Domain.Dtos
{
    public class CreateSaleItemRequest
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
