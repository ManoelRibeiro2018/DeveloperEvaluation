using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ResultResponse<CreateProductResult>>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductHandler> _logger;
        private readonly CreateProductValidator _validator;
        public CreateProductHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductHandler> logger, CreateProductValidator validator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ResultResponse<CreateProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return ResultResponse<CreateProductResult>.Failure(400, validationResult.Errors);

            var product = _mapper.Map<Product>(request);
            var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
            var productResult = _mapper.Map<CreateProductResult>(createdProduct);
            return ResultResponse<CreateProductResult>.Successful(productResult, 201, "Product created successfuly");
        }
    }
}
