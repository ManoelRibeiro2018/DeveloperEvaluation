using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, ResultResponse<UpdateCartResult>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<UpdateCartHandler> _logger;
        private readonly IMapper _mapper;
        private readonly UpdateCartValidator _validator;

        public UpdateCartHandler(ICartRepository cartRepository,
            ILogger<UpdateCartHandler> logger,
            IMapper mapper,
            UpdateCartValidator validator)
        {
            _cartRepository = cartRepository;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ResultResponse<UpdateCartResult>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting process update");

            var validations = await _validator.ValidateAsync(request, cancellationToken);
            if (!validations.IsValid) return ResultResponse<UpdateCartResult>.Failure(400, validations.Errors);


            var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
            if (cart == null)
            {
                _logger.LogError("Cart not found - ID: {ID}", request.Id);
                return ResultResponse<UpdateCartResult>.Failure(404, "Cart not found");
            }

            cart.Update(request.UserId, request.Date, request.Products);

            var result = await _cartRepository.UpdateAsync(cart, cancellationToken);

            _logger.LogInformation("Cart updated successfuly");

            var cartResult = _mapper.Map<UpdateCartResult>(result);

            return ResultResponse<UpdateCartResult>.Successful(cartResult, 200, "Cart updated successfuly");
        }
    }
}
