using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommand : IRequest<ResultResponse<UpdateCartResult>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartItem> Products { get; set; } = [];

        public UpdateCartCommand(Guid id, Guid userId, DateTime date, List<CartItem> products)
        {
            Id = id;
            UserId = userId;
            Date = date;
            Products = products;
        }
    }
}
