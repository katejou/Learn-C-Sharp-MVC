using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using jQueryMobile_Ex2.Models;

namespace jQueryMobile_Ex2.Controllers
{
    public class SisterSchoolController : Controller
    {
        StudentContext db = new StudentContext();
        List<Student> students = null;

        // 編號對應路由

        // 1.
        [OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient, VaryByParam = "none")]
        public ActionResult Index()
        {
            List<Student> students = null;
            ViewBag.Header = "列出所有學生";
            students = (from s in db.Students select s).OrderBy(x => x.Id).ToList();
            return View(students);
        }

        // 2.
        public ActionResult FindGender(string gender)
        {

            //// https://forums.asp.net/t/1832908.aspx?C+how+to+check+char+is+null+or+empty
            //// There's no such thing as an empty char. The closest you can get is '\0' ...
            //if (gender == '\0')
            //{
            //    ViewBag.Header = "列出所有性別的學生";
            //    students = (from s in db.Students select s).OrderBy(x => x.Grade).ToList();
            //    return View(students);
            //}
            //else if (gender != 'M' && gender != 'F') 
            //{
            //    // 沒有性別的學生
            //    return Content("目前沒有男女以外的性別");
            //}
            //else
            //{
            //    ViewBag.Header = $"只列 {gender} 性別學生"; 
            //    students = (from s in db.Students
            //                where s.Gender == gender
            //                select s).OrderBy(x => x.Grade).ToList();
            //    return View(students);
            //}

            if (string.IsNullOrEmpty(gender))
            {
                ViewBag.Header = "列出所有性別的學生";
                students = (from s in db.Students select s).OrderBy(x => x.Grade).ToList();
                return View(students);
            }
            else if (gender != "M" && gender != "F")
            {
                // 沒有性別的學生
                return Content("目前沒有男女以外的性別");
            }
            else
            {
                ViewBag.Header = $"只列 {gender} 性別學生";
                students = (from s in db.Students
                            where s.Gender == gender
                            select s).OrderBy(x => x.Grade).ToList();
                return View(students);
            }

        }

        // 3.
        public ActionResult FindHouse(string house)
        {
            ViewBag.Header = "查詢學生姓氏";

            if (string.IsNullOrEmpty(house))
            {
                return Content(@"已預設了不填就是姓禪院，
                                所以理論上不可能到這裡。
                                除非你是由預設路由︰SisterSchool/FindHouse
                                進入的。");
            }

            students = (from s in db.Students
                        where s.House == house
                        select s).ToList();

            if (students.Count == 0)
                return Content("查無此姓氏");
            else
                return View(students);

        }

        // 4.
        public ActionResult FindGrade(int? grade)
        {
            ViewBag.Header = "查詢學生年級";

            if (grade == null)
            {
                return Content(@"請提供年級");
            }

            students = (from s in db.Students
                        where s.Grade == grade
                        select s).ToList();

            if (students.Count == 0)
                return Content("查無此年級");
            else
                return View(students);
        }

        // 5.
        public ActionResult FindAge(int? age)
        {
            ViewBag.Header = "查詢學生年紀";

            if (age == null)
            {
                return Content(@"年紀有格式限制，要兩個整數，
                                預設了不填就 16 。
                                所以除非預設路由，否則也還是不會到這裡來？");
            }

            students = (from s in db.Students
                        where s.Age == age
                        select s).ToList();

            if (students.Count == 0)
                return Content("查無此年紀");
            else
                return View(students);


        }

        // 6.
        public ActionResult FindGradeGender(int? grade, string gender)
        {
            ViewBag.Header = "查詢學生 年級和男女 ";

            students = (from s in db.Students
                        where s.Grade == grade && s.Gender == gender
                        select s).ToList();

            if (students.Count == 0)
                return Content("查無此 年級和男女");
            else
                return View(students);
        }

        // 7.
        public ActionResult FindRank(int rank)
        {
            ViewBag.Header = "查詢學生實力排名";
            //orderby s.Rank descending
            students = (from s in db.Students
                        orderby s.Rank
                        select s).Take(rank).ToList();

            return View(students);
        }

        // 8. 
        public ActionResult GetRouteData(string p1, string p2)
        {
            // 讀，請求(Request) URL 的 三種方法
            var RawUrl_1 = Request.RawUrl;
            var RawUrl_2 = HttpContext.Request.RawUrl;
            var RawUrl_3 = ControllerContext.RequestContext.HttpContext.Request.RawUrl;

            // 讀取路由的 Pattern 設定值。
            var route = RouteData.Route;
            var UrlPattern = route.GetType().GetProperty("Url").GetValue(route);
            // ? 所以這UrlPattern又不是string，是可以用來幹什麼？

            // 讀取路由中，關於控制器的描述的參數
            var controller_1 = RouteData.Values["controller"];
            var action_1 = RouteData.Values["action"];
            var routeParameter_1 = RouteData.Values["RouteParam"];

            var controller_2 = ControllerContext.RouteData.Values["controller"];
            var action_2 = ControllerContext.RouteData.Values["action"];
            var routeParameter_2 = ControllerContext.RouteData.Values["RouteParam"];

            // 不傳入View()，只在這裡下個中斷點來看，因為View之中，還有另外一方法來看以上的值
            // 所以以上的這些方法，只是當控制器需要這些路由資訊時。才會使用。

            return View();

            // 書中也建議將這個相應的View，建設為PartialView("XXXX");
            // 那日後測試其他Action時，只要把它們的 return 改為
            // PartialView("XXXX"); 就好了，測完之後，又可以簡單的註解起來。

        }


    }
}