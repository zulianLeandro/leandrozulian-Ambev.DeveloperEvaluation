using Ambev.DeveloperEvaluation.Application.Sale.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.GetSaleById
{
    public class GetSaleByIdHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly GetSaleByIdHandler _handler;

        public GetSaleByIdHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new GetSaleByIdHandler(_saleRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSale_WhenSaleExists()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var existingSale = new Sale("Cliente Teste", "Filial Teste");

            // Simula o repositório retornando a venda
            _saleRepository.GetByIdAsync(saleId).Returns(existingSale);

            var query = new GetSaleByIdQuery(saleId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            // Verifica se o handler está retornando o mesmo ID da venda
            Assert.Equal(existingSale.Id, result.SaleId);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSaleDoesNotExist()
        {
            // Arrange
            var nonExistentSaleId = Guid.NewGuid();

            // Simula o repositório retornando null para uma venda não encontrada
            _saleRepository.GetByIdAsync(nonExistentSaleId).Returns((Sale)null);

            var query = new GetSaleByIdQuery(nonExistentSaleId);

            // Act & Assert
            // Verifica se o handler lança uma exceção quando a venda não é encontrada
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
