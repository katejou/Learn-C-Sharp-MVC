using System.ComponentModel.DataAnnotations;


namespace Light_System.Models
{
    public class Class1
    {

        [Required(ErrorMessage = "輸入照度必填")]
        [Display(Name = "輸入照度")]
        public int In_Lux { get; set; }

        [Required(ErrorMessage = "輸入色溫必填")]
        [Display(Name = "輸入色溫")]
        public int In_Kelvin { get; set; }

        [Display(Name = "目標照度")]
        public int Target_Lux { get; set; }

        [Display(Name = "目標色溫")]
        public int Target_Kelvin { get; set; }

        [Display(Name = "輸出流明")]
        public int Out_Lm { get; set; }

        [Display(Name = "輸出色溫")]
        public int Out_Kelvin { get; set; }

    }



}
