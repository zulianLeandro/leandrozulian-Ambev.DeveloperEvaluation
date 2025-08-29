using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSaleById
{
    public class GetSaleByIdResult
    {
        public Guid SaleId { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; }
        public bool IsCancelled { get; set; }
        // Outras propriedades da venda, como itens e preços
    }
}
