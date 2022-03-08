using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace jQueryMobile_Ex2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 1. 最基本的網站入口(詳列所有京都校的學生)
            routes.MapRoute(
            name: "Kyoto",
            url: "Kyoto",
            defaults: new { controller = "SisterSchool", action = "Index" }
            );


            // 2. 用網址去查詢男或女學生
            routes.MapRoute(
            name: "FindKyotoByGender",
            url: "Kyoto/Gender/{gender}",
            defaults: new
            {
                controller = "SisterSchool",
                action = "FindGender",
                gender = UrlParameter.Optional
            }
            );

            // 3. 用網址去查詢學生的姓氏，預設為禪院家
            routes.MapRoute(
            name: "FindKyotoByHouse",
            url: "Kyoto/House/{house}",
            defaults: new
            {
                controller = "SisterSchool",
                action = "FindHouse",
                house = "禪院"
            }
            );

            // 4. 用網址去查詢不同年級的學生(進入到Action才示範怎樣顯示沒有輸入參數的後果。)
            routes.MapRoute(
            name: "FindKyotoByGrade",
            url: "Kyoto/Grade/{grade}",
            defaults: new
            {
                controller = "SisterSchool",
                action = "FindGrade",
                grade = UrlParameter.Optional
            }
            );

            // 5. 用網址輸入參數的格式限制，用歳數來搜尋京都校學生
            routes.MapRoute(
            name: "FindKyotoByAge",
            url: "Kyoto/Age/{age}",
            defaults: new
            {
                controller = "SisterSchool",
                action = "FindAge",
                age = 16
            },
                constraints: new { age = @"\d{2}" }
            );

            // 6. 用網址輸入「多個」參數的格式限制，用「年級和男女」來搜尋京都校學生
            routes.MapRoute(
            name: "FindKyotoByGradeGender",
            url: "Kyoto/Grade-Gender/{grade}-{gender}",
            defaults: new
            {
                controller = "SisterSchool",
                action = "FindGradeGender"
            },
                constraints: new { grade = @"\d{1}", gender = @"[M|F]{1}" }
            );

            // 7. 以實力排名來搜尋京都校學生
            routes.MapRoute(
            name: "FindKyotoByRank",
            url: "Kyoto/Rank/{rank}",
            defaults: new
            {
                controller = "SisterSchool",
                action = "FindRank",
                rank = 3
            },
                constraints: new { rank = @"[1-6]{1}" }
            );

            // 8. 由 Controller 和 View 讀取路由資訊
            routes.MapRoute(
            name: "GetRouteData",
            url: "Kyoto/Route/{p1}-{p2}",
            defaults: new
            {
                controller = "SisterSchool",
                action = "GetRouteData",
                p1 = UrlParameter.Optional,
                p2 = UrlParameter.Optional
            }
            );


            // 預設的，因為其他和路由無關的實作，也需要它，所以不要動
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
