namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.GetAllProduct
{
    public class GetAllProductsRequest
    {
        public string? Order { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllProductsRequest(string? order, int? pageNumber, int? pageSize)
        {
            Order = order;
            PageNumber = pageNumber ?? 1;
            PageSize = pageSize ?? 10;
        }
    }
}
