using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ResultResponse<UpdateProductResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductHandler> _logger;
        private readonly UpdateProductValidator _validator;

        public UpdateProductHandler(IProductRepository productRepository,
            IMapper mapper,
            ILogger<UpdateProductHandler> logger,
            UpdateProductValidator validator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ResultResponse<UpdateProductResult>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return ResultResponse<UpdateProductResult>.Failure(400, validationResult.Errors);


            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
            {
                _logger.LogError("Product with Id: {ProductId} not found", request.Id);
                return ResultResponse<UpdateProductResult>.Failure(404, $"Product with Id: {request.Id} not found");
            }

            product.Update(
                request.Title,
                request.Price,
                request.Description,
                request.Category,
                request.Image,
                request.Rating);

            var updatedProduct = await _productRepository.UpdateAsync(product, cancellationToken);

            _logger.LogInformation("Product updated successfully");

            return ResultResponse<UpdateProductResult>.Successful(new UpdateProductResult(updatedProduct.Id), 204, "Product updated successfully");
        }
    }
}
