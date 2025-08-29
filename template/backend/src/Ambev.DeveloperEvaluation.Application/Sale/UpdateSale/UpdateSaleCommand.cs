using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale
{
    // Comando para atualizar uma venda
    public class UpdateSaleCommand : IRequest<Unit> // 'Unit' indica que não há retorno
    {
        public Guid SaleId { get; set; }
        public string Customer { get; set; }
        public string Branch { get; set; }
        public List<UpdateSaleItemCommand> Items { get; set; }
    }

    public class UpdateSaleItemCommand
    {
        public Guid ItemId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
