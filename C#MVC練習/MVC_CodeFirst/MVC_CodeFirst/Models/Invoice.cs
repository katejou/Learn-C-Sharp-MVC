namespace MVC_CodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Seq { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeNum { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string PN { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string Desc_Good { get; set; }

        [StringLength(100)]
        public string DeclarationItem { get; set; }

        [StringLength(100)]
        public string HSCode { get; set; }

        [StringLength(100)]
        public string Brand { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(5)]
        public string COO { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SumPCS { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal UnitPrice { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal TotalValue { get; set; }
    }
}
