using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jQueryMobile_Ex2.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public string PageId { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Intro { get; set; }
        public string ImgUrl { get; set; }
        public string ImgAlt { get; set; }
        public string VideoUrl { get; set; }
    }
}