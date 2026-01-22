using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Models;
using Ticket.DTOs.EntryTicket;

namespace Ticket.Mappers
{
    public static class EntityTicketMapper
    {
        public static EntryTicketListDto ToListDto(this EntryTickets t)
        {
            return new EntryTicketListDto
            {
                Id = t.Id,
                UserId = t.UserId,
                VisitorName = t.User?.Name ?? "N/A",
                VisitDate = t.VisitDate,
                TimeIn = t.TimeIn,
                Status = t.Status,
                Purpose = t.Purpose
            };
        }

        public static List<EntryTicketListDto> ToListDtos(this IEnumerable<EntryTickets> tickets)
            => tickets.Select(t => t.ToListDto()).ToList();
    }
}