using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Common.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetAllSales
{
    public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, PaginatedList<GetAllSalesResult>>
    {
        private readonly ISaleRepository _saleRepository;

        public GetAllSalesHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<PaginatedList<GetAllSalesResult>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync();

            var paginatedSales = sales
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var results = paginatedSales.Select(sale => new GetAllSalesResult
            {
                SaleId = sale.Id,
                Customer = sale.Customer,
                TotalAmount = sale.TotalAmount,
                Branch = sale.Branch,
                IsCancelled = sale.IsCancelled
            }).ToList();

            return new PaginatedList<GetAllSalesResult>(results, sales.Count, request.PageNumber, request.PageSize);
        }
    }
}
