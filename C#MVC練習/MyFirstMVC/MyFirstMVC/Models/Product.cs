using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
    }
}