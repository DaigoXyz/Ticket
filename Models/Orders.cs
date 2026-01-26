using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Models
{
    public class Orders
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; } = null!;

        public string OrderNo { get; set; } = null!;
        public string Status { get; set; } = "Pending"; 

        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? PaidAt { get; set; }

        public List<EntryTickets> Tickets { get; set; } = new();
    }
}