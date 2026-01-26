using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Ticket.Models;
using Ticket.Data;
using Ticket.Components;

namespace Ticket.Repositories
{
    public class EntryTicketRepository : IEntryTicketRepository
    {
        private readonly AppDbContext _context;

        public EntryTicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<EntryTickets>> GetAllAsync()
            =>  _context.EntryTickets
                .AsNoTracking()
                .Include(t => t.User)
                .Include(t => t.TicketProduct)
                .Include(t => t.Order)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        
        public Task<EntryTickets?> GetByIdAsync(int id)
            => _context.EntryTickets
                .Include(t => t.User)
                .Include(t => t.TicketProduct)
                .Include(t => t.Order)
                .FirstOrDefaultAsync(x => x.Id == id);

        public Task<List<EntryTickets>> GetByVisitDateAsync(DateTime date)
        {
            var d = date.Date;
            return _context.EntryTickets
                    .AsNoTracking()
                    .Include(t => t.User)
                    .Where(x => x.VisitDate.Date == d)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
        }

        public Task<List<EntryTickets>> GetByUserIdAsync(int userId)
            => _context.EntryTickets
                .AsNoTracking()
                .Include(t => t.User)
                .Include(t => t.TicketProduct)
                .Include(t => t.Order)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

        public Task<EntryTickets?> GetByCodeAsync(string ticketCode)
            => _context.EntryTickets
                .Include(t => t.User)
                .Include(t => t.TicketProduct)
                .Include(t => t.Order)
                .FirstOrDefaultAsync(x => x.TicketCode == ticketCode);

        public async Task AddAsync(EntryTickets ticket)
        {
            await _context.EntryTickets.AddAsync(ticket);
        }

        public async Task UpdateAsync(EntryTickets ticket)
        {
            _context.EntryTickets.Update(ticket);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await _context.EntryTickets.FirstOrDefaultAsync(x => x.Id == id);
            if(ticket != null) _context.EntryTickets.Remove(ticket);
        }

        public Task SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}