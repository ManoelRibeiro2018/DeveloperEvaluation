namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    public class DeleteProductResult
    {
        public bool Success { get; set; }

        public DeleteProductResult(bool success)
        {
            Success = success;
        }
    }
}
