using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UnitTest_Ex_1.Controllers;
using UnitTest_Ex_1.Models;


namespace UnitTest_Ex_1.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MakeUpList_Return_isNot_Null()
        {
            MakeUpController controller = new MakeUpController();
            ViewResult result = controller.MakeUpList() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MakeUpList_Return_ManyPossible()
        {
            MakeUpController controller = new MakeUpController();

            var result_1 = controller.MakeUpList_Search_by_ID(null) as HttpNotFoundResult;
            var result_2 = controller.MakeUpList_Search_by_ID(2) as RedirectToRouteResult;
            var result_3 = controller.MakeUpList_Search_by_ID(1) as ViewResult;

            Assert.IsInstanceOfType(result_1,typeof(HttpNotFoundResult));
            Assert.IsInstanceOfType(result_2,typeof(RedirectToRouteResult));
            Assert.IsInstanceOfType(result_3,typeof(ViewResult));

        }

        [TestMethod]
        public void Check_Controller_Action_Name()
        {
            MakeUpController controller = new MakeUpController();

            var result_2 = controller.MakeUpList_Search_by_ID(2) as RedirectToRouteResult;

            Assert.AreEqual("Home",result_2.RouteValues["controller"]);
            Assert.AreEqual("Index", result_2.RouteValues["action"]);
        }

        [TestMethod]
        public void Check_Count()
        {
            MakeUpController controller = new MakeUpController();

            var result_3 = controller.MakeUpList_Search_by_ID(1) as ViewResult;
            //var result_3 = controller.MakeUpList() as ViewResult;
            // 上面兩個都可以。
            int count = (result_3.Model as List<MakeUp>).Count();

            Assert.AreEqual(1, count);
        }

    }
}
