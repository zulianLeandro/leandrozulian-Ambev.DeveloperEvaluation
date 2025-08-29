using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public CreateSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Ambev.DeveloperEvaluation.Domain.Entities.Sale(request.Customer, request.Branch);

            foreach (var itemCommand in request.Items)
            {
                var saleItem = new SaleItem(itemCommand.ProductId, itemCommand.Quantity, itemCommand.UnitPrice);
                sale.AddItem(saleItem);
            }

            await _saleRepository.AddAsync(sale);

            return new CreateSaleResult
            {
                SaleId = sale.Id
            };
        }
    }
}
