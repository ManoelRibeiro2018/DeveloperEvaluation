using Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.GetAllProductsByCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.GetAllCategory
{
    public class GetAllProductsByCategoryProfile : Profile
    {
        public GetAllProductsByCategoryProfile()
        {
            CreateMap<GetAllProductsCategoryRequest, GetAllProductsByCategoryCommand>();
        }
    }
}
