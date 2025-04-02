using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductCommand : IRequest<ResultResponse<GetProductResult>>
    {
        public Guid Id { get; set; }
    }
}
