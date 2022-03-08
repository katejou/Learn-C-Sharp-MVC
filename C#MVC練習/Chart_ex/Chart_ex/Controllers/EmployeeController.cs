using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Chart_ex.Models;
using System.Net;

namespace Chart_ex.Controllers
{
    public class EmployeeController : Controller
    {

        private CmsContext db = new CmsContext();

        // GET: Employee
        public ActionResult Index()
        {
            // 從資料庫取資料
            var emps = db.Employees.ToList();
            return View(emps);
        }

        // GET
        public ActionResult Details(int? Id)
        {
            // 防沒有傳入值
            if (Id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee emp = db.Employees.Find(Id);

            // 防沒有找到員工
            if (emp == null) 
            {
                return HttpNotFound();
            }

            return View(emp);

        }

        // GET
        public ActionResult Create() 
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Mobile,Email,Department,Title")] Employee emp) 
        {
            //是否通過驗證(雖然我在這個範例沒有設)
            if (ModelState.IsValid) 
            {
                // 存到資料庫
                db.Employees.Add(emp);
                db.SaveChanges();
                //儲存完成後，導向Index
                return RedirectToAction("Index");
            }

            // 不通過驗證，原樣返回，參考之前的例子，還可以顯示不通過的原因訊息
            return View(emp);
        }

        // GET
        public ActionResult Edit(int? Id) 
        {
            // 防沒有傳入值
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee emp = db.Employees.Find(Id);

            // 防沒有找到員工
            if (emp == null)
            {
                return HttpNotFound();
            }

            return View(emp);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Mobile,Email,Department,Title")] Employee emp) 
        {


            //是否通過驗證(雖然我在這個範例沒有設)
            if (ModelState.IsValid)
            {
                // 修改資料庫
                db.Entry(emp).State = EntityState.Modified;
                // 就這麼簡單，連 Update 指令都省了
                db.SaveChanges();
                //儲存完成後，導向Index
                return RedirectToAction("Index");
            }

            // 不通過驗證，原樣返回，參考之前的例子，還可以顯示不通過的原因訊息
            return View(emp);

        }

        // Get
        public ActionResult Delete(int? Id)
        {
            // 防沒有傳入值
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee emp = db.Employees.Find(Id);

            // 防沒有找到員工
            if (emp == null)
            {
                return HttpNotFound();
            }

            return View(emp);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Delete(int Id) 
        {

            Employee emp = db.Employees.Find(Id);
            db.Employees.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}