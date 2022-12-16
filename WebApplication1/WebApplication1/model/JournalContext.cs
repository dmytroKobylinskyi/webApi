
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace WebApplication1.model
{
    public class JournalContext : DbContext
    {
        public JournalContext()
            : base("DbConnection")
        {
            Database.SetInitializer(new JournalDBInitializer());
        }

        public DbSet<Journal> Journal { get; set; }
        public DbSet<Labs> Labs { get; set; }
    }
}
