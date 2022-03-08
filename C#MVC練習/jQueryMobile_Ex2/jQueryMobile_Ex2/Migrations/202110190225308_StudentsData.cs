namespace jQueryMobile_Ex2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentsData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        House = c.String(),
                        Grade = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
