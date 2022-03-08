using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bootstrap_Ex.Models;

namespace Bootstrap_Ex.Controllers
{
    public class StudentController : Controller
    {
        // 引入 全網站可用的 Razar Helper ? 
        



        protected List<Student> students = new List<Student>
        {
            new Student{ Id=1,Name="Joe",Chinese=88,English=95,Math=71},
            new Student{ Id=2,Name="Mary",Chinese=92,English=82,Math=60},
            new Student{ Id=3,Name="Cathy",Chinese=98,English=91,Math=94},
            new Student{ Id=4,Name="John",Chinese=63,English=85,Math=55},
            new Student{ Id=5,Name="David",Chinese=59,English=77,Math=82}
        };


        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Scores()
        {
            return View(students);
        }

        public ActionResult ScoresSum()
        {
            int topId = 0;
            int topScore = 0;

            foreach (var student in students) 
            {
                //計算每個學生的總分
                student.Total = student.Chinese + student.English + student.Math;

                //判斷總分最高者
                if (student.Total > topScore) 
                {
                    topScore = student.Total;
                    topId = student.Id;
                }
            
            }

            // 將最高得分者的 Id 存起來
            ViewBag.TopId = Convert.ToInt32(topId);

            return View(students);
        }

        public ActionResult ScoresSum_helper()
        {
            foreach (var student in students)
            {
                //計算每個學生的總分
                student.Total = student.Chinese + student.English + student.Math;
            }
            return View(students);
        }

        public ActionResult GlobalHtmlHelper()
        {
            int topId = 0;
            int topScore = 0;

            foreach (var student in students)
            {
                //計算每個學生的總分
                student.Total = student.Chinese + student.English + student.Math;

                //判斷總分最高者
                if (student.Total > topScore)
                {
                    topScore = student.Total;
                    topId = student.Id;
                }
            }

            // 將最高得分者的 Id 存起來
            ViewBag.TopId = Convert.ToInt32(topId);

            return View(students);
        }
    }
}