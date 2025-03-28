using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    internal class GetSaleHandler : IRequestHandler<GetSaleCommand, ResultResponse<List<GetSaleResult>>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<ResultResponse<List<GetSaleResult>>> Handle(GetSaleCommand command, CancellationToken cancellationToken)
        {
            var result = await _saleRepository.GetAllAsync(cancellationToken);
            var sales = _mapper.Map<List<GetSaleResult>>(result);

            return ResultResponse<List<GetSaleResult>>.Successful(sales, 200, "Get all executed successfully");
        }
    }
}