using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Bootstrap_Ex.Models;

namespace Bootstrap_Ex.Data
{
    public class FriendInitializer : DropCreateDatabaseIfModelChanges<FriendContext> 
    {
        protected override void Seed(FriendContext context)
        {
            List<BootstrapFriend> Friends = new List<BootstrapFriend>
            { 
            new BootstrapFriend{ ID=1,Name="5t5",Phone="123456789"},
            new BootstrapFriend{ ID=1,Name="xyj",Phone="987654321"}
            };

            Friends.ForEach(x => context.BootstrapFriends.Add(x));
            context.SaveChanges();
            //base.Seed(context);
        }

    }



    public class FriendContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public FriendContext() : base("name=FriendContext")
        {
        }

        public System.Data.Entity.DbSet<Bootstrap_Ex.Models.BootstrapFriend> BootstrapFriends { get; set; }

        public System.Data.Entity.DbSet<Bootstrap_Ex.Models.Student> Students { get; set; }
    }
}
