using AIHelpdesk.API.DTOs;
using AIHelpdesk.API.Interfaces;
using AIHelpdesk.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIHelpdesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {

        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        //[HttpGet("test-error")]
        //public IActionResult TestError()
        //{
        //    throw new Exception("This is a test exception.");
        //}
        //After you've verified it, delete the temporary TestError() endpoint. Then we'll learn the different logging levels: LogInformation, LogWarning, LogError, and LogDebug.
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            return Ok(await _ticketService.GetTicketsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketRequest request)
        {

            Ticket createdTicket = await _ticketService.CreateTicketAsync(request);

            return Ok(createdTicket);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            Ticket? fatchedTicket = await _ticketService.GetTicketByIdAsync(id);

            if (fatchedTicket == null)
                return NotFound();

            return Ok(fatchedTicket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicketAsync(int id, UpdateTicketRequest updateTicketRequest)
        {
            Ticket? updatedTicket = await _ticketService.UpdateTicketAsync(id, updateTicketRequest);

            if (updatedTicket == null)
                return NotFound();

            return Ok(updatedTicket);
        }
        [Authorize(Roles = "Admin,Agent")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketAsync(int id)
        {
            bool isDeleted = await _ticketService.DeleteTicketAsync(id);

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("TestGlobalExceptionHandler")]
       public void CheckGlobalHandler(string inputValue)
        {
            //return Convert.ToInt16(inputValue);
            throw new InvalidOperationException("New Coustom Global exception Occoured.");
        }
    }
}
