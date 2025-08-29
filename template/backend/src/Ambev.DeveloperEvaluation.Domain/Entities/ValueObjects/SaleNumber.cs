using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities.ValueObjects
{
    public class SaleNumber
    {
        public int Value { get; private set; }

        public SaleNumber(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("O número da venda deve ser maior que zero.", nameof(value));
            }

            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
} 