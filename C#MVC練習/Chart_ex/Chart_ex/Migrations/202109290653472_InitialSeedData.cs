namespace Chart_ex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    

    // 這個是異動的紀錄檔？檔名記下了時間和這次異動的名稱「InitialSeedData」

    public partial class InitialSeedData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        Department = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Registers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nickname = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Gender = c.Int(nullable: false),
                        City = c.Int(nullable: false),
                        Commutermode = c.String(),
                        Comment = c.String(),
                        Terms = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Registers");
            DropTable("dbo.Employees");
        }
    }
}
