using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.DTOs.EntryTicket
{
    public class EntryTicketCreateDto
    {
        public int TicketProductId{ get; set; }
        public int Qty { get; set; } = 1;
        public DateTime VisitDate { get; set; }
        public string? Purpose { get; set; }
    }
}