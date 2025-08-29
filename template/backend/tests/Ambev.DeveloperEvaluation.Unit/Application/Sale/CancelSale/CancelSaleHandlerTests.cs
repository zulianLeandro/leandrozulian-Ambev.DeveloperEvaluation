using Ambev.DeveloperEvaluation.Application.Sale.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.CancelSale
{
    public class CancelSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly CancelSaleHandler _handler;

        public CancelSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new CancelSaleHandler(_saleRepository);
        }

        [Fact]
        public async Task Handle_ShouldCancelSale_WhenSaleExists()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var saleToCancel = new Sale("Cliente Teste", "Filial Teste");

            // Simula o repositório retornando a venda
            _saleRepository.GetByIdAsync(saleId).Returns(saleToCancel);

            var command = new CancelSaleCommand(saleId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(s => s.IsCancelled == true));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSaleDoesNotExist()
        {
            // Arrange
            var nonExistentSaleId = Guid.NewGuid();

            // Simula o repositório retornando null para uma venda não encontrada
            _saleRepository.GetByIdAsync(nonExistentSaleId).Returns((Sale)null);

            var command = new CancelSaleCommand(nonExistentSaleId);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
