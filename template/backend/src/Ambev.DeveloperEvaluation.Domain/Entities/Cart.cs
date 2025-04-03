using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartItem> Products { get; set; } = [];

        public void Update(Guid userId, DateTime date, List<CartItem> products)
        {
            UserId = userId;
            Date = date;
            Products = products;
        }
    }
}
