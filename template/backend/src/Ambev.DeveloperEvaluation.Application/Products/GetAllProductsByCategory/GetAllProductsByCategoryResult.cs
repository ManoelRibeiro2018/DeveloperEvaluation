using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Util;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory
{
    public class GetAllProductsByCategoryResult
    {
        public ListEntity<Product> ListEntity { get; set; }
    }
}
