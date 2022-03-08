namespace MVC_CodeFirstNewDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        BlogName = c.String(nullable: false),
                        Url = c.String(nullable: false),
                        UsersUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.Users", t => t.UsersUserId)
                .Index(t => t.UsersUserId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        BlogBlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Blog", t => t.BlogBlogId)
                .Index(t => t.BlogBlogId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blog", "UsersUserId", "dbo.Users");
            DropForeignKey("dbo.Post", "BlogBlogId", "dbo.Blog");
            DropIndex("dbo.Post", new[] { "BlogBlogId" });
            DropIndex("dbo.Blog", new[] { "UsersUserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Post");
            DropTable("dbo.Blog");
        }
    }
}
