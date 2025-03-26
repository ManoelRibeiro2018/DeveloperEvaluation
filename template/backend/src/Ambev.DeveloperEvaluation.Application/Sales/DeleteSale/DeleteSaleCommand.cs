using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleCommand : IRequest<ResultResponse<DeleteSaleResponse>>
    {
        public Guid Id { get; }
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
