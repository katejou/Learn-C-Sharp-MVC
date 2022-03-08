using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirstNewDB.Models
{
    // 一張表格
    class Post
    {
        // Primary Key
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }

        //Navigation Property
        public virtual Blog Blog { get; set; }

    }
}
