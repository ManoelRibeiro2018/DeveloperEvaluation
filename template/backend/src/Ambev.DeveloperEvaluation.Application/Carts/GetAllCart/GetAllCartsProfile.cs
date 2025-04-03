using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Util;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCart
{
    public class GetAllCartsProfile : Profile
    {
        public GetAllCartsProfile()
        {
            CreateMap<GetAllCartsResult, ListEntity<Cart>>().ForPath(x => x.Itens,
               o =>
                   o.MapFrom(s => s.Carts.Itens))
           .ForPath(x => x.TotalCount,
               o =>
                   o.MapFrom(s => s.Carts.TotalCount))
           .ForPath(x => x.PageSize,
               o =>
                   o.MapFrom(s => s.Carts.PageSize))
           .ForPath(x => x.TotalCount,
               o =>
                   o.MapFrom(s => s.Carts.TotalCount)).ReverseMap();
        }
    }
}
