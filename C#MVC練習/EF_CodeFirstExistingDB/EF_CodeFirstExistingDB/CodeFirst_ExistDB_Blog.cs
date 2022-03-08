using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EF_CodeFirstExistingDB
{
    public partial class CodeFirst_ExistDB_Blog : DbContext
    {
        public CodeFirst_ExistDB_Blog()
            : base("name=CodeFirst_ExistDB_Blog_Context")
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
