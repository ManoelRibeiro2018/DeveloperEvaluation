using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SaleController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SaleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultResponse<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultResponse<CreateSaleResult>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResultResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateSaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultResponse<GetSaleResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSales(CancellationToken cancellationToken)
        {
            var command = new GetSaleCommand();
            var response = await _mediator.Send(command, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResultResponse<UpdateSaleCommand>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResultResponse<Sale.UpdateSale.UpdateSaleResult>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateSaleCommand>(request);            
            var response = await _mediator.Send(command, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResultResponse<DeleteSaleResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResultResponse<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultResponse<>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeleteSaleCommand>(id);
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(result.StatusCode, result);
        }
    }
}
