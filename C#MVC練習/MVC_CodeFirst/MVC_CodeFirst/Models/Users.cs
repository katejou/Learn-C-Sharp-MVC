namespace MVC_CodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BlogUser")]
    public partial class Users
    {
        
        public Users()
        {
            Blog = new HashSet<Blog>();
        }

        [Column("Id")]
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="Name¥²¶ñ")]
        public string UserName { get; set; }

        [Required,StringLength(255)]
        [Column("PersonalEmail")]
        public string Email { get; set; }

        public virtual ICollection<Blog> Blog { get; set; }
    }
}
