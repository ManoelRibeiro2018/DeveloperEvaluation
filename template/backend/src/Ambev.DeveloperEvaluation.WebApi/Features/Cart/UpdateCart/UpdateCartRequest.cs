using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.UpdateCart
{
    public class UpdateCartRequest
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartItem> Products { get; set; } = [];
    }
}
