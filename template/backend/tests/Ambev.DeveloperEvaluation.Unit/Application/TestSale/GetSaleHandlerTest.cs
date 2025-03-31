using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using NSubstitute;
using Xunit;
using SaleEntity = Ambev.DeveloperEvaluation.Domain.Entities.Sale;


namespace Ambev.DeveloperEvaluation.Unit.Application.TestSale
{
    public class GetSaleHandlerTest
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetSaleHandler _getSaleHandler;

        public GetSaleHandlerTest()
        {

            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _getSaleHandler = new(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "When Executed should return valid payload")]
        public async Task Handler_whenExecuted_ShouldReturnValidPayload()
        {
            //Given
            var command = new GetSaleCommand();
            List<SaleEntity> sale = [SaleHandlerTestData.CreateSale];
            _saleRepository.GetAllAsync(Arg.Any<CancellationToken>())
                .Returns(sale);

            //When
            var result = await _getSaleHandler.Handle(command, CancellationToken.None);

            //Then
            Assert.Equal(200, result.StatusCode);
        }
    }
}
