﻿using Microsoft.EntityFrameworkCore;

namespace Entity.Entity
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<BookedTicket> BookedTickets => Set<BookedTicket>();
    }
}

