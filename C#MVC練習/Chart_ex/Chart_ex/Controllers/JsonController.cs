using Chart_ex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chart_ex.Controllers
{
    public class JsonController : Controller
    {

        public ActionResult LineTemperatureJSON()
        {
            string[] Labels = { "1月", "2月", "3月", "4月"};

            string jsonStr1 = Newtonsoft.Json.JsonConvert.SerializeObject(Labels);

            // Array[] 會化作 js Array (只有一維)
            ViewBag.jsLabels = jsonStr1;

            List<City> Locations = new List<City>
            {
                new City
                {
                    Name="台北",
                    Temperature=new double[]{ 1,2,3,4}
                },
                new City
                {
                    Name="台中",
                    Temperature=new double[]{ 1.5,0.5,2.5,2 }
                },
                new City
                {
                    Name="台南",
                    Temperature=new double[]{ 4,3,2,1 }
                },
            };

            // List 會化作 js Array (這個列子有兩維)
            string jsonStr2 = Newtonsoft.Json.JsonConvert.SerializeObject(Locations);

            ViewBag.jsLocations = jsonStr2;

            return View(Locations); //<-- 這個做文字顯示

        }

        public ActionResult CarSalesAjaxJSON()
        {
            return View(); //<-- 這個做文字顯示
        }

    }
}