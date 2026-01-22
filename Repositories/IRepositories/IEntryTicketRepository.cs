using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Models;

namespace Ticket.Repositories.IRepositories
{
    public interface IEntryTicketRepository
    {
        Task<EntryTickets?> GetByIdAsync(int id);
        Task<List<EntryTickets>> GetAllAsync();
        Task<List<EntryTickets>> GetByVisitDateAsync(DateTime date);
        Task<List<EntryTickets>> GetByUserIdAsync(int userId);

        Task AddAsync(EntryTickets ticket);
        Task UpdateAsync(EntryTickets ticket);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}