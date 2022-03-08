namespace MVC_CodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Head")]
    public partial class Head
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Seq { get; set; }

        public short ShipFrom { get; set; }

        [Required]
        [StringLength(100)]
        public string ShipFromCompany { get; set; }

        [Required]
        [StringLength(200)]
        public string ShipFromAddress { get; set; }

        public short ShipTo { get; set; }

        [Required]
        [StringLength(100)]
        public string ShipToCompany { get; set; }

        [Required]
        [StringLength(200)]
        public string ShipToAddress { get; set; }

        public short BillTo { get; set; }

        [StringLength(100)]
        public string BillToCompany { get; set; }

        [StringLength(200)]
        public string BillToAddress { get; set; }

        [Required]
        [StringLength(200)]
        public string InvoiceNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [StringLength(200)]
        public string Incoterm { get; set; }

        [StringLength(200)]
        public string Reference { get; set; }

        [StringLength(200)]
        public string DeclarationNo { get; set; }

        [StringLength(200)]
        public string TotalPackage { get; set; }

        [StringLength(200)]
        public string Attriubte1 { get; set; }

        [StringLength(200)]
        public string Attriubte2 { get; set; }

        [StringLength(200)]
        public string Attriubte3 { get; set; }

        [StringLength(200)]
        public string Attriubte4 { get; set; }

        [Required]
        [StringLength(10)]
        public string INVPdf { get; set; }

        [Required]
        [StringLength(10)]
        public string INVExcel { get; set; }

        [Required]
        [StringLength(10)]
        public string PKPdf { get; set; }

        [Required]
        [StringLength(10)]
        public string PKExcel { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime UpdatedDateTime { get; set; }
    }
}
