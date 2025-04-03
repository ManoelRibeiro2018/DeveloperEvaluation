using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, ResultResponse<DeleteCartResult>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<DeleteCartHandler> _logger;
        private readonly DeleteCartValidator _validator;

        public DeleteCartHandler(ICartRepository cartRepository,
            ILogger<DeleteCartHandler> logger,
            DeleteCartValidator validator)
        {
            _cartRepository = cartRepository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ResultResponse<DeleteCartResult>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting delete cart");

            var validations = await _validator.ValidateAsync(request, cancellationToken);
            if (!validations.IsValid) return ResultResponse<DeleteCartResult>.Failure(400, validations.Errors);

            var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
            if (cart == null)
            {
                _logger.LogError("Cart not found - ID: {ID}", request.Id);
                return ResultResponse<DeleteCartResult>.Failure(404, "Cart not found");
            }

            await _cartRepository.DeleteAsync(cart, cancellationToken);

            _logger.LogInformation("Cart deleted successfuly");

            return ResultResponse<DeleteCartResult>.Successful(new DeleteCartResult("Cart deleted successfuly"), 200, "Cart deleted successfuly");
        }
    }
}
