using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

namespace JQueryMobile_Ex
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 讀取這個 網站的預設 Display Mode 設定
            //var displayModes = DisplayModeProvider.Instance.Modes;

            #region 加入 iPhone 和 Android 的判斷
            //DisplayModeProvider.Instance.Modes.Insert(0,
            //    new DefaultDisplayMode("iPhone")
            //    {
            //        ContextCondition = (context =>
            //                            context.GetOverriddenUserAgent().IndexOf("iPhone", StringComparison.OrdinalIgnoreCase)
            //                            >= 0)
            //    }
            //);

            //DisplayModeProvider.Instance.Modes.Insert(1,
            //    new DefaultDisplayMode("Android")
            //    {
            //        ContextCondition = (context =>
            //                            context.GetOverriddenUserAgent().IndexOf("Android", StringComparison.OrdinalIgnoreCase)
            //                            >= 0)
            //    }
            //); 
            #endregion

            // 原本這個專案就有的︰
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // 加入了 jQuery.Mobile.MVC 套件之後，要設定的︰
            BundleMobileConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
