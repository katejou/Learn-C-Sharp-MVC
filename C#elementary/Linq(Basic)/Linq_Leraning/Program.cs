using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq_Learning
{
    class Program
    {
        static IEnumerable<IEnumerable<int>> AReallyComplexTower(int[] arr, int n)
        {
            IEnumerable<IEnumerable<int>> result = arr.Select(x => new List<int>() { x });
            for (int i = 1; i < n; i++)
                result = result.SelectMany(x => arr.Where(y => y > x.Max()), (x, y) => x.Concat(new int[] { y }));
            return result;
        }
        static void Main(string[] args)
        {
            // LINQ 的特點︰
            // 您可以使用相同的基本查詢運算式模式，
            // 來查詢並轉換 ︰
            // SQL 資料庫
            // ADO.NET 資料集 (DataSet,DataTable)
            // XML 文件及資料流
            // .NET 集合 (List,Array,Object....etc?)中的資料


            // 基礎一︰
            // https://docs.microsoft.com/zh-tw/dotnet/csharp/linq/#query-expression-overview

            // Specify the data source.
            int[] scores = new int[] { 97, 92, 81, 60 };

            // Define the query expression.
            IEnumerable<int> scoreQuery = from score in scores
                                          where score > 80
                                          select score;

            // score 是一個臨時的變數，同foreach之中的 var X in YY 的 X

            Console.WriteLine("1. 篩選80分以上");
            Console.WriteLine("分數︰ 97, 92, 81, 60 ");
            Console.Write("結果︰ ");
            // Execute the query.
            foreach (int i in scoreQuery)
                Console.Write(" " + i + " ");
            Console.WriteLine(System.Environment.NewLine);

            // ----------------------------------------------------------------------

            // 基礎二︰
            // https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/concepts/linq/introduction-to-linq-queries

            // The Three Parts of a LINQ Query:
            // 1. Data source.
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            // 2. Query creation.
            // numQuery is an IEnumerable<int>
            var numQuery = from num in numbers
                           where (num % 2) == 0
                           select num;

            // 3. Query execution.
            Console.WriteLine("2. 篩選雙數");
            Console.WriteLine("數字︰ 0, 1, 2, 3, 4, 5, 6 ");
            Console.Write("結果︰ ");
            foreach (int num in numQuery)
                Console.Write("{0,1} ", num);

            // {0,1} 的意思
            // https://stackoverflow.com/questions/35898370/what-does-0-1-num-mean
            // 0 is the placeholder 1 is the size of the field 

            Console.WriteLine();
            Console.WriteLine("共用多少個(.Count())雙數 ︰ {0}", numQuery.Count());


            Console.WriteLine("\n可以在搜尋後馬上改型別，不用IEnumerable<>");
            List<int> numQuery2 = (from num in numbers
                                   where (num % 2) == 0
                                   select num).ToList();   // ToList 還可以逼使它馬上執行，詳見筆記。
            Console.Write("ToList︰ ");
            foreach (int num in numQuery2)
                Console.Write("{0,1} ", num);
            Console.WriteLine();


            var numQuery3 = (from num in numbers
                             where (num % 2) == 0
                             select num).ToArray();
            Console.Write("ToArray︰ ");
            foreach (int num in numQuery3)
                Console.Write("{0,1} ", num);
            Console.WriteLine(System.Environment.NewLine);


            // 進階一
            // LINQ to Lambda (=>) 運算式
            Console.WriteLine("3.  從 Link 到 Lambda");
            int[] numbers1 = { 5, 10, 8, 3, 6, 12 };

            //Query syntax:   (Link)
            IEnumerable<int> numQuery1 = from num in numbers1
                                         where num % 2 == 0
                                         orderby num
                                         select num;
            Console.Write("Link︰ ");
            foreach (int i in numQuery1)
                Console.Write(i + " ");
            Console.WriteLine(); // 下一行 \n

            //Method syntax:  (Lambda)
            IEnumerable<int> numQuery21 = numbers1.Where(num => num % 2 == 0).OrderBy(n => n);
            // num 是 each numbers1 , n 是 上層篩下來的結果的代稱？，幻想出︰結果numQuery21是輸出句子的最後面…
            // 「傳回值」就是運算式結果
            Console.Write("Lambda︰ ");
            foreach (int i in numQuery21)
                Console.Write(i + " ");

            Console.WriteLine("\n");
            Console.WriteLine("3.1  Lambda 的另一個例子");
            int[] numbersAry = { 2, 3, 4, 5 };
            var squaredNumbers = numbersAry.OrderByDescending(x => x).Select(y => y * y);
            //var squaredNumbers = numbersAry.OrderByDescending(x => x).Select(x => x * x);
            // 不一定要改代數的名稱。
            Console.WriteLine(string.Join(" ", squaredNumbers));
            // Output:
            // 25 16 9 4

            Console.WriteLine(System.Environment.NewLine);

            // 進階二
            // Lambda (=>) 運算式
            // https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/lambda-expressions
            Console.WriteLine("4.  Lambda 的  Func 委派型別。收，也回傳");
            Func<int, int> square = x => x * x;
            Console.WriteLine(" Func<int, int> square = x => x * x ; \n運算答案 ︰ " + square(5));
            // Func 是型別， <int, int> 不是泛型， 第一個 int 是接收參數的型別 ， 第二個 int 是回傳參數的型別。

            Console.WriteLine();
            Console.WriteLine("4.1. Func 委派型別。收 2 個參數");
            Func<int, int, bool> testForEquality = (x, y) => x == y;
            Console.WriteLine("Func<int, int, bool> testForEquality = (x, y) => x == y ; \n運算答案 ︰ " + testForEquality(5, 5));

            Console.WriteLine();
            Console.WriteLine("4.1.1 Func 委派型別。收 2 個參數 + 有時編譯器無法推斷輸入參數的類型，可以再次明確指定");
            Func<int, string, bool> isTooLong = (int x, string s) => s.Length > x;
            Console.WriteLine("Func<int, string, bool> isTooLong = (int x, string s) => s.Length > x ; \n運算答案 ︰ " + isTooLong(5, "我是不是太長"));

            Console.WriteLine();
            Console.WriteLine("4.1.2 從 c # 9.0 開始，您可以使用 [ 捨棄 ] 來指定運算式中未使用之 lambda 運算式的兩個或多個輸入參數,(以前只能用一個)");
            Func<int, int, int> constant = (_, _) => 0;
            Console.WriteLine("Func<int, int, int> constant = (_, _) => 0 ; \n運算答案  ︰ " + constant(5, 3));
            constant = (_, x) => x;
            Console.WriteLine("修改Func 的算式︰constant = (_, x) => x; \n運算答案  ︰ " + constant(5, 3));

            Console.WriteLine();
            Console.WriteLine("4.1.3 Func 委派型別。把算式列印出來的方法︰ ");
            System.Linq.Expressions.Expression<Func<int, int>> e = x => x * x;
            Console.WriteLine(e); // ?? 除了把算式列印出來有什麼用
            //Console.WriteLine(e(2));  
            //Console.WriteLine(x(2));  // <-- 這兩個都會出錯，那我是要如何才用得到 x 來運算呢？

            Console.WriteLine();
            Console.WriteLine("5.  Lambda 的  Action 委派型別。只收，不回傳");
            Action<string> greet = name =>  // Action 這個<>泛型裡，指的是接收參數型別。 
            {// name 是參數的代詞 , greet 是方法名稱
                string greeting = $"Hello {name}!";
                Console.WriteLine(greeting);
            };
            greet("World");  // 注意，它呼叫的時候，已經同時做了動作，並沒有回傳值 ( 同 void )

            // Output:
            // Hello World!
            Action line = () => Console.WriteLine("\n5.1 以()指定零個參數");
            line();

            // 回頭去看 4 和 4.1 ，注意 Lambda 的一個參數是可以不加 ()，但兩個以上，或無參數就要 ()

            Console.WriteLine();
            Console.WriteLine("6. 用 = > 覆寫簡單方法");
            People John = new People("John", 18);
            Console.WriteLine(John.ToString());


            // 非同步 Lambda
            Console.WriteLine();
            Console.WriteLine("7. 非同步 Lambda");
            Console.WriteLine("這個要用 Web Form 來測，但目前只要記住 await 和 async 這兩個字眼就好，應該是和 Threading 差不多。");


            //  基礎練習︰https://codertw.com/%E7%A8%8B%E5%BC%8F%E8%AA%9E%E8%A8%80/636305/
            Console.WriteLine();
            Console.WriteLine(" >>>>> 基礎練習︰ >>>>>>");
            Console.WriteLine();

            Console.WriteLine(">1. Lambda 比 Link 難看，因為 Link 像 SQL。但是 Lambda 比較精簡");
            Console.WriteLine();
            Console.WriteLine("from x in arr where x % 2 == 0 select x;");
            Console.WriteLine("arr.Where(x => x % 2 == 0);");
            Console.WriteLine();
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8 };
            var query = from x in arr where x % 2 == 0 select x;
            foreach (int x in query) Console.Write(x + "\t");
            Console.WriteLine();
            var queryq = arr.Where(x => x % 2 == 0);
            foreach (int x in queryq) Console.Write(x + "\t");
            Console.WriteLine("\n");

            Console.WriteLine(">2. 將條件，化回為一般回傳bool的方法");
            Console.WriteLine("\nqueryw = arr.Where(myfunc)\nmyfunc 是接收 int , 作一個對比是否小於 5 再回傳bool的方法\n");
            var queryw = arr.Where(myfunc);
            foreach (int x in queryw) Console.Write(x + "\t");
            Console.WriteLine();
            Console.WriteLine("結論1︰ 等價於 arr.Where(x => x < 5) \n結論2︰ Lambda 表示式的本質是一個匿名的方法，箭頭前面的部分是它的引數，後面的語句就是這個函式(於True時)會返回該值。");
            Console.WriteLine("\n");

            Console.WriteLine(">3. 操作 select ，何為 projection 投影。");
            var arr2 = arr.Select(x => x * 2);
            Console.WriteLine("select 是 (有條件)複製原本的東東，到另一個新的東東的方法。 ");
            foreach (int x in arr2) Console.Write(x + "\t");

            Console.WriteLine();
            List<People> list = new List<People> { new People("Peter", 10), new People("Mary", 11) };
            var arr3 = list.Select(x => x.name);
            foreach (string x in arr3) Console.Write(x + "\t");

            Console.WriteLine("\n");
            Console.WriteLine(">3.1 操作 select 過載形式");
            string s = "hello";
            Console.WriteLine("s = \"hello\"");
            Console.WriteLine("s.Select((x, i) => new { x, i })");
            var queryH = s.Select((x, i) => new { x, i });
            foreach (var item in queryH) Console.WriteLine(item);
            Console.WriteLine("當只有一個參數輸入，卻有兩個等待輸入的接口，第二個接口會自動變成index\n");
            // item 是 IEunmable 之中的一個元素


            Console.WriteLine(">4. 操作 OrderBy﹑GroupBy, 運用 IGroup 型別\n");
            int[] scores1 = { 90, 65, 82, 71, 84, 88, 52, 78, 61, 75, 85, 79 };
            var query1 = scores1.OrderBy(x => x).GroupBy(x => x / 10);
            // query1 是IGroup 型別
            foreach (var g in query1)
            {
                Console.Write("group key = {0} values = ", g.Key);
                foreach (var item in g) Console.Write(item + " ");
                Console.WriteLine();
            }

            Console.WriteLine("\n");
            Console.WriteLine(">5. SelectMany \n");
            Console.WriteLine(">5.1 SelectMany像是 GroupBy 的反操作, 連接起群組︰");
            var query2 = query1.SelectMany(x => x);
            foreach (var item in query2) Console.Write(item + " ");

            Console.WriteLine("\n");
            Console.WriteLine(">5.2 SelectMany 是 乘法的投射︰");
            string[] fruits = { "橘子", "香蕉", "西瓜", "蘋果" };
            string[] people = { "張三", "李四" };
            var queryf = people.SelectMany(x => fruits.Select(y => x + " 喜歡吃 " + y));
            // fruits.Select讓 y 成為「水果」，people.SelectMany 讓 x 成為「人」。
            // SelectMany 是 兩個select相乘，迴圈組成多句句子，再合成新的 queryf , foreach 將 queryf 一句句列印出來。
            foreach (var item in queryf) Console.WriteLine(item);

            Console.WriteLine("\n");
            Console.WriteLine("> 5.2 SelectMany 的省略寫法︰");
            Console.WriteLine("people.SelectMany(x => fruits.Select(y => x + \" 喜歡吃 \" + y));");
            Console.WriteLine("people.SelectMany(x => fruits,(x, y) => x +\" 喜歡吃 \"+ y);");
            var queryf2 = people.SelectMany(x => fruits, (x, y) => x + " 喜歡吃 " + y);
            foreach (var item in queryf2) Console.WriteLine(item);

            Console.WriteLine("\n");
            Console.WriteLine("> 5.3 實驗Linq之中代數的關係︰");
            Console.WriteLine("people.SelectMany(x => fruits,(x, y) => x +\" 喜歡吃 \"+ y);");
            Console.WriteLine("people.SelectMany(x => fruits,(y, x) => y +\" 喜歡吃 \"+ x);\n");
            var queryf3 = people.SelectMany(x => fruits, (y, x) => y + " 喜歡吃 " + x);
            foreach (var item in queryf3) Console.WriteLine(item);
            Console.WriteLine("\n");
            Console.WriteLine("結果兩者相等，所以 fruits,( 之後的 x ， 並不是 SelectMany( 之後的 x  ");

            Console.WriteLine("\n");
            Console.WriteLine("> 6  Skip 和 Take，類似 substring ");

            int[] arrV = { 1, 2, 3, 4, 5 };
            Console.WriteLine("1, 2, 3, 4, 5\n");
            Console.WriteLine(".Skip(1)");
            var sk = arrV.Skip(1);
            foreach (var item in sk) Console.Write(item + " "); Console.WriteLine("\n");

            Console.WriteLine(".Skip(1).Take(2)");
            var st = arrV.Skip(1).Take(2);
            foreach (var item in st) Console.Write(item + " "); Console.WriteLine("\n");

            Console.WriteLine(".Skip(100).Take(10)");
            var over = arrV.Skip(100).Take(10);
            foreach (var item in over) Console.Write(item + " ");
            Console.WriteLine("回傳為空，但沒有出錯");

            Console.WriteLine("\n");
            Console.WriteLine("> 7  Aggregate ");
            string[] stations = { "北京", "石家莊", "鄭州", "武漢", "衡陽", "廣州" };
            List<string> result = new List<string>();
            var queryAg = stations.Skip(1)
                                  .Aggregate(stations[0], (acc, curr) =>
                                  { result.Add(acc + "->" + curr); return curr; },
                                  x => "");
            foreach (var item in result) Console.WriteLine(item);
            // 全句以我的理解是這樣的:
            // Slip(1), 由第二個元素開始。
            // Aggreate( 第一個開始當 acc 的是誰？ , ( acc , curr (=station中尚未輪到的元素)) =>
            // { 加入 List<string> result , 將 curr return 為下一個 acc },
            // x => "");     // = Aggreate 自己不要不生出任何東西，所以 queryAg 是空的。
            foreach (var item in queryAg) Console.Write(item + " ");
            // 証實 queryAg 為空。

            Console.WriteLine();
            Console.WriteLine(" >>>>> 新手常犯的錯︰ >>>>>>");


            Console.WriteLine("\n");
            Console.WriteLine("> 1  忘記 延遲執行的這回事︰ ");

            List<int> lili = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            var qy = lili.Where(x => x % 2 == 0);
            lili.Add(10);
            lili.Add(12);
            foreach (int i in qy) Console.Write(i + " ");  // 延遲到現在才執行
            // 2 4 6 8 10 12
            Console.WriteLine();
            var qry = lili.Where(x => x % 2 == 0).ToList(); // 立即執行
            lili.Add(14);
            lili.Add(16);
            foreach (int i in qry) Console.Write(i + " ");
            // 2 4 6 8 10 12

            Console.WriteLine("\n");
            Console.WriteLine("> 2  忘記 回傳的是IEnumerable型別︰ ");
            lili = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            lili = lili.Where(x => x % 2 == 0).ToList();
            // 直接刪減 lili ，記得要加 .ToList , 否則的話，Lambda 所回傳的是IEnumerable型別，而不是List<int>
            foreach (int i in lili) Console.Write(i + " ");
            // 2,4,6,8

            Console.WriteLine("\n");
            Console.WriteLine("> 2.1  即使只回傳一個，也是IEnumerable型別︰可以運用.Frist() / Single 避免 ");
            //int INT = lili.Where(x => x == 1);
            //int INT = lili.Where(x => x == 1).Take(1);
            // 上述會出錯
            int INT = lili.Where(x => x == 2).First();
            Console.WriteLine("int INT = lili.Where(x => x == 2).First() : " + INT);
            INT = lili.Where(x => x == 4).Take(1).First();
            Console.WriteLine("INT = lili.Where(x => x == 4).Take(1).First() : " + INT);
            INT = lili.Where(x => x == 6).Take(1).Single();
            Console.WriteLine("INT = lili.Where(x => x == 6).Take(1).Single() : " + INT);

            Console.WriteLine("\n");
            Console.WriteLine("> 2.2 Max()、Average()、Aggregate() 也是回傳單一元素 ");
            INT = lili.Where(x => x < 7).Max();
            Console.WriteLine("INT = lili.Where(x => x < 7).Max() : " + INT);
            double Dou = lili.Where(x => x < 7).Average();
            Console.WriteLine("double Dou = lili.Where(x => x < 7).Average() : " + Dou);
            string str3 = lili.Where(x => x < 7).Aggregate(lili[0], (start, next) => { return next; }, x => "The only return of Aggregate");
            Console.WriteLine(str3);

            Console.WriteLine();
            Console.WriteLine(" >>>>> 進階練習︰ >>>>>>");
            Console.WriteLine();

            Console.WriteLine("> 1 隨機洗牌 ");
            lili = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            lili = lili.OrderBy(x => Guid.NewGuid()).ToList();
            foreach (int i in lili)
                Console.Write(i + " ");

            Console.WriteLine();
            lili = lili.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
            foreach (int i in lili)
                Console.Write(i + " ");
            Console.WriteLine("\n");

            Console.WriteLine("> 2 一個很複雜高深的陣列，估計沒有用，不學下去了。 ");
            int[] rr = { 1, 2, 3, 4 };
            for (int i = 1; i <= rr.Length; i++)
            {
                var rt = AReallyComplexTower(rr, i);
                foreach (var item in rt)
                {
                    foreach (int x in item)
                        Console.Write(x + " ");
                    Console.WriteLine();
                }
            }


            //題外︰ Lambda 不允許多型。

            // Keep the console open in debug mode.
            Console.WriteLine(System.Environment.NewLine); // 空一行 = \n\n
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            //Console.Read();  // 這個新版有自動去卡的功能，不用特意寫這一行。
            //Console.ReadKey();  // 目前我覺得Read 和 ReadKey 是一樣的，沒有差
        }
        static bool myfunc(int x)
        {
            return x < 5;
        }


    }
    internal class People
    {
        internal string name;
        internal int age;
        internal People(string x, int y)
        {
            name = x;
            age = y;
        }

        /// <summary>
        /// 以Lambda覆寫方法。 => 用來取代 return
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"NAME :{name} AGE: {age}".Trim();
        // 相等於︰
        //public override string ToString()
        //{
        //    return $"NAME :{name} AGE: {age}".Trim();
        //}
    }


}

