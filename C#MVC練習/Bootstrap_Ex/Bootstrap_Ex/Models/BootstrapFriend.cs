using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bootstrap_Ex.Models
{
    // 欄目以中文顯示︰

    public class BootstrapFriend
    {
        [Display(Name="號碼" )]
        public int ID { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "電話")]
        public string Phone { get; set; }
    }
}