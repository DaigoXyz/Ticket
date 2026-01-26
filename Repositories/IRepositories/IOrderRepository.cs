using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Models;

namespace Ticket.Repositories.IRepositories
{
    public interface IOrderRepository
    {
        Task<Orders?> GetByIdAsync(int id);
        Task<Orders?> GetByOrderNoAsync(string orderNo);

        Task<List<Orders>> GetAllAsync();
        Task<List<Orders>> GetByUserIdAsync(int userId);

        Task AddAsync(Orders order);
        Task UpdateAsync(Orders order);
        Task SaveChangesAsync();
    }
}