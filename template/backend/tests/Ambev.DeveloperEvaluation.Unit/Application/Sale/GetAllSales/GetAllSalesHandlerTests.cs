using Ambev.DeveloperEvaluation.Application.Sale.GetAllSales;
using Ambev.DeveloperEvaluation.Common.Utilities;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.GetAllSales
{
    public class GetAllSalesHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly GetAllSalesHandler _handler;

        public GetAllSalesHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new GetAllSalesHandler(_saleRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnPaginatedListOfSales()
        {
            // Arrange
            var salesData = new List<Sale>
            {
                new Sale("Cliente A", "Filial Centro"),
                new Sale("Cliente B", "Filial Norte"),
                new Sale("Cliente C", "Filial Sul")
            };

            // Simula o repositório retornando a lista de vendas
            _saleRepository.GetAllAsync().Returns(salesData);

            var query = new GetAllSalesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(salesData.Count, result.TotalItems);
            Assert.IsType<PaginatedList<GetAllSalesResult>>(result);
        }
    }
}
