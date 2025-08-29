using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSaleById
{ // A query para buscar uma venda por ID
    public class GetSaleByIdQuery : IRequest<GetSaleByIdResult>
    {
        public Guid SaleId { get; set; }

        public GetSaleByIdQuery(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
