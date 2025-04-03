using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.UpdateCart;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CartController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCartCommand createCartCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createCartCommand, cancellationToken);

            return StatusCode(result.StatusCode, result.Success ? result.Payload : result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] string? order,
            CancellationToken cancellationToken)
        {
            var command = new GetAllCartsCommand(pageSize, pageNumber, order);

            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(result.StatusCode, result.Success ? result.Payload : result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById( Guid id, CancellationToken cancellationToken)
        {
            var command = new GetCartCommand(id);

            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(result.StatusCode, result.Success ? result.Payload : result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,
            [FromBody] UpdateCartRequest updateCartRequest,
            CancellationToken cancellationToken)
        {
            var command = new UpdateCartCommand(id, updateCartRequest.UserId, updateCartRequest.Date, updateCartRequest.Products);

            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(result.StatusCode, result.Success ? result.Payload : result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCartCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(result.StatusCode, result.Success ? result.Payload : result.Message);
        }
    }
}
