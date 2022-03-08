using Chart_ex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Chart_ex.Controllers
{
    public class HtmlHelperController : Controller
    {
        // GET: HtmlHelper
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SampleHelpers()
        {
            User register = new User
            {
                Id = 1001,
                Name = "Kate",
                Nickname = "Jou",
                Email = "katejouXXXXX@gmail.com",
                City = 2,
                Terms = true
            };

            ViewData.Model = register;

            return View();
        }


        // 下面這個標簽是可寫可不寫，不寫就預設是 Get
        [HttpGet]
        public ActionResult SampleHelpers2()//ValidationMessage() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult SampleHelpers2(User user)//ValidationMessage(User user) 
        {
            if (ModelState.IsValid) 
            {
                // 如果兩個都有填，按下後，畫面只會有這兩個字
                return Content("成功！");
            }

            return View(user);
        }

        public ActionResult SampleHelpers3()
        {
            return View();
        }

        public ActionResult SampleHelpers4()
        {
            User register = new User
            {
                Id = 1001,
            };

            ViewData.Model = register;

            return View();

        }

        // 參考第二章 Scaffolding 和 CRUD
        // 不可以加這些 Post 標簽 ? 
        // 要用RedirectToAction 來 reutrn 
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(int? id) 
        {
            //return SampleHelpers4();
            // 可下中斷點，你就知道它回來過了。
            return RedirectToAction("SampleHelpers4");
        }

        public ActionResult Details(int? id)
        {
            return RedirectToAction("SampleHelpers4");
        }

        public ActionResult Edit(int? id)
        {
            return RedirectToAction("SampleHelpers4");
        }

        public ActionResult Delete(int? id)
        {
            return RedirectToAction("SampleHelpers4");
        }

        public ActionResult SampleHelpers5()
        {
            Editor_Using_Obj register = new Editor_Using_Obj
            {
                Id = 1001,
                Name = "Kate",
                Email = "katejouXXXXX@gmail.com",
                City = 2,
                Terms = true
            };

            ViewData.Model = register;

            return View();
        }

        [HttpPost]
        public ActionResult SampleHelpers5(Editor_Using_Obj obj)//ValidationMessage(User user) 
        {
            return View(obj);
        }

    }
}