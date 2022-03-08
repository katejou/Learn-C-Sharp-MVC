using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jQueryMobile_Ex2.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Gender { get; set; }
        public string House { get; set; }
        public int Grade { get; set; }
        public int Age { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }

    }
}