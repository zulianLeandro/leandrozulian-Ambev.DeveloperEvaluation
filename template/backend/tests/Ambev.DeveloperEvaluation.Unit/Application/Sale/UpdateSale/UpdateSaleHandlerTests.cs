using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.UpdateSale
{
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new UpdateSaleHandler(_saleRepository);
        }

        [Fact]
        public async Task Handle_ShouldUpdateSale_WhenSaleExists()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var existingSale = new Sale("Cliente Antigo", "Filial Antiga");
            _saleRepository.GetByIdAsync(saleId).Returns(existingSale);

            var command = new UpdateSaleCommand
            {
                SaleId = saleId,
                Customer = "Cliente Novo",
                Branch = "Filial Nova",
                Items = new List<UpdateSaleItemCommand>
                {
                    new UpdateSaleItemCommand { ItemId = Guid.NewGuid(), ProductId = "PROD01", Quantity = 5, UnitPrice = 10.00m }
                }
            };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(s =>
                s.Customer == "Cliente Novo" && s.Branch == "Filial Nova"));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSaleDoesNotExist()
        {
            // Arrange
            var nonExistentSaleId = Guid.NewGuid();
            _saleRepository.GetByIdAsync(nonExistentSaleId).Returns((Sale)null);

            var command = new UpdateSaleCommand { SaleId = nonExistentSaleId, Customer = "Cliente Inexistente" };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
