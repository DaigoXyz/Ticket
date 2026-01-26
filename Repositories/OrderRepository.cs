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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Orders?> GetByIdAsync(int id)
            => _context.Orders
                .Include(o => o.User)
                .Include(o => o.Tickets) // hapus kalau gak punya navigation
                .FirstOrDefaultAsync(x => x.Id == id);

        public Task<Orders?> GetByOrderNoAsync(string orderNo)
            => _context.Orders
                .Include(o => o.User)
                .Include(o => o.Tickets) // hapus kalau gak punya navigation
                .FirstOrDefaultAsync(x => x.OrderNo == orderNo);

        public Task<List<Orders>> GetAllAsync()
            => _context.Orders
                .AsNoTracking()
                .Include(o => o.User)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

        public Task<List<Orders>> GetByUserIdAsync(int userId)
            => _context.Orders
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

        public async Task AddAsync(Orders order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task UpdateAsync(Orders order)
        {
            _context.Orders.Update(order);
            await Task.CompletedTask;
        }

        public Task SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}