using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MVC_CodeFirst.Models
{
    public partial class OtherDB : DbContext
    {
        public OtherDB()
            : base("name=OtherDB")
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Head> Head { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Box> Box { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Box>()
                .Property(e => e.NW)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Box>()
                .Property(e => e.GW)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.UnitPrice)
                .HasPrecision(20, 10);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.TotalValue)
                .HasPrecision(20, 2);
        }
    }
}
