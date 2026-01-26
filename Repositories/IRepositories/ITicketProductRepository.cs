using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Models;

namespace Ticket.Repositories.IRepositories
{
    public interface ITicketProductRepository
    {
        Task<List<TicketProducts>> GetAllAsync();
        Task<List<TicketProducts>> GetActiveAsync();
        Task<TicketProducts?> GetByIdAsync(int id);

        Task AddAsync(TicketProducts product);
        Task UpdateAsync(TicketProducts product);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}