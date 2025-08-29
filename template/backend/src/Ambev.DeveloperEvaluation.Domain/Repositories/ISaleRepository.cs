using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task AddAsync(Entities.Sale sale);
        Task<Entities.Sale> GetByIdAsync(Guid saleId);
        Task UpdateAsync(Entities.Sale sale);
        Task<List<Entities.Sale>> GetAllAsync();
        // Você pode adicionar métodos para paginação, se necessário
    }
}
