using AIHelpdesk.API.Data;
using AIHelpdesk.API.DTOs;
using AIHelpdesk.API.Models;
using AIHelpdesk.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AIHelpdesk.Tests
{
    public class TicketServiceTests
    {
        [Fact]
        public async Task CreateTicketAsync_ShouldCreateTicketWithOpenStatus()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new AppDbContext(options);

            var logger = new Mock<ILogger<TicketService>>();

            var service = new TicketService(context, logger.Object);

            CreateTicketRequest request = new CreateTicketRequest
            {
                Title = "Printer not working",
                Description = "Office printer is not responding",
                Priority = "High"
            };

            // Act
            var result = await service.CreateTicketAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Open", result.Status);
            Assert.Equal("Printer not working", result.Title);
            Assert.Equal("High", result.Priority);
            Assert.True(result.Id > 0);
        }
        [Fact]
        public async Task GetTicketByIdAsync_ShouldReturnNull_WhenTicketDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new AppDbContext(options);

            var logger = new Mock<ILogger<TicketService>>();

            var service = new TicketService(context, logger.Object);

            // Act
            var result = await service.GetTicketByIdAsync(999);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task UpdateTicketAsync_ShouldUpdateExistingTicket()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            await using var context = new AppDbContext(options);

            var logger = new Mock<ILogger<TicketService>>();
            var service = new TicketService(context, logger.Object);

            var ticket = new Ticket
            {
                Title = "Old title",
                Description = "Old description",
                Priority = "Low",
                Status = "Open",
                CreatedAt = DateTime.UtcNow
            };

            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();

            var request = new UpdateTicketRequest
            {
                Title = "Updated title",
                Description = "Updated description",
                Priority = "High",
                Status = "In Progress"
            };

            // Act
            var result = await service.UpdateTicketAsync(ticket.Id, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated title", result.Title);
            Assert.Equal("Updated description", result.Description);
            Assert.Equal("High", result.Priority);
            Assert.Equal("In Progress", result.Status);
        }
        [Fact]
        public async Task UpdateTicketAsync_ShouldReturnNull_WhenTicketDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            await using var context = new AppDbContext(options);

            var logger = new Mock<ILogger<TicketService>>();
            var service = new TicketService(context, logger.Object);

            var request = new UpdateTicketRequest
            {
                Title = "Updated title",
                Description = "Updated description",
                Priority = "High",
                Status = "In Progress"
            };

            // Act
            var result = await service.UpdateTicketAsync(999, request);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task DeleteTicketAsync_ShouldDeleteExistingTicket()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            await using var context = new AppDbContext(options);

            var logger = new Mock<ILogger<TicketService>>();
            var service = new TicketService(context, logger.Object);

            var ticket = new Ticket
            {
                Title = "Ticket to delete",
                Description = "This ticket should be deleted",
                Priority = "Low",
                Status = "Open",
                CreatedAt = DateTime.UtcNow
            };

            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();

            // Act
            var result = await service.DeleteTicketAsync(ticket.Id);

            // Assert
            Assert.True(result);

            var deletedTicket = await context.Tickets
                .FindAsync(ticket.Id);

            Assert.Null(deletedTicket);
        }
        [Fact]
        public async Task DeleteTicketAsync_ShouldReturnFalse_WhenTicketDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            await using var context = new AppDbContext(options);

            var logger = new Mock<ILogger<TicketService>>();
            var service = new TicketService(context, logger.Object);

            // Act
            var result = await service.DeleteTicketAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}