using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSaleById
{
    public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, GetSaleByIdResult>
    {
        private readonly ISaleRepository _saleRepository;

        public GetSaleByIdHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<GetSaleByIdResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);

            if (sale == null)
            {
                throw new InvalidOperationException($"A venda com o ID {request.SaleId} não foi encontrada.");
            }

            var result = new GetSaleByIdResult
            {
                SaleId = sale.Id,
                Customer = sale.Customer,
                TotalAmount = sale.TotalAmount,
                Branch = sale.Branch,
                IsCancelled = sale.IsCancelled
            };

            return result;
        }
    }
}
