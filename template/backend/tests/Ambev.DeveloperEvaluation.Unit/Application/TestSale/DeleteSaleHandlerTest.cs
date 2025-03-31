using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestSale
{
    public class DeleteSaleHandlerTest
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly DeleteSaleHandler _handler;
        private readonly ILogger<DeleteSaleHandler> _logger;
        private readonly IEventPublisher _eventPublisher;
        private readonly DeleteSaleValidator _validator;

        public DeleteSaleHandlerTest()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<DeleteSaleHandler>>();
            _eventPublisher = Substitute.For<IEventPublisher>();
            _validator = new();
            _handler = new(_saleRepository, _logger, _eventPublisher, _validator);
        }

        [Fact(DisplayName = "When excuted should delete specific sale")]
        public async Task Handler_WhenExecuted_ShouldDeleteSale()
        {
            //Given
            var guid = Guid.NewGuid();
            var command = new DeleteSaleCommand(guid);
            var sale = SaleHandlerTestData.CreateSale;
            _saleRepository.GetByIdAsync(Arg.Is<Guid>(guid => guid == command.Id), Arg.Any<CancellationToken>())
                            .Returns(sale);
            //When
            var result = await _handler.Handle(command, CancellationToken.None);

            //Then
            Assert.True(result.Success);
            Assert.Equal(204, result.StatusCode);

        }
    }
}
