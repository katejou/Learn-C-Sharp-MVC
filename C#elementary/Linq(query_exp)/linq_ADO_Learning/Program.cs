using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;


namespace linq_ADO_Query_Exp_Learning
{
    class Program
    {
        // 利用 Linq 重覆搜尋 從資料庫取回來，存於 ADO.NET 的結果，減少和資料庫連線的負擔。

        static void Main(string[] args)
        {

            //-----------------------------------------------------------------------------------------------------
            // https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/creating-a-datatable-from-a-query-linq-to-dataset
            // https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/queries-in-linq-to-dataset
            // 使用官網範例 (稍有自己更改，更適合展示及學習。)
            //-----------------------------------------------------------------------------------------------------
            // 連結字串︰
            // 下載資料庫地址︰https://docs.microsoft.com/zh-tw/sql/samples/adventureworks-install-configure?view=sql-server-ver15&tabs=ssms
            string connectionString = "Data Source=localhost;Initial Catalog=AdventureWorksLT2019;Integrated Security=true;";

            // https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/query-expression-syntax-examples-projection-linq-to-dataset
            SqlDataAdapter da = new SqlDataAdapter("Select * from SalesLT.Product;", connectionString);
            DataSet ds = new DataSet();
            ds.Locale = CultureInfo.InvariantCulture;
            da.Fill(ds);
            ds.Tables[0].TableName = "Product";
            DataTable products = ds.Tables["Product"];

            IEnumerable<DataRow> query = (from product in products.AsEnumerable()
                                          select product).Take(5);

            Console.WriteLine("> 1.1 用 foreach 取 Product Names:\n");
            #region 1.1
            foreach (DataRow p in query)
                Console.WriteLine(p.Field<string>("Name"));

            Console.WriteLine();
            IEnumerable<string> query1 = (from product in products.AsEnumerable()
                                          select product.Field<string>("Name")).Skip(5).Take(5); 
            #endregion

            Console.WriteLine("> 1.2 用 Linq 取 Product Names:\n");
            #region 1.2
            foreach (string productName in query1)
                Console.WriteLine(productName);

            //重點 :  .Field<string>("Name") 加在哪裡。 
            #endregion

            Console.WriteLine("\n> 1.3 用 Linq 取 跨表格資料 (select new () 的多欄同選作用 ):\n");
            #region 1.3

            SqlDataAdapter da2 = new SqlDataAdapter("Select * from SalesLT.Customer; Select * from SalesLT.SalesOrderHeader", connectionString);
            da2.Fill(ds); //  fill 的意思，是指 舊 ds(已有一個表格) 在後方加入兩個 table，所以他們排 2 , 3 
            ds.Tables[1].TableName = "Contact"; // SalesLT.Customer
            ds.Tables[2].TableName = "SalesOrderHeader"; // SalesLT.SalesOrderHeader
            DataTable contacts = ds.Tables["Contact"];
            DataTable orders = ds.Tables["SalesOrderHeader"];

            var query2 = (from contact in contacts.AsEnumerable()
                          from order in orders.AsEnumerable()
                          where contact.Field<int>("CustomerID") == order.Field<int>("CustomerID")
                              && order.Field<decimal>("TotalDue") < 500.00M
                              && order.Field<DateTime>("OrderDate") >= new DateTime(2002, 10, 1)
                          select new
                          {
                              ContactID = contact.Field<int>("CustomerID"),
                              LastName = contact.Field<string>("LastName"),
                              FirstName = contact.Field<string>("FirstName"),
                              OrderID = order.Field<int>("SalesOrderID"),
                              Total = order.Field<decimal>("TotalDue")
                          }).Take(5);

            foreach (var smallOrder in query2)
                Console.WriteLine("Contact ID: {0} Name: {1}, {2} Order ID: {3} Total Due: ${4} ",
                    smallOrder.ContactID, smallOrder.LastName, smallOrder.FirstName,
                    smallOrder.OrderID, smallOrder.Total);

            #endregion

            Console.WriteLine("\n> 1.4 在 Linq 中使用 let :\n");
            #region 1.4

            var query3 = (from contact in contacts.AsEnumerable()
                          from order in orders.AsEnumerable()
                          let total = order.Field<decimal>("TotalDue")
                          where contact.Field<int>("CustomerID") == order.Field<int>("CustomerID")
                                &&
                                total >= 10000.0M
                          select new
                          {
                              ContactID = contact.Field<int>("CustomerID"),
                              LastName = contact.Field<string>("LastName"),
                              OrderID = order.Field<int>("SalesOrderID"),
                              total
                          }).Take(5);
            foreach (var order in query3)
                Console.WriteLine("Contact ID: {0} Last name: {1} Order ID: {2} Total: {3}",
                    order.ContactID, order.LastName, order.OrderID, order.total);

            #endregion

            Console.WriteLine("\n> 1.5 Where 的用法 bool :\n");
            #region 1.5

            // query 這個變數無法被共用，一個給賦值，就會停留在一個有詳細欄位格式的 DataRow 型別。
            var query4 = (from order in orders.AsEnumerable()
                          where order.Field<bool>("OnlineOrderFlag") == false
                          select new
                          {
                              SalesOrderID = order.Field<int>("SalesOrderID"),
                              OrderDate = order.Field<DateTime>("OrderDate"),
                              SalesOrderNumber = order.Field<string>("SalesOrderNumber")
                          }).Take(5);

            foreach (var onlineOrder in query4)
                Console.WriteLine("Order ID: {0} Order date: {1:d} Order number: {2}",
                    onlineOrder.SalesOrderID,
                    onlineOrder.OrderDate,
                    onlineOrder.SalesOrderNumber);

            #endregion

            Console.WriteLine("\n> 1.5 Where 的用法 < 大於 和 小於 > :\n");
            #region 1.5

            SqlDataAdapter da3 = new SqlDataAdapter("Select * from SalesLT.SalesOrderDetail", connectionString);
            da3.Fill(ds); //  fill 的意思，是指 舊 ds(已有一個表格) 在後方加入兩個 table，所以他們排 2 , 3 
            ds.Tables[3].TableName = "SalesOrderDetail"; // SalesLT.SalesOrderDetail
            var ordersDetail = ds.Tables["SalesOrderDetail"];

            var query5 = (from order in ordersDetail.AsEnumerable()
                          where order.Field<Int16>("OrderQty") > 2 &&
                              order.Field<Int16>("OrderQty") < 6
                          select new
                          {
                              SalesOrderID = (int)order.Field<int>("SalesOrderID"),
                              OrderQty = order.Field<Int16>("OrderQty")
                          }).Take(5);

            foreach (var order in query5)
                Console.WriteLine("Order ID: {0} Order quantity: {1}", order.SalesOrderID, order.OrderQty);

            #endregion

            Console.WriteLine("\n> 1.6 Where 的用法 == \"  \" :\n");
            #region 1.6

            var query6 = (from product in products.AsEnumerable()
                          where product.Field<string>("Color") == "Red"
                          select new
                          {
                              Name = product.Field<string>("Name"),
                              ProductNumber = product.Field<string>("ProductNumber"),
                              ListPrice = product.Field<Decimal>("ListPrice")
                          }).Take(2);

            foreach (var product in query6)
            {
                Console.WriteLine("Name: {0}", product.Name);
                Console.WriteLine("Product number: {0}", product.ProductNumber);
                Console.WriteLine("List price: ${0}", product.ListPrice);
                Console.WriteLine("");
            }

            #endregion
            
            Console.WriteLine("\n> 1.7  DataRelation 和 .GetChildRows  :\n");
            #region 1.7

            var ordersHeader = ds.Tables["SalesOrderHeader"];

            // 新增關係︰
            DataColumn parentColumn = ds.Tables["SalesOrderHeader"].Columns["SalesOrderID"];
            DataColumn childColumn = ds.Tables["SalesOrderDetail"].Columns["SalesOrderID"];
            DataRelation relCustOrder = new DataRelation("SalesOrderHeaderDetail", parentColumn, childColumn);
            ds.Relations.Add(relCustOrder);

            // 只有 Header 取 2 筆資料
            IEnumerable<DataRow> query7 = (from order in ordersHeader.AsEnumerable()
                                           where order.Field<DateTime>("OrderDate") >= new DateTime(2002, 12, 1)
                                           select order).Take(2);

            foreach (DataRow order in query7)
            {
                Console.WriteLine(" OrderID {0} Order date: {1:d} ", order.Field<int>("SalesOrderID"), order.Field<DateTime>("OrderDate"));
                // 找出 與 Header 在 DataSet 有關連的地方，取出其中的兩個欄位︰ProductID 和 UnitPrice，列印成一行。
                foreach (DataRow orderDetail in order.GetChildRows("SalesOrderHeaderDetail"))
                {
                    Console.WriteLine("  > Product ID: {0} Unit Price {1}",
                        orderDetail["ProductID"], orderDetail["UnitPrice"]);
                }
            } 
            #endregion

            Console.WriteLine("\n> 1.8  OrderBy  :\n");
            #region 1.8

            IEnumerable<DataRow> query8 = (from contact in contacts.AsEnumerable()
                                           orderby contact.Field<string>("LastName")
                                           select contact).Take(5);

            foreach (DataRow contact in query8)
                Console.WriteLine(contact.Field<string>("LastName"));

            #endregion

            Console.WriteLine("\n> 1.9  .Distinct() 和 OrderBy 以 .GroupBy 和 .First 取代 :\n");
            #region 1.9

            //IEnumerable<DataRow> query9 = (from contact in contacts.AsEnumerable()
            //                               orderby contact.Field<string>("LastName").Length
            //                               select contact).Distinct().Take(5);
            //// distinct 會因為 OrderBy 而失效。 
            //// https://stackoverflow.com/questions/26068277/linq-to-sql-order-by-with-distinct
            IEnumerable<DataRow> query9 = (from contact in contacts.AsEnumerable()
                                           orderby contact.Field<string>("LastName").Length
                                           select contact)
                                           .GroupBy(x => x.Field<string>("LastName"))
                                           .Select(g => g.First()).Take(5);

            foreach (DataRow contact in query9)
                Console.WriteLine(contact.Field<string>("LastName"));

            #endregion

            Console.WriteLine("\n> 2.0  OrderByDescending :\n");
            #region 2.0

            IEnumerable<Decimal> query10 = (from p in products.AsEnumerable()
                                            orderby p.Field<Decimal>("ListPrice") descending
                                            select p.Field<Decimal>("ListPrice"))
                                            .GroupBy(x => x)
                                            .Select(g => g.First()).Take(5);

            foreach (Decimal d in query10)
                Console.WriteLine(d);

            #endregion

            Console.WriteLine("\n> 2.1  Reverse :\n");
            #region 2.1

            var query11 = (from order in ordersHeader.AsEnumerable()
                           where order.Field<decimal>("Freight") > 70
                           orderby order.Field<decimal>("Freight") descending
                           select order.Field<decimal>("Freight"))
                           .Reverse()
                           .GroupBy(x => x)
                           .Select(g => g.First())
                           .Take(5);

            foreach (var order in query11)  // 因為 orderby descending 之後又 Reverse 所以變成了 正序
                Console.WriteLine(order);

            #endregion

            Console.WriteLine("\n> 2.2  order by 兩個欄位 首要和次要 一個順序，一個倒序 :\n");
            #region 2.2

            IEnumerable<DataRow> query12 = (from product in products.AsEnumerable()
                                            orderby
                                            product.Field<Decimal>("ListPrice") descending,
                                            product.Field<int>("ProductID")
                                            select product)
                                            .GroupBy(x => x.Field<Decimal>("ListPrice"))
                                            .Select(g => g.First())
                                            .Take(5);

            foreach (DataRow product in query12)
            {
                Console.WriteLine("List Price :{1} Product ID: {0} ",
                    product.Field<int>("ProductID"),
                    product.Field<Decimal>("ListPrice"));
            }

            #endregion

            Console.WriteLine("\n> 2.3  ElementAt 指定第幾筆資料:\n");
            #region 2.3

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from SalesLT.Address", connectionString);
            da4.Fill(ds); //  fill 的意思，是指 舊 ds(已有一個表格) 在後方加入兩個 table，所以他們排 2 , 3 
            ds.Tables[4].TableName = "Address"; // SalesLT.SalesOrderDetail
            var addresses = ds.Tables["Address"];

            var fifthAddress = (from address in addresses.AsEnumerable()
                                where address.Field<string>("PostalCode") == "M4B 1V7"
                                select address.Field<string>("AddressLine1"))
                                .ElementAt(5);

            Console.WriteLine("  5 th address where PostalCode = 'M4B 1V7': {0}", fifthAddress);

            #endregion

            Console.WriteLine("\n> 2.4  group XX by YY into g 再使用 g.Key﹑g.Average  &&  select new 之中設定屬性 :\n");
            #region 2.4

            var style = (from product in products.AsEnumerable()
                         group product by product.Field<string>("Color") into g
                         select new
                         {
                             Style = g.Key,
                             AverageListPrice = g.Average(product => product.Field<Decimal>("ListPrice"))
                         }
                        ).Take(5);

            foreach (var product in style)
                Console.WriteLine("     Product color: {0} \nAverage list price: {1}",
                    product.Style, product.AverageListPrice);

            #endregion

            Console.WriteLine("\n> 2.5  在 Let 使用 Average ，用 select new 之中設定的屬性做 第二層 foreach  :\n");
            #region 2.5

            var letA = (from order in products.AsEnumerable()
                        group order by order.Field<string>("Color") into g
                        let Gaverage = g.Average(order => order.Field<Decimal>("ListPrice"))
                        select new
                        {
                            Category = g.Key,
                            Gaverage = Gaverage,
                            ListPriceArroundGaverage = g.Where(order =>
                                                               order.Field<decimal>("ListPrice") < Gaverage + 100
                                                               &&
                                                               order.Field<decimal>("ListPrice") > Gaverage - 100)
                        }).Take(2);

            foreach (var a in letA)
            {
                Console.WriteLine("\nColor: {0} , Group_Avg_List_Price: {1} \n", a.Category, a.Gaverage);
                foreach (var order in a.ListPriceArroundGaverage)
                    Console.WriteLine(" ProductID : {0}, ListPrice : {1} ", order.Field<int>("ProductID"), order.Field<decimal>("ListPrice")); ;
            } 
            #endregion

            Console.WriteLine("\n> 2.6 .Count()  :\n");
            #region 2.6

            var letB = (from order in products.AsEnumerable()
                        group order by order.Field<string>("Color") into g
                        let Gaverage = g.Average(order => order.Field<Decimal>("ListPrice"))
                        select new
                        {
                            Category = g.Key,
                            Gaverage = Gaverage,
                            CountOverGAve =
                            g.Where(order => order.Field<decimal>("StandardCost") > Gaverage).Count()
                        }).Take(2);

            foreach (var b in letB)
            {
                Console.WriteLine("Color: {0} , Group_Avg_List_Price: {1} ", b.Category, b.Gaverage);
                Console.WriteLine("Product StandardCost > Group_Avg_List_Price: {0} ", b.CountOverGAve);
                Console.WriteLine("");
            } 
            #endregion

            Console.WriteLine("\n> 2.7 Max 放在 Where 的位置 :\n");
            #region 2.7

            var qM = (from order in products.AsEnumerable()
                      group order by order.Field<string>("Color") into g
                      select new
                      {
                          Category = g.Key,
                          maxListPrice =
                          g.Max(order => order.Field<Decimal>("ListPrice"))
                      }
                      ).OrderBy(x => x.maxListPrice).Take(5);

            foreach (var b in qM)
                Console.WriteLine("Color: {0} , Maximum_List_Price: {1} ", b.Category, b.maxListPrice);

            #endregion

            Console.WriteLine("\n> 2.8 在 Let 行使用 Max :\n");
            #region 2.8

            var qLM = (from order in products.AsEnumerable()
                       group order by order.Field<string>("Color") into g
                       let maxListPrice = g.Max(order => order.Field<Decimal>("ListPrice"))
                       select new
                       {
                           Category = g.Key,
                           M = maxListPrice
                       }
                      ).Take(5);

            foreach (var b in qLM)
                Console.WriteLine("Color: {0} , Maximum_List_Price: {1} ", b.Category, b.M);

            #endregion

            Console.WriteLine("\n> 2.9 Min 的用法一樣，省略 \n");

            Console.WriteLine("\n> 3.0 Sum , group by 和 order by \n");
            #region 3.0
            var CS = (from product in products.AsEnumerable()
                      group product by product.Field<int>("ProductCategoryID") into g
                      orderby g.Sum(order => order.Field<Decimal>("ListPrice")) descending
                      select new
                      {
                          Category = g.Key,
                          TotalLP = g.Sum(order => order.Field<Decimal>("ListPrice"))
                      }
                         ).Take(5);
            foreach (var row in CS)
                Console.WriteLine("ProductCategoryID = {0} \t TotalListPrice = {1}", row.Category, row.TotalLP);

            #endregion

            Console.WriteLine("\n> 3.1 Join..on..equals..into  \n");
            #region 3.1

            var gj = (from order in ordersHeader.AsEnumerable()
                      join detail in ordersDetail.AsEnumerable()
                      on order.Field<int>("SalesOrderID")
                      equals detail.Field<int>("SalesOrderID") into eq
                      orderby eq.Count() descending
                      select new
                      {
                          SalesOrderID = order.Field<int>("SalesOrderID"),
                          //unitPrice = detail.Field<int>("UnitPrice"), // 只要有 into 在就不能用 detail
                          eqCount = eq.Count()
                      }
                      ).Take(5);

            foreach (var order in gj)
                Console.WriteLine("SalesOrderID: {0}  Detail Count: {1}", order.SalesOrderID, order.eqCount);

            #endregion

            Console.WriteLine("\n> 3.2  只要有 into 在就不能用 join 右手邊的代名詞，因為 join..into是一起的  \n");
            #region 3.2

            var g_j = (from order in ordersHeader.AsEnumerable()
                       join detail in ordersDetail.AsEnumerable()
                       on order.Field<int>("SalesOrderID")
                       equals detail.Field<int>("SalesOrderID") //into j // <-- 可以加來測出紅線
                       select new
                       {
                           SalesOrderID = order.Field<int>("SalesOrderID"),
                           unitPrice = detail.Field<decimal>("UnitPrice"), // 只要有 into 在就不能用 detail
                       }
                      ).Take(5);

            foreach (var order in g_j)
                Console.WriteLine("SalesOrderID: {0}  UnitPrice: {1}", order.SalesOrderID, order.unitPrice);

            #endregion

            Console.WriteLine("\n> 3.3 from a in As join b in Bs on a.field equals b.field where 「bool」 select ...  \n");
            #region 3.3

            var jw = (from order in ordersHeader.AsEnumerable()
                      join detail in ordersDetail.AsEnumerable()
                      on order.Field<int>("SalesOrderID") equals detail.Field<int>("SalesOrderID")
                      where order.Field<bool>("OnlineOrderFlag") == false
                            &&
                            detail.Field<Int16>("OrderQty") > 10
                      select new
                      {
                          SalesOrderID = order.Field<int>("SalesOrderID"),
                          SalesOrderDetailID = detail.Field<int>("SalesOrderDetailID"),
                          OrderDate = order.Field<DateTime>("OrderDate"),
                          ProductID = detail.Field<int>("ProductID"),
                          OrderQty = detail.Field<Int16>("OrderQty")
                      }
                     ).Take(2);

            foreach (var order in jw)
                Console.WriteLine("HeaderID :{0}\t\tDetailID:{1}\nOrderDate:{2:d}\tProductID:{3}\tOrderQty:{4}\n",
                    order.SalesOrderID,
                    order.SalesOrderDetailID,
                    order.OrderDate,
                    order.ProductID,
                    order.OrderQty
                    );

            #endregion


            Console.Write("\n\n按一下退出");
            Console.ReadKey();
        }
    }
}
