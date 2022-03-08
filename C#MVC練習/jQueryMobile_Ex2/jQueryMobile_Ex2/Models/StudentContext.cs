using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace jQueryMobile_Ex2.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext() : base("StudentConnection") { }
        public DbSet<Student> Students { get; set; }
    }
}