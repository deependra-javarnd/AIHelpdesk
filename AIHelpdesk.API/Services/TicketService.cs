using AIHelpdesk.API.Data;
using AIHelpdesk.API.DTOs;
using AIHelpdesk.API.Interfaces;
using AIHelpdesk.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace AIHelpdesk.API.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TicketService> _logger;

        public TicketService(AppDbContext context, ILogger<TicketService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _context.Tickets.AsNoTracking().ToListAsync();
        }

        public async Task<Ticket> CreateTicketAsync(CreateTicketRequest request)
        {

            Ticket ticket = new Ticket
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                Status = "Open",
                CreatedAt = DateTime.UtcNow
            };

            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();
            // Logging through Ilogger.
            _logger.LogInformation("Ticket created successfully with Id {TicketId}", ticket.Id);
            return ticket;
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            Ticket? ticket =  await _context.Tickets.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (ticket == null)
            {
                _logger.LogWarning(
                    "Ticket with Id {TicketId} was not found",
                    id);
            }

            return ticket;
        }

        public async Task<Ticket?> UpdateTicketAsync(int id, UpdateTicketRequest request)
        {
            Ticket? fatchedTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);

            if (fatchedTicket == null)
                return null;

            fatchedTicket.Title = request.Title;
            fatchedTicket.Priority = request.Priority;
            fatchedTicket.Status = request.Status;
            fatchedTicket.Description = request.Description;
            fatchedTicket.CreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return fatchedTicket;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            Ticket? fatchedTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);

            if (fatchedTicket == null)
                return false;

            _context.Tickets.Remove(fatchedTicket);

            await _context.SaveChangesAsync();

            return true;

        }
    }
}