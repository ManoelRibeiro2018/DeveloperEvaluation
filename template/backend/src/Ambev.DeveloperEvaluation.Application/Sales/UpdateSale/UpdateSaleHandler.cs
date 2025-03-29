using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, ResultResponse<UpdateSaleResult>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSaleHandler> _logger;
        private readonly IEventPublisher _eventPublisher;
        private readonly UpdateSaleValidator _validationRules;

        public UpdateSaleHandler(ISaleRepository saleRepository,
            IMapper mapper,
            ILogger<UpdateSaleHandler> logger,
            IEventPublisher eventPublisher,
            UpdateSaleValidator validationRules)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
            _eventPublisher = eventPublisher;
            _validationRules = validationRules;
        }

        public async Task<ResultResponse<UpdateSaleResult>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validationRules.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return ResultResponse<UpdateSaleResult>.Failure(400, validationResult.Errors);


            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale is null)
            {
                _logger.LogError("ClassName : {ClassName} - MethodName: {MethodName} - Message : {Message}",
                               nameof(CreateSaleHandler),
                               nameof(Handle),
                               "Sale not found to update.");

                return ResultResponse<UpdateSaleResult>.Failure(404, "Sale not found to update");
            }

            if (request.SaleItemDtos.Exists(e => e.Quantity > 20))
            {
                _logger.LogError("ClassName : {ClassName} - MethodName: {MethodName} - Message : {Message}",
                               nameof(CreateSaleHandler),
                               nameof(Handle),
                               "Maximum limit of 20 items per product.");

                return ResultResponse<UpdateSaleResult>.Failure(409, "Maximum limit of 20 items per product.");
            }

            sale.UserId = request.UserId;
            sale.BranchId = request.BranchId;
            sale.IsCanceled = request.IsCanceled;
            sale.UpdatedAt = DateTime.UtcNow;
            sale.SaleItens = request.SaleItemDtos.Select(p =>
            {
                var saleItem = new SaleItem
                {
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice
                };

                saleItem.ApplyDiscount();
                return saleItem;
            }).ToList();

            sale.CalculateTotalAmount();

            var result = await _saleRepository.UpdateAsync(sale, cancellationToken);

            _logger.LogInformation("Sale updated successfully");

            await _eventPublisher.PublishAsync(sale, cancellationToken);

            return ResultResponse<UpdateSaleResult>.Successful(new UpdateSaleResult { Id = result }, 204, "Sale updated successfully");
        }
    }
}