using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using SaleEntity = Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestSale
{
    public class UpdateSaleHandlerTest
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly UpdateSaleHandler _handler;
        private readonly ILogger<UpdateSaleHandler> _logger;
        private readonly IEventPublisher _eventPublisher;
        private readonly UpdateSaleValidator _validator;

        public UpdateSaleHandlerTest()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<UpdateSaleHandler>>();
            _eventPublisher = Substitute.For<IEventPublisher>();
            _validator = new();
            _handler = new UpdateSaleHandler(_saleRepository, _mapper, _logger, _eventPublisher, _validator);
        }

        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_WhenExecuteValidateResquet_ShouldReturnSuccess()
        {
            // Given
            var command = SaleHandlerTestData.UpdateSaleHandlerCommand;
            var sale = SaleHandlerTestData.UpdateSale;
            _saleRepository.UpdateAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>())
             .Returns(sale.Id);

            _saleRepository.GetByIdAsync(Arg.Is<Guid>(guid => guid == command.Id), Arg.Any<CancellationToken>())
                .Returns(sale);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
            await _saleRepository.Received(1).UpdateAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>());
            await _eventPublisher.Received(1).PublishAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "When executed and product is not found should return invalid")]
        public async Task Handle_WhenProductIsNotFount_ShouldReturnInvalid()
        {
            //Given
            var command = SaleHandlerTestData.UpdateSaleHandlerCommand;

            await _validator.ValidateAsync(command);

            //When
            var result = await _handler.Handle(command, CancellationToken.None);

            //Then
            Assert.False(result.Success);
            Assert.Equal(404, result.StatusCode);
            await _saleRepository.DidNotReceive().CreateAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>());
            await _eventPublisher.DidNotReceive().PublishAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "When executed and product quantity is greater than twenty it should return invalid")]
        public async Task Handle_WhenProductQuantityGreater_ThanTwety_ShouldReturnInvalid()
        {
            //Given
            var command = SaleHandlerTestData.UpdateSaleHandlerCommand;
            var sale = SaleHandlerTestData.UpdateSale;
            command.SaleItens[0].Quantity = 25;

            await _validator.ValidateAsync(command);

            _saleRepository.GetByIdAsync(Arg.Is<Guid>(guid => guid == command.Id), Arg.Any<CancellationToken>())
                .Returns(sale);
            //When
            var result = await _handler.Handle(command, CancellationToken.None);

            //Then
            Assert.False(result.Success);
            Assert.Equal(409, result.StatusCode);
            await _saleRepository.DidNotReceive().CreateAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>());
            await _eventPublisher.DidNotReceive().PublishAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>());
        }
    }
}