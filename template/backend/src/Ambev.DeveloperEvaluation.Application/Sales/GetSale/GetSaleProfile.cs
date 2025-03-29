using AutoMapper;
using Sale = Ambev.DeveloperEvaluation.Domain.Entities.Sale;
namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<Sale, GetSaleResult>().ReverseMap();
        }
    }
}
