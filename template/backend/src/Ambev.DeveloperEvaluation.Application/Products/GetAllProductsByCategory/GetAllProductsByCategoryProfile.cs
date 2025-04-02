using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Util;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory
{
    public class GetAllProductsByCategoryProfile : Profile
    {
        public GetAllProductsByCategoryProfile()
        {
            CreateMap<GetAllProductsByCategoryResult, ListEntity<Product>>().ForPath(x => x.Itens,
                o =>
                    o.MapFrom(s => s.ListEntity.Itens))
            .ForPath(x => x.TotalCount,
                o =>
                    o.MapFrom(s => s.ListEntity.TotalCount))
            .ForPath(x => x.PageSize,
                o =>
                    o.MapFrom(s => s.ListEntity.PageSize))
            .ForPath(x => x.TotalCount,
                o =>
                    o.MapFrom(s => s.ListEntity.TotalCount)).ReverseMap();
        }
    }
}