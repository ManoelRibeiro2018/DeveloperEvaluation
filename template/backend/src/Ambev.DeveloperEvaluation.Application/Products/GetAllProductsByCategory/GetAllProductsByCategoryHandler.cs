using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory
{
    public class GetAllProductsByCategoryHandler : IRequestHandler<GetAllProductsByCategoryCommand, ResultResponse<GetAllProductsByCategoryResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductsByCategoryHandler> _logger;
        private readonly GetAllPRoductsByCategoryValidator _validator;

        public GetAllProductsByCategoryHandler(IProductRepository productRepository,
            IMapper mapper,
            ILogger<GetAllProductsByCategoryHandler> logger,
            GetAllPRoductsByCategoryValidator validator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ResultResponse<GetAllProductsByCategoryResult>> Handle(GetAllProductsByCategoryCommand request, CancellationToken cancellationToken)
        {

            var validations = await _validator.ValidateAsync(request, cancellationToken);

            if (!validations.IsValid) return ResultResponse<GetAllProductsByCategoryResult>.Failure(400, validations.Errors);
        
            var result = await _productRepository.GetAllProductsByCategoryAsync(request.Category, request.Order, request.PageNumber, request.PageSize,
              cancellationToken);

            _logger.LogInformation("Products retrieved successfully");

            var products = _mapper.Map<GetAllProductsByCategoryResult>(result);

            return ResultResponse<GetAllProductsByCategoryResult>.Successful(products, 200, "Products retrieved successfully");
        }
    }
}
