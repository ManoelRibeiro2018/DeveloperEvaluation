using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;
using SaleEntity = Ambev.DeveloperEvaluation.Domain.Entities.Sale;


namespace Ambev.DeveloperEvaluation.Unit.Application.Sale
{
    public class CreateSaleHandlerTest
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly CreateSaleHandler _handler;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly IEventPublisher _eventPublisher;
        private readonly CreateSaleValidator _validator;

        public CreateSaleHandlerTest()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<CreateSaleHandler>>();
            _eventPublisher = Substitute.For<IEventPublisher>();
            _validator = new();
            _handler = new CreateSaleHandler(_logger, _eventPublisher, _saleRepository, _mapper, _validator);
        }

        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_WhenExecuteValidateResquet_ShouldReturnSuccess()
        {
            // Given
            var command = SaleHandlerTestData.CreateSaleHandlerCommand;
            var sale = SaleHandlerTestData.CreateSale;
            _saleRepository.CreateAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>())
             .Returns(sale);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            Assert.NotNull(result);
            Assert.Equal(sale.Id, result.Payload.Id);
            await _saleRepository.Received(1).CreateAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid sale data When creating sale Then returns invalid")]
        public async Task Handle_WhenExecuteValidateResquet_ShouldReturnInvalid()
        {
            // Given
            var command = new CreateSaleCommand();
            await _validator.ValidateAsync(command);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            Assert.False(result.Success);
            await _saleRepository.DidNotReceive().CreateAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "When executed and product quantity is greater than twenty it should return invalid")]
        public async Task Handle_WhenProductQuantityGreater_ThanTwety_ShouldReturnInvalid()
        {
            //Given
            var command = SaleHandlerTestData.CreateSaleHandlerCommand;
            command.SaleItens[0].Quantity = 25;

            await _validator.ValidateAsync(command);

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
