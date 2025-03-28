namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class CreateSaleResponse
    {
        public Guid Id { get; set; }        
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public string Branch { get; set; }
        public List<CreateProductSaleResponse> Products { get; set; } = [];
        public bool IsCanceled { get; set; }
    }
}
