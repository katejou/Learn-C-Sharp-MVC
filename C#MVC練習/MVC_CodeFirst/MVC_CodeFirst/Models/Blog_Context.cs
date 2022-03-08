using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using MVC_CodeFirst.Models;

namespace MVC_CodeFirst
{
    public partial class Blog_Context : DbContext
    {
        public Blog_Context()
            : base("name=Blog_Connection")
        {
        }

        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasMany(e => e.Post)
                .WithRequired(e => e.Blog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Blog)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);
        }
    }
}
