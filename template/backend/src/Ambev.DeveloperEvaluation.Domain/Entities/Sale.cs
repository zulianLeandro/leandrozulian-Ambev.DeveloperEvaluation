using System;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.Entities.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.DTOs;  

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public SaleNumber SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public string Customer { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Branch { get; private set; }
        public List<SaleItem> Items { get; private set; }
        public bool IsCancelled { get; private set; }

        public Sale(string customer, string branch)
        {
            Id = Guid.NewGuid();
            SaleNumber = new SaleNumber(0);
            SaleDate = DateTime.Now;
            Customer = customer;
            Branch = branch;
            Items = new List<SaleItem>();
            IsCancelled = false;
        }

        public void AddItem(SaleItem item)
        {
            Items.Add(item);
            UpdateTotalAmount();
        }

        public void Cancel()
        {
            IsCancelled = true;
        }

        // Método de atualização que aceita DTOs
        public void Update(string newCustomer, string newBranch, List<UpdateSaleItemDto> newItems)
        {
            Customer = newCustomer;
            Branch = newBranch;
            Items.Clear();
            foreach (var itemDto in newItems)
            {
                var saleItem = new SaleItem(itemDto.ProductId, itemDto.Quantity, itemDto.UnitPrice);
                Items.Add(saleItem);
            }
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = Items.Sum(item => item.TotalItemAmount);
        }
    }
}
