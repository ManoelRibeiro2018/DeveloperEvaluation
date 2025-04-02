namespace Ambev.DeveloperEvaluation.Application.Products.GetAllCategories
{
    public class GetAllCategoriesResult
    {
        public List<string> Categories { get; set; }

        public GetAllCategoriesResult(List<string> categories)
        {
            Categories = categories;
        }
    }
}
