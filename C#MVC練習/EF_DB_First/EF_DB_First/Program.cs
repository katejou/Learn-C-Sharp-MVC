using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DB_First
{
    class Program
    {
        static void Main(string[] args)
        {
            // 因為我沒有掛到哪裡，所以每一次跑完Main
            // 資料庫的內容都會回歸原本，沒有更改。

            Query_1();
            Query_2();
            Update();
            Create();
            Delete();

            // 結尾 (後移)
            Console.WriteLine("\n\n按任意鍵離開...");
            Console.ReadKey();

        }

        static private void Query_1() 
        {
            // 初始化 (？外加開始連線？)
            var db = new NorthWindContext();

            // 查詢
            var products = from p in db.Products select p;

            // 開頭
            Console.WriteLine("\n 產品資訊如下︰ \n");

            // 逐筆顯示
            foreach (var p in products.Take(5))
                Console.WriteLine($" {p.ProductID} , {p.ProductName} , {p.UnitsInStock} ");

            // 結尾 (後移)
            //Console.WriteLine("按任意鍵離開...");
            //Console.ReadKey();

            // 關閉連線
            db.Dispose();
        }

        static private void Query_2() 
        {
            // Query
            Console.WriteLine($"\n using  --- Query : \n");

            using (var db1 = new NorthWindContext())
            {
                var products_1 = from p in db1.Products select p;
                foreach (var p in products_1.Take(5))
                    Console.WriteLine($" {p.ProductID} , {p.ProductName} , {p.UnitsInStock} ");
            }
        }

        static private void Update() 
        {
            // Update
            Console.WriteLine($"\n using  --- Update : \n");

            using (var db1 = new NorthWindContext())
            {
                // 搜尋目標
                var p = db1.Products.Find(1); // 它是會自動偵測 Key 是 ProductID
                // 列印原本值
                Console.WriteLine($" 原本值︰ p.UnitsInStock = {p.UnitsInStock} ");
                // 修改目標
                p.UnitsInStock = 13;
                // 存入資料庫
                db1.SaveChanges();
                // 再找出來一次
                p = db1.Products.Find(1);
                // 列印目前值
                Console.WriteLine($" 修改後目前值︰ p.UnitsInStock = {p.UnitsInStock} ");
            }
        }

        static private void Create() 
        {
            // Create
            Console.WriteLine($"\n using  --- Create : \n");

            using (var db1 = new NorthWindContext())
            {
                // 創造資料
                var p = new Products
                {
                    ProductName = "Car",
                    UnitPrice = 100000,
                    UnitsInStock = 1,
                    UnitsOnOrder = 10
                };
                // 新增資料
                db1.Products.Add(p);
                // 存入資料庫
                db1.SaveChanges();
                // 再找出來一次 (取最後一筆)
                var products_1 = from fp in db1.Products select fp;
                p = db1.Products.Find(products_1.Count());
                // 列印新增資料
                Console.WriteLine($" 列印新增出來的資料 : ");
                Console.WriteLine($" {p.ProductID} , {p.ProductName} , {p.UnitsInStock} ");
            }

        }

        static private void Delete()
        {

            // Delete
            Console.WriteLine($"\n using  --- Delete : \n");

            using (var db1 = new NorthWindContext())
            {

                // 查詢最後五筆
                Console.WriteLine($" 查詢尾五筆 : ");
                var products_1 = from fp in db1.Products select fp;
                products_1 = db1.Products.OrderByDescending(x => x.ProductID).Take(5);
                foreach (var p1 in products_1.Take(5))
                    Console.WriteLine($" {p1.ProductID} , {p1.ProductName} , {p1.UnitsInStock} ");


                // 尋找目標
                var new_p = db1.Products.Find(78);// 最後一筆的 ProductID = 78
                // 刪除目標
                db1.Products.Remove(new_p);
                // 存入資料庫
                db1.SaveChanges();


                // 再查詢一次最後五筆
                Console.WriteLine($"\n 刪最後第一筆Car後，查詢尾五筆 : ");
                products_1 = db1.Products.OrderByDescending(x => x.ProductID).Take(5);
                foreach (var p1 in products_1.Take(5))
                    Console.WriteLine($" {p1.ProductID} , {p1.ProductName} , {p1.UnitsInStock} ");
            }
        }


    }
}
