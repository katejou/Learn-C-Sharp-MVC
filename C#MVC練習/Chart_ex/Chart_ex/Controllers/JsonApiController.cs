using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Chart_ex.Models;
//using Chart_ex.Helpers;

namespace Chart_ex.Controllers
{
    public class JsonApiController : Controller
    {
        //// GET: JsonApi
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult getCarSalesNumber()
        {
            List<Car> liCar = new List<Car>
            {
                new Car{ ID=1, Brand = "BMW", Sales = new int[]{ 1, 2, 3 } },
                new Car{ ID=2, Brand = "BENZ", Sales = new int[]{ 3, 2, 1 } }
            };

            return Json(liCar,JsonRequestBehavior.AllowGet);
        }

        public ActionResult getCarSalesNumber2()
        {
            List<Car> liCar = new List<Car>
            {
            new Car{ ID=3, Brand = "Audi", Sales = new int[]{ 4,5,6 } },
            new Car{ ID=4, Brand = "Lexus", Sales = new int[]{ 6,5,4 } }
            };

            return Json(liCar, JsonRequestBehavior.AllowGet);

        }

        public ActionResult getTemperature()
        {
            List<City> liCity = new List<City>
            {
            new City{ Name="北",Temperature= new double[]{ 12.3, 45.6, 78.9} },
            new City{ Name="南",Temperature= new double[]{ 98.7, 65.4, 12.3} }
            };
            return Json(liCity, JsonRequestBehavior.AllowGet);
        }



    }
}