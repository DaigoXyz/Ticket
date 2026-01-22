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

        public string? Purpose { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime? TimeIn { get; set; }
        public string Status { get; set; } = "Booked";
        public DateTime CreatedAt { get; set; }
    }
}