using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Controllers
{
    public class ProductsNewController : Controller
    {
        // GET: Products
        public ActionResult Index_NEW()
        {
            return View();
        }
    }
}