namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.GetAllProductsByCategory
{
    public class GetAllProductsCategoryRequest
    {
        public string Category { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Order { get; set; }

        public GetAllProductsCategoryRequest(string category, string? order, int? pageNumber, int? pageSize)
        {
            Category = category;
            PageNumber = pageNumber ?? 1;
            PageSize = pageSize ?? 10;
            Order = order;
        }
    }
}
