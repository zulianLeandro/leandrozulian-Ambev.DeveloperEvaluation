using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public string ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalItemAmount { get; private set; }
        public bool IsItemCancelled { get; private set; }

        public SaleItem(string productId, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
            {
                throw new InvalidOperationException("Não é possível vender mais de 20 itens idênticos.");
            }

            Id = Guid.NewGuid();
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
            IsItemCancelled = false;

            // Aplica as regras de negócio para o desconto
            ApplyDiscount();

            // Calcula o valor total do item
            TotalItemAmount = (quantity * unitPrice) - Discount;
        }

        public void Cancel()
        {
            IsItemCancelled = true;
        }

        private void ApplyDiscount()
        {
            if (Quantity >= 10 && Quantity <= 20)
            {
                Discount = UnitPrice * Quantity * 0.20m; // 20%
            }
            else if (Quantity >= 4 && Quantity < 10)
            {
                Discount = UnitPrice * Quantity * 0.10m; // 10%
            }
            else
            {
                Discount = 0;
            }
        }
    }
}
