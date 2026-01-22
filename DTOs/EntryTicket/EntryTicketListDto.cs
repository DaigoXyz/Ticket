using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Models;

namespace Ticket.DTOs.EntryTicket
{
    public class EntryTicketListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string VisitorName { get; set; } = string.Empty;
        public DateTime VisitDate { get; set; }
        public DateTime? TimeIn { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Purpose { get; set; }
    }
}