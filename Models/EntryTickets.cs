using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Models
{
    public class EntryTickets
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; } = null!;

        public int TicketProductId { get; set; }
        public TicketProducts TicketProduct { get; set; } = null!;

        public int OrderId { get; set; }
        public Orders Order { get; set; } = null!;

        public string TicketCode { get; set; } = null!;

        public string? Purpose { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime? TimeIn { get; set; }

        public string Status { get; set; } = "Issued"; 
        public decimal Price { get; set; } 
        public DateTime CreatedAt { get; set; }
    }

}