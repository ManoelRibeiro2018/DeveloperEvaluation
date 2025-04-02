using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    internal class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ResultResponse<DeleteProductResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductHandler> _logger;
        private readonly DeleteProductValidator _validator;

        public DeleteProductHandler(IProductRepository productRepository, ILogger<DeleteProductHandler> logger, DeleteProductValidator validator)
        {
            _productRepository = productRepository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ResultResponse<DeleteProductResult>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return ResultResponse<DeleteProductResult>.Failure(400, validationResult.Errors);


            var isSuccess = await _productRepository.DeleteAsync(request.Id, cancellationToken);
            if (!isSuccess)
                return ResultResponse<DeleteProductResult>.Failure(404, $"Product with ID {request.Id} not found");
            

            _logger.LogInformation("Product deleted successfully");

            return ResultResponse<DeleteProductResult>.Successful(new DeleteProductResult(isSuccess), 204, "Product deleted successfully");

        }
    }
}
