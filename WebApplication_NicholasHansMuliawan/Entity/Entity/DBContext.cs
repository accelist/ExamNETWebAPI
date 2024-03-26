using Microsoft.EntityFrameworkCore;


namespace Entity.Entity
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Tickets> Tickets => Set<Tickets>();
        public DbSet<BookTicket> BookTickets => Set<BookTicket>();
    }
}
