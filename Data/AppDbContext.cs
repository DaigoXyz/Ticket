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
        public DbSet<Orders> Orders { get; set; } = null!;
        public DbSet<TicketProducts> TicketProducts { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntryTickets>()
                .HasOne(t => t.Order)
                .WithMany(o => o.Tickets)
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EntryTickets>()
                .HasOne(t => t.TicketProduct)
                .WithMany()
                .HasForeignKey(t => t.TicketProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EntryTickets>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Orders>()
                .Property(x => x.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TicketProducts>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<EntryTickets>()
                .HasIndex(x => x.TicketCode)
                .IsUnique();

            modelBuilder.Entity<Orders>()
                .HasIndex(x => x.OrderNo)
                .IsUnique();
        }
    }
}