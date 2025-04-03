using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartCommand : IRequest<ResultResponse<DeleteCartResult>>
    {
        public Guid Id { get; set; }

        public DeleteCartCommand(Guid id)
        {
            Id = id;
        }
    }
}
