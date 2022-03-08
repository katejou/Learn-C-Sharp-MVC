using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyFirstMVC.Models
{
    public class Employee
    {
        [Display(Name = "員編")]
        public int ID { get; set; }
        [Display(Name = "名稱")]
        public string Name { get; set; }
        [Display(Name = "電話")]
        public string Phone { get; set; }
        [Display(Name = "電郵")]
        public string Email { get; set; }
    }
}