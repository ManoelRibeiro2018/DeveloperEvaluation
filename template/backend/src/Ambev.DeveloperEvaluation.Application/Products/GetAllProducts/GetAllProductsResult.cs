using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Util;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    public class GetAllProductsResult
    {
        public ListEntity<Product> Products { get; set; }
    }
}
