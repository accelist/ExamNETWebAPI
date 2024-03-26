using System;
using System.Collections.Generic;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace Entity
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> dBContextOptions) : base(dBContextOptions)
        {

        }
       
        public DbSet<Ticket> Tickets => Set<Ticket>();
        


    }
}

