using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartHandler : IRequestHandler<GetCartCommand, ResultResponse<GetCartResult>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCartHandler> _logger;
        private readonly GetCartValidator _validator;

        public GetCartHandler(ICartRepository cartRepository,
            IMapper mapper,
            ILogger<GetCartHandler> logger,
            GetCartValidator validator)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ResultResponse<GetCartResult>> Handle(GetCartCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting get cart by id");

            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid) return ResultResponse<GetCartResult>.Failure(400, validation.Errors);

            var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);

            var cartResult = _mapper.Map<GetCartResult>(cart);

            return ResultResponse<GetCartResult>.Successful(cartResult, 200, "Retrieved cart successfuly");
        }
    }
}
