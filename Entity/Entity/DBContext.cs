using Microsoft.EntityFrameworkCore;

namespace Entity.Entity
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base (dbContextOptions)
        {            
        }

        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<BookedTicket> BookedTickets => Set<BookedTicket>();
        public DbSet<TicketBookedTicketMapping> TicketBookedTicketMappings => Set<TicketBookedTicketMapping>();
    }
}
