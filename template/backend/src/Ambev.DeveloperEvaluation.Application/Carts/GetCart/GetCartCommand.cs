using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartCommand : IRequest<ResultResponse<GetCartResult>>
    {
        public Guid Id { get; set; }

        public GetCartCommand(Guid id)
        {
            Id = id;
        }
    }
}
