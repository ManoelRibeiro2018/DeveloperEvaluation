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

        /// <summary>
        /// Initializes a new instance of GetSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetSaleCommand request
        /// </summary>
        /// <param name="command">The GetSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>All sales</returns>
        public async Task<ResultResponse<List<GetSaleResult>>> Handle(GetSaleCommand command, CancellationToken cancellationToken)
        {
            var result = await _saleRepository.GetAllAsync(cancellationToken);
            var sales = _mapper.Map<List<GetSaleResult>>(result);

            return ResultResponse<List<GetSaleResult>>.Successful(sales, 200, "Get all executed successfully");
        }
    }
}