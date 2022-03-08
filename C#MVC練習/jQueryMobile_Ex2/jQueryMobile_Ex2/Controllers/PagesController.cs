using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jQueryMobile_Ex2.Models;

namespace jQueryMobile_Ex2.Controllers
{
    public class PagesController : Controller
    {
        // GET: Pages
        public ActionResult SinglePage()
        {
            ViewBag.Header = "咒迴人物";
            ViewBag.Footer = "釘崎野薔薇";
            ViewBag.Image = "/Content/images/hero/rose.jpg";
            ViewBag.Intro = @"釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武
                              釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武
                              釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武
                              釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武
                              釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武
                              釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武
                              釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武
                              釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武釘哥威武
                              ";

            return View();
        }

        public ActionResult MutiPages()
        {
            List<Hero> Heroes = new List<Hero>
            {
                new Hero{ Id=1, PageId="page_uta",
                    Header = "乙骨優太",
                    Footer = "戀童？中二病",
                    Intro = "純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神",
                    ImgUrl="../Content/images/hero/uta.jpg",
                    ImgAlt="優太",
                    VideoUrl="https://www.youtube.com/embed/Z7XaSSrN5_8"
                },
                new Hero{ Id=2, PageId="page_rika",
                    Header = "祈本里香",
                    Footer = "死了都被詛咒的可憐女孩",
                    Intro = " 特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈",
                    ImgUrl="../Content/images/hero/rika.jpg",
                    ImgAlt="里香",
                    VideoUrl="https://www.youtube.com/embed/hgcncB05Qzk"
                }

            };

            return View(Heroes);

        }


        public ActionResult MutiPages_Partial()
        {
            List<Hero> Heroes = new List<Hero>
            {
                new Hero{ Id=1, PageId="page_uta",
                    Header = "乙骨優太",
                    Footer = "戀童？中二病",
                    Intro = "純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神純愛戰神",
                    ImgUrl="../Content/images/hero/uta.jpg",
                    ImgAlt="優太",
                    VideoUrl="https://www.youtube.com/embed/Z7XaSSrN5_8"
                },
                new Hero{ Id=2, PageId="page_rika",
                    Header = "祈本里香",
                    Footer = "死了都被詛咒的可憐女孩",
                    Intro = " 特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈特級過怨咒靈",
                    ImgUrl="../Content/images/hero/rika.jpg",
                    ImgAlt="里香",
                    VideoUrl="https://www.youtube.com/embed/hgcncB05Qzk"
                }

            };

            return View(Heroes);

        }
       
        public MobileContext db = new MobileContext();

        public ActionResult MutiPages_Partial_DB()
        {
            return View(db.Heroes.ToList());
        }


    }
}