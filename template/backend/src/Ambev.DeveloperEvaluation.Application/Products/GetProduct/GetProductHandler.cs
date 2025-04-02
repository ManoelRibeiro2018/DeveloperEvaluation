using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductCommand, ResultResponse<GetProductResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductHandler> _logger;
        private readonly GetProductValidator _validator;

        public GetProductHandler(IProductRepository productRepository,
            IMapper mapper,
            ILogger<GetProductHandler> logger,
            GetProductValidator validator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ResultResponse<GetProductResult>> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return ResultResponse<GetProductResult>.Failure(400, validationResult.Errors);

            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
            {
                _logger.LogError("Product with Id: {ProductId} not found", request.Id);
                return ResultResponse<GetProductResult>.Failure(404, $"Product with ID {request.Id} not found");
            }

            _logger.LogInformation("Product recovered successfully");

            var productResult = _mapper.Map<GetProductResult>(product);

            return ResultResponse<GetProductResult>.Successful(productResult, 200, "Product recovered successfully");
        }
    }
}
