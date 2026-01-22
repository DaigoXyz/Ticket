using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.DTOs.EntryTicket
{
    public class EntryTicketCreateDto
    {
        public DateTime VisitDate { get; set; }
        public string? Purpose { get; set; }
    }
}