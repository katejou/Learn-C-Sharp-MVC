using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Login_ex.Controllers
{
    //帳密︰ abc@edf.com  和 123456789Aa*
    //帳密︰ Kate@123.com  和 Kate@123.com

    [Authorize]
    public class HomeController : Controller
    {

        //[Authorize]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Users = "abc@edf.com, xxx@yyy.com")]
        public ActionResult About()
        {
            // 以下判斷和標簽的功能相同，請擇一而測試
            if (User.Identity.Name != "abc@edf.com")
            {
                return Content($"{User.Identity.Name}帳號無權限！");
            }

            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize(Roles = "Admin, XX_Group")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}