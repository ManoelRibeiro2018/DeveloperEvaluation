using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, ResultResponse<CreateSaleResult>>
    {
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly IEventPublisher _eventPublisher;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly CreateSaleValidator _validationRules;

        public CreateSaleHandler(ILogger<CreateSaleHandler> logger,
            IEventPublisher eventPublisher,
            ISaleRepository saleRepository,
            IMapper mapper,
            CreateSaleValidator validationRules)
        {
            _logger = logger;
            _eventPublisher = eventPublisher;
            _saleRepository = saleRepository;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<ResultResponse<CreateSaleResult>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validationRules.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return ResultResponse<CreateSaleResult>.Failure(400, validationResult.Errors);
            

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
                SaleItens = request.Products.Select(p =>
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

            await _eventPublisher.PublishAsync(createdSale, cancellationToken);

            var saleResult = new CreateSaleResult { Id = createdSale.Id };

            return ResultResponse<CreateSaleResult>.Successful(saleResult, 201, "Sale created successfully");
        }
    }
}
