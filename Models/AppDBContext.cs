using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ThekwiniAdvanaced.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() : base("Thekwini") { }

        public DbSet<Area> areas { get; set; }
        public DbSet<Owner> owners { get; set; }

        public DbSet<Violation> violations { get; set; }
        public DbSet<EstatePenalty> estatePenalties { get; set; }
    }
}