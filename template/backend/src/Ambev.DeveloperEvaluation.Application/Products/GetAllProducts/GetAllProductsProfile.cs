using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Util;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    public class GetAllProductsProfile : Profile
    {
        public GetAllProductsProfile()
        {
            CreateMap<GetAllProductsResult, ListEntity<Product>>().ForPath(x => x.Itens,
                           o =>
                               o.MapFrom(s => s.Products.Itens))
                       .ForPath(x => x.TotalCount,
                           o =>
                               o.MapFrom(s => s.Products.TotalCount))
                       .ForPath(x => x.PageSize,
                           o =>
                               o.MapFrom(s => s.Products.PageSize))
                       .ForPath(x => x.TotalCount,
                           o =>
                               o.MapFrom(s => s.Products.TotalCount)).ReverseMap();
        }
    }
}
