using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllCategories
{
    public class GetAllCategoriesCommand : IRequest<ResultResponse<GetAllCategoriesResult>>
    {
    }
}
