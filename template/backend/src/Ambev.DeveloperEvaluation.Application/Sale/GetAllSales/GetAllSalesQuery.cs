using MediatR;
using Ambev.DeveloperEvaluation.Common;
using Ambev.DeveloperEvaluation.Common.Utilities;
namespace Ambev.DeveloperEvaluation.Application.Sale.GetAllSales
{
    // A query para buscar todas as vendas com paginação
    public class GetAllSalesQuery : IRequest<PaginatedList<GetAllSalesResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}