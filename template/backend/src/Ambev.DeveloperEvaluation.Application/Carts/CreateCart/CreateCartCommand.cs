using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartCommand : IRequest<ResultResponse<CreateCartResult>>
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CartItem> Products { get; set; } = [];
    }
}
