using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace jQueryMobile_Ex2.Models
{
    public class MobileContext: DbContext
    {
        public MobileContext() : base("MobileDbConnection") { }
        public DbSet<Hero> Heroes { get; set; }

    }
}