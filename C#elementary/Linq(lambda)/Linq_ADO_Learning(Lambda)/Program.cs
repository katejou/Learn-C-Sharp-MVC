using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;


namespace Linq_ADO_Learning_Lambda_
{
    class Program
    {
        static void Main(string[] args)
        {
            // 連結字串︰
            // 下載資料庫地址︰https://docs.microsoft.com/zh-tw/sql/samples/adventureworks-install-configure?view=sql-server-ver15&tabs=ssms
            string connectionString = "Data Source=localhost;Initial Catalog=AdventureWorksLT2019;Integrated Security=true;";

            // https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/query-expression-syntax-examples-projection-linq-to-dataset
            SqlDataAdapter da = new SqlDataAdapter("Select * from SalesLT.Product; " +
                                                   "Select * from SalesLT.Customer; " +
                                                   "Select * from SalesLT.SalesOrderHeader;" +
                                                   "Select * from SalesLT.Address;",
                                                   connectionString);
            DataSet ds = new DataSet();
            ds.Locale = CultureInfo.InvariantCulture;
            da.Fill(ds);

            ds.Tables[0].TableName = "Product";
            ds.Tables[1].TableName = "Contact";
            ds.Tables[2].TableName = "SalesOrderHeader";
            ds.Tables[3].TableName = "Address";

            DataTable products = ds.Tables["Product"];
            var contacts = ds.Tables["Contact"].AsEnumerable();
            var orders = ds.Tables["SalesOrderHeader"].AsEnumerable();
            var addresses = ds.Tables["Address"].AsEnumerable();

            // https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/method-based-query-syntax-examples-projection

            Console.WriteLine("> 1. IEnumerable.Select(each => new { }) 物件化結果");
            #region 1

            var query = products.AsEnumerable()
                        .Select(product => new
                        {
                            ProductName = product.Field<string>("Name"),
                            ProductNumber = product.Field<string>("ProductNumber"),
                            Price = product.Field<decimal>("ListPrice")
                        })
                        .Take(5);
            foreach (var productInfo in query)
                Console.WriteLine("Product name: {0} \tnumber: {1} \tList price: ${2} ",
                    productInfo.ProductName, productInfo.ProductNumber, productInfo.Price); 
            #endregion

            Console.WriteLine("\n> 2. SelectMany().Select() (SelectMany 有 join 和 where 的效果 : )");
            #region 2
            var query2 = contacts.SelectMany(contact =>
                                                 orders.Where(order =>
                                                             (contact.Field<Int32>("CustomerID") == order.Field<Int32>("CustomerID"))
                                                              &&
                                                              order.Field<DateTime>("OrderDate") >= new DateTime(2002, 10, 1))
                                     .Select(order => new
                                     {
                                         ContactID = contact.Field<int>("CustomerID"),
                                         LastName = contact.Field<string>("LastName"),
                                         FirstName = contact.Field<string>("FirstName"),
                                         OrderID = order.Field<int>("SalesOrderID"),
                                         OrderDate = order.Field<DateTime>("OrderDate")
                                     }))
                                     .Take(5);

            foreach (var order in query2)
            {
                Console.WriteLine("Contact ID: {0} \tName: {1}, {2,-15} \tOrder ID: {3} \tOrder date: {4:d} ",
                    order.ContactID, order.LastName, order.FirstName,
                    order.OrderID, order.OrderDate);
            } 
            #endregion

            // https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/method-based-query-syntax-examples-partitioning-linq

            Console.WriteLine("\n> 3. Skip (去了某資料表的頭 5 行 = 取 6-10 行):");

            #region 3
            IEnumerable<DataRow> allButFirst5Contacts = contacts.Skip(5).Take(5);

            foreach (DataRow contact in allButFirst5Contacts)
                Console.WriteLine("FirstName = {0} \tLastname = {1}",
                    contact.Field<string>("FirstName"),
                    contact.Field<string>("Lastname")); 
            #endregion

            Console.WriteLine("\n> 4. 雙 from in (比較和 query expression 那邊相關) " +
                                      "\n 在select new 就可以用兩個名詞，也許可以解決 join xx into 的問題:");
            #region 4

            var query4 = (from address in addresses
                          from order in orders
                          where address.Field<int>("AddressID") == order.Field<int>("BillToAddressID")
                          select new
                          {
                              City = address.Field<string>("City"),
                              OrderID = order.Field<int>("SalesOrderID"),
                              OrderDate = order.Field<DateTime>("OrderDate")
                          }
                          ).Skip(2).Take(5);

            foreach (var order in query4)
                Console.WriteLine("City: {0,-10} \tOrder ID: {1} \tOrderDate: {2:d}",
                    order.City, order.OrderID, order.OrderDate); 
            #endregion

            Console.WriteLine("\n> 5. Skip While 和 Take While : ");
            #region 5

            IEnumerable<DataRow> skip_take_While = products.AsEnumerable()
                                                                     .OrderBy(listprice => listprice.Field<decimal>("ListPrice"))
                                                                     .SkipWhile(product => product.Field<decimal>("ListPrice") < 300M) // 去了 300M 以下的
                                                                     .TakeWhile(product => product.Field<decimal>("ListPrice") < 400M) // 取 400M 以下的                                                                     // 不能當作 Range 使用 
                                                                     .Take(5);  // 要 300 - 400 中間的頭 5 個


            foreach (DataRow product in skip_take_While)
                Console.WriteLine(product.Field<decimal>("ListPrice"));

            #endregion

            Console.WriteLine("\n> 6. Order by : ");
            #region 6

            IEnumerable<DataRow> queryOB = contacts.AsEnumerable()
                                                   //.GroupBy(contact => contact.Field<string>("LastName")) // Group by 和 Order by 總是不相容
                                                   .OrderBy(contact => contact.Field<string>("LastName"))
                                                   //, new CaseInsensitiveComparer());  // 這個在官網上說可以使它不區分大小寫的東東，不知道為什麼出錯。一直有紅線。
                                                   //.Distinct()  // 這個也失效了。                                                   
                                                   .Take(10);

            foreach (DataRow contact in queryOB)
                Console.WriteLine(contact.Field<string>("LastName")); 
            #endregion

            Console.WriteLine("\n> 7. Order by + Group by : ");
            #region 7

            IEnumerable<DataRow> queryOG = contacts.AsEnumerable()
                                                   .OrderBy(contact => contact.Field<string>("LastName"))
                                                   .GroupBy(contact => contact.Field<string>("LastName"))
                                                   .Select(g => g.First())
                                                   .Take(5);

            foreach (DataRow contact in queryOG)
                Console.WriteLine(contact.Field<string>("LastName")); 
            #endregion

            Console.WriteLine("\n> 8. Reverse (比較像是query exp 但是不用 order by ... dese): ");
            #region 8

            IEnumerable<DataRow> queryR = (from order in orders.AsEnumerable()
                                           where order.Field<DateTime>("OrderDate") > new DateTime(2002, 02, 20)
                                           select order)
                                           .Reverse()
                                           .Take(5);

            foreach (DataRow order in queryR)
                Console.WriteLine(order.Field<DateTime>("OrderDate"));

            #endregion

            Console.WriteLine("\n> 9. Reverse (因為我覺得上個例子在資料的抽取和句式都沒有表現得很好): ");
            #region 9

            IEnumerable<DataRow> queryR2 = contacts.AsEnumerable()
                                                   .OrderBy(contact => contact.Field<string>("LastName"))
                                                   .GroupBy(contact => contact.Field<string>("LastName"))
                                                   .Select(g => g.First())
                                                   .Reverse()
                                                   .Take(5);

            foreach (DataRow contact in queryR2)
                Console.WriteLine(contact.Field<string>("LastName"));

            #endregion

            Console.WriteLine("\n> 10. Order by + Then by + each Group take top 2: ");
            #region 10
            // https://github.com/dotnet/efcore/issues/13805
            IEnumerable<DataRow> queryOT = products.AsEnumerable()
                                                   .OrderBy(product => product.Field<string>("Color"))
                                                   .ThenBy(product => product.Field<int>("ProductID"))
                                                   .GroupBy(product => product.Field<string>("Color"))
                                                   .Select(g => g.Take(2))
                                                   .SelectMany(g => g);

            foreach (DataRow product in queryOT)
                Console.WriteLine("Product Color: {1,-15} \tID: {0}",
                                   product.Field<int>("ProductID"),
                                   product.Field<string>("Color"));

            #endregion

            Console.WriteLine("\n> 11. CopyToDataTable() 和 Distinct: ");
            #region 11

            //IEnumerable<DataRow> query100 = (from c in contacts select c).Take(10);
            // 精簡為︰
            IEnumerable<DataRow> query100 = contacts.Take(100);

            // IEnumerable<DataRow> 轉化為 DataTable︰// <------------------------------------
            DataTable contactsTableWith100Rows = query100.CopyToDataTable();
            // 設List<DataRow>容器︰
            List<DataRow> rows = new List<DataRow>();
            // 製造重複的資料︰
            for (int i = 0; i < 10; i++)
                rows.Add(contactsTableWith100Rows.Rows[i]);
            for (int i = 0; i < 10; i++)
                rows.Add(contactsTableWith100Rows.Rows[i]);

            // List<DataRow> 轉化為 DataTable︰// <------------------------------------
            DataTable table = System.Data.DataTableExtensions.CopyToDataTable<DataRow>(rows);

            // DataTable 轉化為 IEnumerable<DataRow>︰// <------------------------------------
            // .Distinct(DataRowComparer.Default) // <------- Distinct 的使用要加參數？不加會用不了嗎？
            // 測不加 Distinct 的效果︰
            //IEnumerable<DataRow> uniqueContacts = table.AsEnumerable();//.Distinct(DataRowComparer.Default);
            // 加了之後︰
            IEnumerable<DataRow> uniqueContacts = table.AsEnumerable().Distinct(DataRowComparer.Default);
            // 測 Distinct 不加參數 = 沒有作用
            //IEnumerable<DataRow> uniqueContacts = table.AsEnumerable().Distinct();

            foreach (DataRow uniqueContact in uniqueContacts)
                Console.WriteLine(uniqueContact.Field<string>("FirstName"));

            // https://dotblogs.com.tw/kinanson/2017/06/04/192619#6

            #endregion

            Console.WriteLine("\n> 12. 左-右 Except: ");
            #region 12

            // 選 小姐
            IEnumerable<DataRow> query11 = from contact in contacts
                                           where contact.Field<string>("Title") == "Ms."
                                           select contact;
            // 選 沒有中間名的
            IEnumerable<DataRow> query12 = from contact in contacts
                                           where contact.Field<string>("MiddleName") is null
                                           //where contact.Field<string>("MiddleName") != null // 可以測反效果
                                           select contact;

            DataTable contactsT1 = query11.CopyToDataTable();
            DataTable contactsT2 = query12.CopyToDataTable();

            // 選「有」中間名的小姐 
            var contacts13 = contactsT1.AsEnumerable()
                                       .Except(contactsT2.AsEnumerable(), DataRowComparer.Default) // <-------------
                                       .Take(5);

            foreach (DataRow row in contacts13)
                Console.WriteLine("{0} {1} {2} {3}", row["Title"], row["FirstName"], row["MiddleName"], row["LastName"]);

            #endregion

            Console.WriteLine("\n> 13. 兩邊比較 留相同值 (=左右都有) Intersect: ");
            #region 13

            // 選 中間名 是 J.
            IEnumerable<DataRow> query14 = from contact in contacts
                                           where contact.Field<string>("MiddleName") == "J."
                                           select contact;

            DataTable contactsT3 = query14.CopyToDataTable();

            // 中間名 是 J. 的小姐
            var contact14 = contactsT1.AsEnumerable()
                                     .Intersect(contactsT3.AsEnumerable()  //<----------------------
                                     , DataRowComparer.Default)
                                     .Take(5);

            foreach (DataRow row in contact14)
                Console.WriteLine("{0} {1} {2} {3}", row["Title"], row["FirstName"], row["MiddleName"], row["LastName"]);

            #endregion

            Console.WriteLine("\n> 14. 左+右(但相同值不會再加入多一次) Union: ");
            #region 14

            IEnumerable<DataRow> query15 = (from contact in contacts
                                            where contact.Field<string>("Title") == "Ms."
                                            select contact)
                                           .Take(3);
            // 選 小姐 + 中間名是 M. 的「人」
            IEnumerable<DataRow> query16 = (from contact in contacts
                                            where contact.Field<string>("Title") == "Ms."
                                            select contact)
                                           .Skip(1).Take(3); // 中間會有兩個重覆的人

            DataTable contactsT5 = query15.CopyToDataTable();
            DataTable contactsT6 = query16.CopyToDataTable();

            var contact16 = contactsT5.AsEnumerable()
                                       .Union(contactsT6.AsEnumerable()  //<----------------------
                                       , DataRowComparer.Default)
                                       .Take(5);

            foreach (DataRow row in contact16)
                Console.WriteLine("{0} {1} {2} {3}", row["Title"], row["FirstName"], row["MiddleName"], row["LastName"]);

            Console.WriteLine("有 4 個結果，因為 (1,2,3) 和 (2,3,4) 會產出 (1,2,3,4)");

            #endregion

            Console.WriteLine("\n> 15.  ToArray : ");
            #region 15
            // V 重點就是轉型，沒什麼好說的。
            DataRow[] productsArray1 = products.AsEnumerable().Take(5).ToArray();

            Console.WriteLine("> 先取頭5個，再在該5個之中排列 : ");
            IEnumerable<DataRow> queryTA = from product in productsArray1
                                           orderby product.Field<Decimal>("ListPrice") descending
                                           select product;
            foreach (DataRow product in queryTA)
                Console.WriteLine(product.Field<Decimal>("ListPrice"));

            Console.WriteLine("> 先排列，再取頭5個 : ");
            IEnumerable<DataRow> productsArray2 = products.AsEnumerable();
            IEnumerable<DataRow> queryTB = (from product in productsArray2
                                            orderby product.Field<Decimal>("ListPrice") descending
                                            select product).Take(5);
            foreach (DataRow product in queryTB)
                Console.WriteLine(product.Field<Decimal>("ListPrice"));

            #endregion

            Console.WriteLine("\n> 16.  ToDictionary : ");
            #region 16

            // .ToDictionary 的 Key 和 Value 長這樣
            Dictionary<string, DataRow> scoreRecordsDict = products.AsEnumerable()
                                           .ToDictionary(record => record.Field<string>("ProductNumber"));
            // 設下了名稱為 Key 用 ProductNumber : FR-R92B-58 去找它的 ProductID
            Console.WriteLine("ProductNumber: FR-R92B-58 的 \nProductID: {0} \n和\nName : {1}"
                , scoreRecordsDict["FR-R92B-58"]["ProductID"], scoreRecordsDict["FR-R92B-58"]["Name"]);

            #endregion

            Console.WriteLine("\n> 17.  ToList : ");
            #region 17
            // 和 ToArray 一樣，沒什麼好說的。
            List<DataRow> productList = products.AsEnumerable().ToList();

            IEnumerable<DataRow> querytl = (from product in productList
                                            orderby product.Field<string>("Name")
                                            select product).Take(5);

            foreach (DataRow product in querytl)
                Console.WriteLine(product.Field<string>("Name").ToLower(CultureInfo.InvariantCulture));

            #endregion

            Console.WriteLine("\n> 18.  ElementAt : ");
            #region 18
            // 注意它回傳了 string ，所以不止 ElementsAt 連 select 都對回傳型別有所影響
            // 不信可以刪了︰.Field<string>("AddressLine1") ，馬上出紅線，顯示它所回傳的是 DataRow
            string fifthAddress = (from address in addresses
                                   where address.Field<string>("PostalCode") == "M4B 1V7"
                                   select address.Field<string>("AddressLine1")).ElementAt(5);

            Console.WriteLine("Fifth address where PostalCode = 'M4B 1V7': {0}", fifthAddress);

            #endregion

            Console.WriteLine("\n> 19. First ");
            #region 19
            // 大致同上，沒什麼好說的。
            DataRow queryC = (from contact in contacts.AsEnumerable()
                              where (string)contact["FirstName"] == "Lucy"
                              select contact).First(); // 如果沒有這個名字，連一個結果都沒有，.First 會Runtime Error。

            Console.WriteLine("CustomerID: " + queryC.Field<int>("CustomerID"));
            Console.WriteLine("FirstName: " + queryC.Field<string>("FirstName"));
            Console.WriteLine("LastName: " + queryC.Field<string>("LastName"));

            #endregion

            Console.WriteLine("\n> 20. Aggregate \n(這個比Linq_Learning那一邊的例子不一樣，它回傳的是句子，那一邊是只利用它做迴圈");
            #region 20

            string nameList = contacts
                              .Take(5)
                              .Select(x => x.Field<string>("LastName"))
                              .Aggregate((before, end) => before + "," + end);

            Console.WriteLine(nameList);

            // .Aggregate((before, end) => before + "," + end);
            // 以我的理解，它是個內迴。
            // 它找到了最後一個end, 才開始把所有的before, 以重畳的方式拼接起來。
            // 1 , 2
            //     2 , 3
            //         3 , 4
            //             4 , 5
            // 拼接時，將所有重覆的地方消除。 
            #endregion

            Console.WriteLine("\n> 21. Average 回傳一個 Decimal ");
            #region 21

            Decimal averageListPrice = products.AsEnumerable()
                                               .Average(product => product.Field<Decimal>("ListPrice"));

            Console.WriteLine("{0}", averageListPrice);

            #endregion

            Console.WriteLine("\n> 22. Group by + Average 分組算平均值 ︰  (這個例子 和 query expression 那邊的沒差，都是混在一起用");
            #region 22

            var queryGA = (from product in products.AsEnumerable()
                           group product by product.Field<string>("Color") into g
                           select new
                           {
                               Color = g.Key,
                               AverageListPrice = g.Average(product => product.Field<Decimal>("ListPrice"))
                           }).Take(5);

            foreach (var product in queryGA)
                Console.WriteLine("Color: {0} \tAverage: {1}",
                    product.Color, product.AverageListPrice); 
            #endregion

            Console.WriteLine("\n> 23. 測只用 Lambda 做 Group by + Average");
            #region 23

            var queryGA2 = products.AsEnumerable()
                                   .GroupBy(p => p.Field<string>("Color")) // 每一條資料 = p
                                   .Select(g => new // 每一組 = g
                                   {
                                       Color = g.Key, // 有 GroupBy 就有 Key 傳下來
                                       AverageListPrice = g.Average(p => p.Field<Decimal>("ListPrice"))  // g 之中的 p  = 組內的每一條資料
                                   })
                                   .Take(5);
            // 這寫法 效果和上面是一樣的。

            foreach (var product in queryGA2)
                Console.WriteLine("Color: {0} \tAverage: {1}", product.Color, product.AverageListPrice);

            #endregion

            Console.WriteLine("\n> 24.  Group by into + Let + Average + 用屬性設定，衍生第 2 層 結果 ( 這是和query expression 2.5題沒差的寫法 )");
            #region 24

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

            Console.WriteLine("\n> 25. 用 Lambda 寫 Let 的可能？ 代替方法 : 使用整句, 而非代詞");
            #region 25
            // https://entityframework.net/knowledge-base/9240375/how-to--let--in-lambda-expression-

            var letB = products.AsEnumerable()
                               .GroupBy(p => p.Field<string>("Color"))
                               .Select(g => new // 每一組 = g
                               {
                                   Color = g.Key, // 有 GroupBy 就有 Key 傳下來
                                   Gaverage = g.Average(p => p.Field<Decimal>("ListPrice")),
                                   ListPriceArroundGaverage = g.Where(order =>
                                                               order.Field<decimal>("ListPrice") < g.Average(p => p.Field<Decimal>("ListPrice")) + 100
                                                               &&
                                                               order.Field<decimal>("ListPrice") > g.Average(p => p.Field<Decimal>("ListPrice")) - 100)
                               })
                               .Take(2);

            foreach (var a in letB)
            {
                Console.WriteLine("\nColor: {0} , Group_Avg_List_Price: {1} \n", a.Color, a.Gaverage);
                foreach (var order in a.ListPriceArroundGaverage)
                    Console.WriteLine(" ProductID : {0}, ListPrice : {1} ", order.Field<int>("ProductID"), order.Field<decimal>("ListPrice")); ;
            } 
            #endregion

            Console.WriteLine("\n> 26. 還有其他用 Lambda 寫 Let 的可能？ 不再研究了，用到的機會很少，太複雜。");

            Console.WriteLine("\n> 27. Count 的例子，我記得在 query expression 那邊做過，太簡單了，不做。");

            Console.WriteLine("\n> 28. LongCount 回轉 long 數字型態");
            #region 28

            long numberOfContacts = contacts.AsEnumerable().LongCount();
            Console.WriteLine("There are {0} Contacts", numberOfContacts); 
            #endregion

            Console.WriteLine("\n> 29. Max 這是「外」用，query exp 那邊是「內」用");
            #region 29

            Decimal maxTotalDue = orders.AsEnumerable()
                                        .Max(w => w.Field<decimal>("TotalDue"));
            Console.WriteLine("The maximum TotalDue is {0}.", maxTotalDue); 
            #endregion

            Console.WriteLine("\n> 31. Min 和 Sum 的用法和 Max 一樣，省略");

            Console.WriteLine("\n> 32. Join 聯合(不分主次)");
            #region 32
            // 左表.Join 要加入 4 個參數 ( 右表, 左表鍵欄, 右表鍵欄, 左右表各抽什麼？  )
            var queryJ = contacts.AsEnumerable().Join(orders.AsEnumerable(),
                                                      order => order.Field<Int32>("CustomerID"),
                                                      contact => contact.Field<Int32>("CustomerID"),
                                                      (contact, order) => new
                                                      {
                                                          ContactID = contact.Field<Int32>("CustomerID"),
                                                          SalesOrderID = order.Field<Int32>("SalesOrderID"),
                                                          FirstName = contact.Field<string>("FirstName"),
                                                          Lastname = contact.Field<string>("Lastname"),
                                                          TotalDue = order.Field<decimal>("TotalDue")
                                                      })
                                                 .Take(5);

            foreach (var contact_order in queryJ)
            {
                Console.WriteLine("ContactID: {0} "
                                + "SalesOrderID: {1} "
                                + "FirstName: {2} "
                                + "Lastname: {3} "
                                + "TotalDue: {4}",
                    contact_order.ContactID,
                    contact_order.SalesOrderID,
                    contact_order.FirstName,
                    contact_order.Lastname,
                    contact_order.TotalDue);
            }

            #endregion

            Console.WriteLine("\n> 33. Join 加 GroupBy (感覺之前 Let 的那一題應該用這個方法改，但有空再說)");
            #region 33

            var queryLJ = contacts.AsEnumerable().Join(orders.AsEnumerable(),
                                                       order => order.Field<Int32>("CustomerID"),
                                                       contact => contact.Field<Int32>("CustomerID"),
                                                       (contact, order) => new
                                                       {
                                                           ContactID = contact.Field<Int32>("CustomerID"),
                                                           SalesOrderID = order.Field<Int32>("SalesOrderID"),
                                                           Title = contact.Field<string>("Title"),
                                                           FirstName = contact.Field<string>("FirstName"),
                                                           Lastname = contact.Field<string>("Lastname"),
                                                           TotalDue = order.Field<decimal>("TotalDue")
                                                       })
                                                       .GroupBy(record => record.Title);

            foreach (var group in queryLJ)
            {
                Console.WriteLine();
                foreach (var contact_order in group)
                {
                    Console.WriteLine("ContactID: {0} "
                                    + "Title: {1} "
                                    + "Name: {2} {3}",
                        contact_order.ContactID,
                        contact_order.Title,
                        contact_order.FirstName,
                        contact_order.Lastname);
                }
            }

            #endregion

            Console.WriteLine("\n> 34. CopyToDataTable 已實作過，就是從IEnumerable<DataRow> 變回 DataTable 。沒什好說的。");

            Console.WriteLine("\n> 35. DataRowComparer 比較兩個不同的資料列 (感覺會是 Aggregate / loop 的朋友？)");
            #region 35

            DataTable Otable = orders.CopyToDataTable();
            DataRow left = (DataRow)Otable.Rows[0];
            DataRow right = (DataRow)Otable.Rows[1];  // 可以改index 為 0 看看效果

            // Compare the two different rows.
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;

            bool bEqual = comparer.Equals(left, right);
            if (bEqual)
                Console.WriteLine("The two rows are equal");
            else
                Console.WriteLine("The two rows are not equal");

            // 相同內容，會有一樣的hashcodes。相異則相異，但不太關我的事。
            Console.WriteLine("The hashcodes for the two rows are {0}, {1}",
                comparer.GetHashCode(left),
                comparer.GetHashCode(right)); 
            #endregion

            // 2021-01-07 到這裡停止，到下一個教材的主題
            // 以下是我一路實作的MS官網，底下還有一些小目錄可以翻。 
            // https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/dataset-specific-operator-examples-linq-to-dataset

            Console.WriteLine("\n\n\n按一下退出");
            Console.ReadKey();
        }
    }
}
