using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ticket.Data;
using Ticket.Models;
using Ticket.Repositories.IRepositories;

namespace Ticket.Repositories
{
    public class TicketProductRepository : ITicketProductRepository
    {
        private readonly AppDbContext _context;

        public TicketProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<TicketProducts>> GetAllAsync()
            => _context.TicketProducts
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToListAsync();

        public Task<List<TicketProducts>> GetActiveAsync()
            => _context.TicketProducts
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync();

        public Task<TicketProducts?> GetByIdAsync(int id)
            => _context.TicketProducts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(TicketProducts product)
        {
            await _context.TicketProducts.AddAsync(product);
        }

        public async Task UpdateAsync(TicketProducts product)
        {
            _context.TicketProducts.Update(product);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.TicketProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null) _context.TicketProducts.Remove(product);
        }

        public Task SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}