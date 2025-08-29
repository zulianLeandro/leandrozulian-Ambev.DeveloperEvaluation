using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale
{
    // Comando para cancelar uma venda
    public class CancelSaleCommand : IRequest<Unit>
    {
        public Guid SaleId { get; set; }

        public CancelSaleCommand(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
