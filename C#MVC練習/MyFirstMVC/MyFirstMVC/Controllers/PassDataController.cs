using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Controllers
{
    public class PassDataController : Controller
    {
        // GET: PassData
        public ActionResult PetShop()
        {
            ViewData["Company"] = "汪星人竉物店";
            ViewBag.Address = "我家附近";
            List<string> petList = new List<string>();
            petList.Add("汪");
            petList.Add("喵");
            petList.Add("泡泡");
            petList.Add("吱");
            petList.Add("呱");
            
            ViewData.Model = petList;
            return View();
        
            //return View(petList);
        
        }
    }
}