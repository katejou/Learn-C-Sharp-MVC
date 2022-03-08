namespace MVC_CodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        [Key]
        public int Log_Seq { get; set; }

        [Required]
        [StringLength(50)]
        public string Who { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Time { get; set; }

        [Required]
        [StringLength(10)]
        public string Action { get; set; }

        public int Seq { get; set; }
    }
}
