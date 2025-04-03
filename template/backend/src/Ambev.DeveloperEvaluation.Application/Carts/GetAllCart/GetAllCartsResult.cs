using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Util;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCart
{
    public class GetAllCartsResult
    {
        public ListEntity<Cart> Carts { get; set; }
    }
}
