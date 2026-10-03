using AIHelpdesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AIHelpdesk.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}