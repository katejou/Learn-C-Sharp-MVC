using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // 只有下載(也不用Post回來，直接在前端用 <a> 解決)
        public ActionResult Query()
        {
            // 如果是多個檔名的話，要用到迴圈，你自己再想方法，但基本上就這樣簡單           
            ViewBag.DownloadLink = "../File/Fixed/Photo.jpg";
            return View();
        }

        // 只有新增
        public ActionResult New()
        {
            return View();
        }

        // 新增 : 我找到最簡單的Ajax上傳法，不涉及整個 Form
        // (如果是要多個檔，以一個Form來提交的話，你自己再想辨法吧…這個案子的需求不是這樣的。)
        public virtual ActionResult UploadFile()
        {
            // 參考︰https://dotblogs.com.tw/kkman021/2014/11/09/147243

            string fileName = "";

            //## 如果有任何檔案類型才做
            if (Request.Files.AllKeys.Any())
            {
                //## 讀取指定的上傳檔案ID
                var httpPostedFile = Request.Files["UploadedImage"];

                //## 真實有檔案，進行上傳
                if (httpPostedFile != null && httpPostedFile.ContentLength != 0)
                {
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(httpPostedFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/File"), fileName);
                    httpPostedFile.SaveAs(path);
                }
            }

            Thread.Sleep(5000); // 這個是讓前端的Loading有機會出來一下。

            //## 將結果回傳
            return Json(new { Status = "Upload Success", FileName = fileName });
        }


        // 刪增兼下載
        public ActionResult Alter()
        {
            // 做法︰
            // 找出所有資料夾中的檔案，列出來，旁附刪除按鈕

            // 找出所有資料夾中的檔案
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\File";
            DirectoryInfo d = new DirectoryInfo(path); //Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.jpg"); //Getting Text files

            // 串連檔名以送到前端(不附合你的需求，請自行改編)
            List<string> fns = new List<string>();
            foreach (FileInfo file in Files)
                fns.Add(file.Name);

            // 送到前端
            ViewBag.FileNames = fns;
            return View();
        }

        // 傳送檔名用物件
        public class FileNameObj
        {
            public string Name { get; set; }
        }

        // 刪除檔案
        public ActionResult DeleteFile(FileNameObj fnObj)
        {
            // 如果有副檔名(如.jpg)的話，不能POST進來，因為會Routing出現問題。所以用個OBJ裝起來。
            string fn = fnObj.Name;

            // 找出檔案 + 刪除
            string path = AppDomain.CurrentDomain.BaseDirectory + @"File\" + fn;
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            // 將結果回傳
            return Json(new { Status = "Delete Success" });
        }

    }
}