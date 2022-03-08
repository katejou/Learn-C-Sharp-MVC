using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirstNewDB.Models
{
    // 一張表格
    class User
    {

        // Primary Key
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        // Navigation Property
        public virtual ICollection<Blog> Blogs { get; set; }


    }
}
