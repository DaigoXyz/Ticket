using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Models;
using Microsoft.EntityFrameworkCore;

namespace Ticket.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<EntryTickets> EntryTickets { get; set; } = null!;
    }
}