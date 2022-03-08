using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bootstrap_Ex.Models;

namespace Bootstrap_Ex.Controllers
{
    public class PartialView_2Controller : Controller
    {
        // GET: PartialView
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SimpleCard()
        {
            return View();
        }

        public ActionResult SimpleCard_Model()
        {
            List<Card> cards = new List<Card>
            {
                new Card { Name="夏油傑",Brief="咒靈操使",Photo="xyj.jpg", Url="https://zh.wikipedia.org/wiki/%E5%92%92%E8%A1%93%E8%BF%B4%E6%88%B0%E8%A7%92%E8%89%B2%E5%88%97%E8%A1%A8" },
                new Card { Name="五條悟",Brief="六眼",Photo="5t5.jpg", Url="https://zh.wikipedia.org/wiki/%E5%92%92%E8%A1%93%E8%BF%B4%E6%88%B0%E8%A7%92%E8%89%B2%E5%88%97%E8%A1%A8" }
            };

            return View(cards);
        }
    }
}