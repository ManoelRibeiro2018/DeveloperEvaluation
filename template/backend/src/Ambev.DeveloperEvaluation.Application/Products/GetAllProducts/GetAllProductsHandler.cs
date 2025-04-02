using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, ResultResponse<GetAllProductsResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductsHandler> _logger;

        public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetAllProductsHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultResponse<GetAllProductsResult>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {

            var produts = await _productRepository.GetAllAsync(request.Order, request.PageNumber, request.PageSize,
                 cancellationToken: cancellationToken);

            _logger.LogInformation("Products retrieved successfully");

            var result = _mapper.Map<GetAllProductsResult>(produts);
            return ResultResponse<GetAllProductsResult>.Successful(result, 200, "Products retrieved successfully");
        }
    }
}
