namespace MVC_CodeFirstNewDB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_CodeFirst.Blog_Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVC_CodeFirst.Blog_Context context)
        {
            context.Users.AddOrUpdate(x => x.UserId, new MVC_CodeFirst.Models.Users { UserName = "seed1", Email = "seed1@mail.com" });
            context.Users.AddOrUpdate(x => x.UserId, new MVC_CodeFirst.Models.Users { UserName = "seed2", Email = "seed2@mail.com" });
            context.Users.AddOrUpdate(x => x.UserId, new MVC_CodeFirst.Models.Users { UserName = "seed3", Email = "seed3@mail.com" });
        }
    }
}
