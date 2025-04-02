using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.GetProduct
{
    public class GetProductProfile : Profile
    {
        public GetProductProfile()
        {
            CreateMap< GetProductResult, GetProductResponse>();
            CreateMap<GetProductRequest, GetProductCommand>();
        }
    }
}
