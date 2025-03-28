using Ambev.DeveloperEvaluation.Domain.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class CreateSaleRequest
    {
        public string Customer { get; set; } 
        public string Branch { get; set; }
        public List<SaleItemDto> SaleItemDtos { get; set; } = [];
    }
}
