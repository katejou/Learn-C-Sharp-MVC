using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jQueryMobile_Ex2.Models;

namespace jQueryMobile_Ex2.Controllers
{
    public class CarsController : Controller
    {
        // GET: Cars
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CarsList()
        {
            return View();
        }

        public ActionResult Repair()
        {
            return View();
        }

        public ActionResult Repair2()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        MobileContext db = new MobileContext();
        public ActionResult CarsListDB()
        {
            var heros = db.Heroes.OrderBy(x => x.Id).ToList();
            return View(heros);
        }


    }
}