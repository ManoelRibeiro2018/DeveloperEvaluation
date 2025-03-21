using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, ResultResponse<CreateSaleResult>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly IEventPublisher _eventPublisher;
        public async Task<ResultResponse<CreateSaleResult>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (request.Products.Exists(e => e.Quantity > 20))
            {
                _logger.LogError("ClassName : {ClassName} - MethodName: {MethodName} - Message : {Message}",
                               nameof(CreateSaleHandler),
                               nameof(Handle),
                               "Maximum limit of 20 items per product.");

                return ResultResponse<CreateSaleResult>.Failure(409, "Maximum limit of 20 items per product.");
            }

            var sale = new Sale
            {
                UserId = request.UserId,
                BranchId = request.BranchId,
                Products = request.Products.Select(p =>
                {
                    var productSale = new SaleItem
                    {
                        Quantity = p.Quantity,
                        UnitPrice = p.UnitPrice
                    };

                    productSale.ApplyDiscount();
                    return productSale;
                }).ToList()
            };

            sale.CalculateTotalAmount();

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            _logger.LogInformation("Sale created successfully ID {SaleId}", createdSale.Id);

            await PublishSaleCreatedEventAsync(createdSale, cancellationToken);

            var saleResult = new CreateSaleResult { Id = createdSale.Id };

            return ResultResponse<CreateSaleResult>.Successful(saleResult, 201, "Sale created with success");
        }
    }
}
