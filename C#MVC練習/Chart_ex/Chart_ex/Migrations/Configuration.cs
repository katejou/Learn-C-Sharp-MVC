namespace Chart_ex.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Chart_ex.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Chart_ex.Models.CmsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Chart_ex.Models.CmsContext context)
        {
            // x => x.Id 是設定唯一值還是什麼意思？
            context.Employees.AddOrUpdate(
                x => x.Id,
                new Employee { Id = 1, Name = "David", Mobile = "123456789", Email = "David@hotmail.com", Department = "總經理室", Title = "CEO" },
                new Employee { Id = 2, Name = "Mary", Mobile = "234567891", Email = "Mary@hotmail.com", Department = "人事部", Title = "HR" },
                new Employee { Id = 3, Name = "Joe", Mobile = "345678912", Email = "Joe@hotmail.com", Department = "財務部", Title = "Manager" },
                new Employee { Id = 4, Name = "Mark", Mobile = "456789123", Email = "Mark@hotmail.com", Department = "業務部", Title = "Sales" },
                new Employee { Id = 5, Name = "Rose", Mobile = "567891234", Email = "Rose@hotmail.com", Department = "資訊部", Title = "Lion" }
             );

            context.Registers.AddOrUpdate(
                x => x.Id,
                new Register
                {
                    Id = 1,
                    Name = "登記者=管理員？",
                    Nickname = "我本人",
                    Password = "我的密碼",
                    Email = "me@hotmail.com",
                    City = 4,
                    Gender = 2,
                    Commutermode = "等車",
                    Comment = "沒有",
                    Terms = true
                }

            );


        }
    }
}
