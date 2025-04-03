using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, ResultResponse<CreateCartResult>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCartHandler> _logger;
        private readonly CreateCartValidator _validator;

        public CreateCartHandler(ICartRepository cartRepository,
            IMapper mapper,
            ILogger<CreateCartHandler> logger,
            CreateCartValidator validator)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ResultResponse<CreateCartResult>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting create cart");

            var validations = await _validator.ValidateAsync(request, cancellationToken);

            if (!validations.IsValid) return ResultResponse<CreateCartResult>.Failure(400, validations.Errors);

            var cart = _mapper.Map<Cart>(request);

            var result = await _cartRepository.CreateAsync(cart, cancellationToken);

            _logger.LogInformation("Cart created successfuly");

            var carResult = _mapper.Map<CreateCartResult>(result);

            return ResultResponse<CreateCartResult>.Successful(carResult, 201, "Cart created successfuly");
        }
    }
}
