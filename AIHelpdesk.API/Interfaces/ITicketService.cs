using AIHelpdesk.API.DTOs;
using AIHelpdesk.API.Models;

namespace AIHelpdesk.API.Interfaces
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetTicketsAsync();
        Task<Ticket> CreateTicketAsync(CreateTicketRequest request);
        Task<Ticket?> GetTicketByIdAsync(int id);

        Task<Ticket?> UpdateTicketAsync(int id, UpdateTicketRequest request);
        Task<bool> DeleteTicketAsync(int id);
    }
}
