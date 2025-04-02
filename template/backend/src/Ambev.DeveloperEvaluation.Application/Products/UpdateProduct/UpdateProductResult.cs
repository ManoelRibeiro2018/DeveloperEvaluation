namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductResult
    {
        public Guid Id { get; set; }

        public UpdateProductResult(Guid id)
        {
            Id = id;
        }
    }
}
