using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;

        public CancelSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);

            if (sale == null)
            {
                throw new InvalidOperationException($"A venda com o ID {request.SaleId} não foi encontrada.");
            }

            sale.Cancel();

            await _saleRepository.UpdateAsync(sale);

            return Unit.Value;
        }
    }
}
