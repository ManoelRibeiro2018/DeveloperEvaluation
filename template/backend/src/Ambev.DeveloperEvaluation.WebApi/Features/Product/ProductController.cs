using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetAllCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.GetAllProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.GetAllProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, IMapper mapper, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] int? pageNumber, [FromQuery] string? order, [FromQuery] int? pageSize, CancellationToken
            cancellationToken)
        {
            var request = new GetAllProductsRequest(order, pageNumber, pageSize);
            var command = _mapper.Map<GetAllProductsCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(result.StatusCode, result.Payload);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var command = new GetProductCommand { Id = id };
            var response = await _mediator.Send(command, cancellationToken);

            return StatusCode(response.StatusCode, response.Payload);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProductAsync(CreateProductRequest request, CancellationToken
            cancellationToken)
        {
            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response.Payload);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductAsync(UpdateProductRequest request,
        CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateProductCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(result.StatusCode, result.Payload);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {

            var command = new DeleteProductCommand(id);
            var response = await _mediator.Send(command, cancellationToken);

            return StatusCode(response.StatusCode, response.Payload);
        }

        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProductsByCategoryAsync([FromRoute] string? category, [FromQuery]
        int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? order, CancellationToken cancellationToken)
        {
            var request = new GetAllProductsCategoryRequest(category, order, pageNumber, pageSize);
            var command = _mapper.Map<GetAllProductsByCategoryCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode(result.StatusCode, result.Payload);
        }

        [HttpGet("categories")]
        [ProducesResponseType(typeof(ApiResponseWithData<IReadOnlyList<string>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var command = new GetAllCategoriesCommand();
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(
                result.StatusCode,
                result.Payload
            );
        }
    }
}
