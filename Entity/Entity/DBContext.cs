using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base (dbContextOptions)
        {
            
        }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<BookedTicket> BookedTickets => Set<BookedTicket>();
        public DbSet<Ticket> Tickets => Set<Ticket>();

    }
}
