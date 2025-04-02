using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    public class GetAllProductsCommand : IRequest<ResultResponse<GetAllProductsResult>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? Order { get; set; }
    }
}
