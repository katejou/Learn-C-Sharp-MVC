namespace MVC_CodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Box")]
    public partial class Box
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Seq { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BoxNum { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal NW { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal GW { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string Size { get; set; }
    }
}
