using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace EF_CodeFirstNewDB.Models
{
    // 一張表格
    class Blog
    {
        // Primary Key
        public int BlogId { get; set; }
        public string BlogName { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }

        // Navigation Property
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<Post> Post { get; set; }

    }
}
