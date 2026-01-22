using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.DTOs.EntryTicket
{
    public class EntryTicketUpdateDto
    {
        public DateTime VisitDate { get; set; }
        public string? Purpose { get; set; }
        public string Status { get; set; } = "Booked";
    }
}