namespace jQueryMobile_Ex2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentsData1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Gender");
        }
    }
}
