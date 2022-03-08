using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC.Models;

namespace MyFirstMVC.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult EmployeeList()
        {
            List<Employee> employees = new List<Employee> {
                    new Employee { ID = 10001, Name = "Apple0", Phone = "0000", Email = "0000@mail.com" },
                    new Employee { ID = 10002, Name = "Ben1", Phone = "0001", Email = "0001@mail.com" },
                    new Employee { ID = 10003, Name = "Cathy2", Phone = "0020", Email = "0020@mail.com" },
                    new Employee { ID = 10004, Name = "David3", Phone = "0300", Email = "0300@mail.com" }
            };
            if (TempData.ContainsKey("NewEmployee"))
            {
                employees.Add((Employee)TempData["NewEmployee"]);
                //TempData.Keep();
                //TempData.Keep("NewEmployee");
            }
            return View(employees);
        }
        public ActionResult Create()
        {
            TempData["NewEmployee"] = new Employee { ID = 10005, Name = "NewEmployee", Phone = "4000", Email = "4000@mail.com" };
            return RedirectToAction("EmployeeList"); // 如果是同一個Controller 不可以加入自己Cotorller的名字！
            // return RedirectToAction("EmployeeList" , "EmployeesController");  <-- 會出錯！
        }

        public ActionResult OneEmployee() 
        {
            return View(new Employee { ID = 10001, Name = "Apple0", Phone = "0000", Email = "0000@mail.com" });
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}