using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCart
{
    public class GetAllCartsCommand : IRequest<ResultResponse<GetAllCartsResult>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? Order { get; set; }

        public GetAllCartsCommand(int? pageSize, int? pageNumber, string? order)
        {
            PageSize = pageSize ?? 10;
            PageNumber = pageNumber ?? 1;
            Order = order;
        }
    }
}
