using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);

            if (sale == null)
            {
                throw new InvalidOperationException($"A venda com o ID {request.SaleId} não foi encontrada.");
            }

            var updateItemsDto = request.Items.Select(item => new UpdateSaleItemDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList();

            sale.Update(request.Customer, request.Branch, updateItemsDto);

            await _saleRepository.UpdateAsync(sale);

            return Unit.Value;
        }
    }
}
