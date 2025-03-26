using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    internal class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, ResultResponse<DeleteSaleResponse>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<DeleteSaleHandler> _logger;
        private readonly IEventPublisher _eventPublisher;
        private readonly DeleteSaleValidator _validations;

        public DeleteSaleHandler(
            ISaleRepository saleRepository,
            ILogger<DeleteSaleHandler> logger,
            IEventPublisher eventPublisher,
            DeleteSaleValidator validations)
        {
            _saleRepository = saleRepository;
            _logger = logger;
            _eventPublisher = eventPublisher;
            _validations = validations;
        }

        public async Task<ResultResponse<DeleteSaleResponse>> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validations.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return ResultResponse<DeleteSaleResponse>.Failure(400, validationResult.Errors);

            var success = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
            if (!success)
            {
                _logger.LogError("Sale with ID {SaleId} not found to delete", request.Id);
                return ResultResponse<DeleteSaleResponse>.Failure(404, "Sale not found");
            }

            _logger.LogInformation("Sale with ID {SaleId} deleted successfully", request.Id);

            await _eventPublisher.PublishAsync(request.Id, cancellationToken);

            return ResultResponse<DeleteSaleResponse>.Successful(new DeleteSaleResponse { Success = true }, 204, "Sale deleted successfully");
        }
    }
}
