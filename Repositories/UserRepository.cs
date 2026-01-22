using Ticket.Repositories.IRepositories;
using Ticket.Data;
using Ticket.Models;
using Microsoft.EntityFrameworkCore;

namespace Ticket.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public UserRepository(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        private AppDbContext CreateContext() => _dbContextFactory.CreateDbContext();

        public async Task<List<Users>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Users
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            using var context = CreateContext();
            return await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Users?> GetByUsernameAsync(string username)
        {
            using var context = CreateContext();
            return await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            using var context = CreateContext();
            return await context.Users
                .AsNoTracking()
                .AnyAsync(x => x.Username == username);
        }

        public async Task AddAsync(Users user)
        {
            using var context = CreateContext();
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync(); // Simpan langsung
        }

        public async Task UpdateAsync(Users user)
        {
            using var context = CreateContext();
            context.Users.Update(user);
            await context.SaveChangesAsync(); // Simpan langsung
        }

        public async Task DeleteAsync(int id)
        {
            using var context = CreateContext();
            var user = await context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync(); // Simpan langsung
            }
        }
    }
}