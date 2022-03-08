namespace MVC_CodeFirstNewDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDataAnnotationUpdate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "BlogUser");
            RenameColumn(table: "dbo.BlogUser", name: "UserId", newName: "Id");
            RenameColumn(table: "dbo.BlogUser", name: "Email", newName: "PersonalEmail");
            AlterColumn("dbo.BlogUser", "UserName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.BlogUser", "PersonalEmail", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlogUser", "PersonalEmail", c => c.String(nullable: false));
            AlterColumn("dbo.BlogUser", "UserName", c => c.String(nullable: false));
            RenameColumn(table: "dbo.BlogUser", name: "PersonalEmail", newName: "Email");
            RenameColumn(table: "dbo.BlogUser", name: "Id", newName: "UserId");
            RenameTable(name: "dbo.BlogUser", newName: "Users");
        }
    }
}
