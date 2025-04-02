using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory
{
    public class GetAllProductsByCategoryCommand : IRequest<ResultResponse<GetAllProductsByCategoryResult>>
    {
        public string Category { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Order { get; set; }
    }
}
