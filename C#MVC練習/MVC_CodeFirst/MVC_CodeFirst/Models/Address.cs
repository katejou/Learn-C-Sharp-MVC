namespace MVC_CodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Number { get; set; }

        [Required]
        [StringLength(50)]
        public string Abbreviation { get; set; }

        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(1000)]
        public string CompanyAddress { get; set; }
    }
}
