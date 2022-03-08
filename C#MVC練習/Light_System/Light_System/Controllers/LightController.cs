using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Light_System.Models;

namespace Light_System.Controllers
{
    public class LightController : Controller
    {
        // GET: Light
        public ActionResult Balance()
        {
            Class1 class1 = new Class1
            {
                Target_Lux = 40000,
                Target_Kelvin = 6000
            };

            ViewData.Model = class1;
            return View();
        }

        // Post 回來，再回傳計算好的答案
        [HttpPost]
        public ActionResult Balance(Class1 class1)//ValidationMessage(User user) 
        {
            if (ModelState.IsValid)
            {
                ViewData.ModelState.Clear(); // 要清除頁面，否則新的值也顯示不出來。
                class1.Out_Kelvin = class1.Target_Kelvin - class1.In_Kelvin;
                class1.Out_Lm = class1.Target_Lux - class1.In_Lux;
                return View(class1);
            }

            return View(class1);
        }



    }
}