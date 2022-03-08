using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EF_CodeFirstNewDB.Models
{
    // 在 namespace 中的 using ( 記得先 nuget EntityFramework )
    using System.Data.Entity;

    // 繼承 DbContext 代表 它等如一個資料庫的作業環境
    class BlogContext : DbContext
    {
        public BlogContext() : base("BlogContext")
        { }


        // 資料行 = Entity (實體)
        // Entity Sets = 資料行(實體)集 = 從資料庫「暫時存取出來」的結果 = 每Set對應一張表格 
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

    }
}
