using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chart_ex.Models;
namespace Chart_ex.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LineTemp()
        {
            return View();
        }
        public ActionResult LineTemp_Data()
        {
            string[] Months = { "January", "February", "March", "April", "May", "June" };

            ViewBag.MonthsLabel = Months;

            List<City> cities = new List<City>()
            {
                new City { Name="台北", Temperature=new double[]{ 16, 15, 18, 21, 25, 27 } },
                new City { Name="台中", Temperature=new double[]{ 18, 5, 30, 21, 10, 24 } },
                new City { Name="台南", Temperature=new double[]{ 26, 10, 8, 15, 20, 7 } }
            };

            return View(cities);
        }

        public ActionResult Bars()
        {
            return View();
        }
        public ActionResult Bars_Data()
        {
            string[] Months = { "January", "February", "March", "April", "May", "June" };
            int[] temperature = { 10, 5, 18, 21, 12, 27 };
            ViewBag.Months = Months;
            ViewBag.Temperature = temperature;
            return View();
        }
        public ActionResult Radar_Data()
        {
            string[] Months = { "January", "February", "March", "April", "May", "June" };
            int[] temperature1 = { 10, 5, 18, 21, 12, 27 };
            int[] temperature2 = { 25, 15, 10, 7, 20, 4 };

            ViewBag.Months = Months;
            ViewBag.temperature1 = temperature1;
            ViewBag.temperature2 = temperature2;

            return View();
        }

        public ActionResult Pie_Data()
        {
            string[] Months1 = { "January", "February", "March", "April", "May", "June" };
            int[] temperature1 = { 10, 5, 18, 21, 12, 27 };
            string[] Months2 = { "July", "August", "September", "October", "November", "December" };
            int[] temperature2 = { 25, 15, 10, 7, 20, 4 };

            ViewBag.Months1 = Months1;
            ViewBag.Months2 = Months2;
            ViewBag.temperature1 = temperature1;
            ViewBag.temperature2 = temperature2;

            return View();
        }


    }
}