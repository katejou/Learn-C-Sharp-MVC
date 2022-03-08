using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Chart_ex.Models
{
    // 負責管理 Entity 物件資料
    public class CmsContext : DbContext 
    {
        // 建構子，利用 Web.config 中設定好的連線
        public CmsContext() : base("CmsDbConnection") { }

        // 查詢和儲存Entity個體資料，使用時，可以接上 .Find  .ToList 什麼的…
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Register> Registers { get; set; }

    }

}