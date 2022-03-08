using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chart_ex.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "最少需 3 個字元！")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "需為9位數字")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "必填Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="必填所屬部門")]
        public string Department { get; set; }
        [Required(ErrorMessage = "必填職稱")]
        public string Title { get; set; }
    }
}