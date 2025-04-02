using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesCommand, ResultResponse<GetAllCategoriesResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetAllCategoriesHandler> _logger;

        public GetAllCategoriesHandler(IProductRepository productRepository, ILogger<GetAllCategoriesHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ResultResponse<GetAllCategoriesResult>> Handle(GetAllCategoriesCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetAllCategoryAsync(cancellationToken);

            _logger.LogInformation("Products retrieved successfully");

            return ResultResponse<GetAllCategoriesResult>.Successful(new GetAllCategoriesResult(result), 200, "Products retrieved successfully");
        }
    }
}
