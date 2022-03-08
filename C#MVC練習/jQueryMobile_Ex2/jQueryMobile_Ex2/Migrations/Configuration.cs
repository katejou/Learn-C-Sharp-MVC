namespace jQueryMobile_Ex2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using jQueryMobile_Ex2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<jQueryMobile_Ex2.Models.StudentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(jQueryMobile_Ex2.Models.StudentContext context)
        {
            context.Students.AddOrUpdate(
                new Student { Id = 1, Name = "禪院真依", House = "禪院", Age = 16, Gender = "F", Grade = 2, Rank = 5 },
                new Student { Id = 2, Name = "加茂憲紀", House = "加茂", Age = 17, Gender = "M", Grade = 3, Rank = 3 },
                new Student { Id = 3, Name = "東堂葵", House = "東堂", Age = 16, Gender = "M", Grade = 3, Rank = 1 },
                new Student { Id = 4, Name = "西宮桃", House = "西宮", Age = 17, Gender = "F", Grade =3, Rank = 4 },
                new Student { Id = 5, Name = "與幸吉", House = "與", Age = 16, Gender = "M", Grade = 2, Rank = 2 },
                new Student { Id = 6, Name = "三輪霞", House = "三輪", Age = 16, Gender = "F", Grade = 2, Rank = 6 }

                );
        }
    }
}
