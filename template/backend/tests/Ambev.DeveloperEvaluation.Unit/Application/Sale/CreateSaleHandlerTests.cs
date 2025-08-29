using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.CreateSale
{
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new CreateSaleHandler(_saleRepository);
        }

        [Fact]
        public async Task Handle_ShouldCallAddAsyncOnRepository_WhenCommandIsValid()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                Customer = "João da Silva",
                Branch = "Filial SP",
                Items = new System.Collections.Generic.List<CreateSaleItemCommand>
                {
                    new CreateSaleItemCommand { ProductId = "prod_1", Quantity = 5, UnitPrice = 50.00m }
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _saleRepository.Received(1).AddAsync(Arg.Is<Ambev.DeveloperEvaluation.Domain.Entities.Sale>(s =>
                s.Customer == command.Customer &&
                s.Branch == command.Branch &&
                s.Items.Count == 1));

            Assert.NotEqual(Guid.Empty, result.SaleId);
        }
    }
}
