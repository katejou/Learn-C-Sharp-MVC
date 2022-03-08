using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitTest_Ex_1.Models;

namespace UnitTest_Ex_1.Controllers
{
    public class MakeUpController : Controller
    {
        // GET: MakeUp
        public ActionResult MakeUpList()
        {
            List<MakeUp> mk = new List<MakeUp>
            {
                new MakeUp{ Id=1,sth="bullshit"}
            };

            return View("MakeUpList", mk);
        }

        public ActionResult MakeUpList_Search_by_ID(int? Id)
        {
            if (Id == null)
            {
                // 結果一
                var result = HttpNotFound("Id is illeagal");
                return result;
            }

            List<MakeUp> mk = new List<MakeUp>
            {
                new MakeUp{ Id = 1 , sth = "bullshit"}
            };

            var get_mk = mk.Where(x => x.Id == Id);

            if (get_mk.Count() == 0)
            {
                // 結果二(找不到)
                return RedirectToAction("Index", "Home");
            }

            // 結果三
            return View("MakeUpList", get_mk.ToList());
        }



    }
}