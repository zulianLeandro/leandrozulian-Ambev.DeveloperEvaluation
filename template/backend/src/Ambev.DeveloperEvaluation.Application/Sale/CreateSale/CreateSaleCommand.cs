using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    // O comando que será enviado para o Mediator
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string Customer { get; set; }
        public string Branch { get; set; }
        public List<CreateSaleItemCommand> Items { get; set; }
    }

    // A classe que representa um item dentro do comando
    public class CreateSaleItemCommand
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
