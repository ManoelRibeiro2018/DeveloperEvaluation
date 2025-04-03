using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCart
{
    public class GetAllCartsHandler : IRequestHandler<GetAllCartsCommand, ResultResponse<GetAllCartsResult>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCartsHandler> _logger;

        public GetAllCartsHandler(ICartRepository cartRepository,
            IMapper mapper,
            ILogger<GetAllCartsHandler> logger)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultResponse<GetAllCartsResult>> Handle(GetAllCartsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting get all carts");

            var carts = await _cartRepository.GetAllAsync(request.PageNumber, request.PageSize, request.Order, cancellationToken);

            var result = _mapper.Map<GetAllCartsResult>(carts);

            return ResultResponse<GetAllCartsResult>.Successful(result, 200, "Retrieved successfuly");           
        }
    }
}
