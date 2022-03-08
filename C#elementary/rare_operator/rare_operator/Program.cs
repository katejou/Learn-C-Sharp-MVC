using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace rare_operator
{

    static class MyLinq
    {
        public static IEnumerable<int> Where(this int[] arr, Func<int, bool> cond)
        {
            // 這個 Where 明明是大寫的，但是呼叫時卻是小寫。
            // https://codertw.com/%E7%A8%8B%E5%BC%8F%E8%AA%9E%E8%A8%80/636305/
            return new int[] { 2, 4, 6, 8, 10, 12, 13, 15 };
            // 回傳的串列，會取代原本的串列，甚至無視掉select的算式結果。
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public Person(string fname, string mname, string lname, string cityName, string stateName)
        {
            FirstName = fname; MiddleName = mname; LastName = lname;
            City = cityName; State = stateName;
        }

        // Return the first and last name.
        public void Deconstruct(out string fname, out string lname)
        {
            fname = FirstName;
            lname = LastName;
        }
        /// <summary>
        /// 多型的 Deconstruct(1)
        /// </summary>
        public void Deconstruct(out string fname, out string mname, out string lname)
        {
            fname = FirstName; mname = MiddleName; lname = LastName;
        }
        /// <summary>
        /// 多型的 Deconstruct(2)
        /// </summary>
        public void Deconstruct(out string fname, out string lname, out string city, out string state)
        {
            fname = FirstName; lname = LastName;
            city = City; state = State;
        }
    }



    class Program
    {
        /// <summary>
        /// 回傳多值，而不用Tuple的方法 ( 省解Tuple的步驟 )
        /// </summary>
        private static (string, double, int, int, int, int) QueryCityDataForYears(string name, int year1, int year2)
        {

            int population1 = 0, population2 = 0; double area = 0;

            if (name == "New York City")
            {
                area = 468.48;
                if (year1 == 1960) population1 = 7781984;
                if (year2 == 2010) population2 = 8175133;

                return (name, area, year1, population1, year2, population2);
            }
            return ("", 0, 0, 0, 0, 0);
        }

        /// <summary>
        /// switch case 判斷
        /// </summary>
        private static void ProvidesFormatInfo(object obj)
        {
            switch (obj)
            {
                case IFormatProvider fmt:
                    Console.WriteLine($"1. {fmt} object");
                    break;
                case null:
                    Console.WriteLine("2. Null");
                    break;
                case object _:
                    Console.WriteLine("3. Object without format information");
                    break;
            }
        }



        static void Main(string[] args)
        {
            // Null 聯合運算子
            Console.WriteLine();
            Console.WriteLine("1.  Null 聯合運算子 ?? ");
            Console.WriteLine("「左 ??= 右」 如果左方是 null 的話，就被右方賦值 ");

            List<int> Z = null;
            //int a = null;  // 不加問號地寫會出錯，因為 int 型別本來是不允許為null值的
            // https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/nullable-value-types
            int? z = null;

            (Z ??= new List<int>()).Add(5);
            Console.WriteLine("\n(Z ??= new List<int>()).Add(5); \n運算結果︰" + string.Join(" ", Z));  // output: 5

            Z.Add(z ??= 6);
            Console.WriteLine("\nZ.Add(z ??= 6); \n運算結果︰" + string.Join(" ", Z));  // output: 5 6
            Console.WriteLine("z 的值是︰ " + z);  // output: 6

            // ??= 必須是變數、屬性或索引子 元素, 這個要在 C# 9 的環境下開，否則出錯。
            // 如果在 Visual Studio 2017 開過，失敗過。要重新開機，再回來 2019 這邊開。否則會無法建置。

            Console.WriteLine();
            Console.WriteLine("2. 捨棄 _ ");
            Console.WriteLine("int.TryParse('2', out _); \n運算答案 ︰ " + int.TryParse("2", out _)); // True

            bool a = int.TryParse("2", out int ans);
            Console.WriteLine("bool a = int.TryParse('2', out int ans); \n運算答案(ans) ︰ " + ans + " 而且bool(a)是 ︰ " + a);

            Console.WriteLine();
            Console.WriteLine("2.1 捨棄 _ 和 DateTime.TryParse 加 out 參數，迴圈 ");
            string[] dateStrings = {"05/01/2018 14:57:32.8",
                                    "2018-05-01 14:57:32.8",
                                    "2018-05-01T14:57:32.8375298-04:00",
                                    "5/01/2018",
                                    "5/01/2018 14:57:32.80 -07:00",
                                    "1 May 2018 2:57:32.8 PM",
                                    "16-05-2018 1:00:32 PM",
                                    "Fri, 15 May 2018 20:10:57 GMT" };
            foreach (string dateString in dateStrings)
            {
                if (DateTime.TryParse(dateString, out _))
                    Console.WriteLine($"'{dateString}': valid");
                else
                    Console.WriteLine($"'{dateString}': invalid");
            }

            Console.WriteLine();
            Console.WriteLine("3. 測試自己覆寫 where ");
            int[] arr = { 1, 2, 3, 4 };
            var query = from x in arr where x % 2 == 0 select x;
            foreach (int x in query) Console.Write(x + "    ");

            Console.WriteLine("\n");
            Console.WriteLine("5. 捨棄 _ 的進階，及用 ( , ) 接收或回傳多個參數，於同一個方法。 ");
            // https://docs.microsoft.com/zh-tw/dotnet/csharp/discards
            var (_, _, _, pop1, _, pop2) = QueryCityDataForYears("New York City", 1960, 2010);
            // 原本會回傳︰return (name, area, year1, population1, year2, population2)
            Console.WriteLine($"Population change, 1960 to 2010: {pop2 - pop1:N0}");
            // 輸出︰Population change, 1960 to 2010: 393,149
            // 393,149 是 pop2-pop1 的一個數字。    可以研究一下 : 的功能。

            Console.WriteLine("\n");
            Console.WriteLine("6. 捨棄 _ 的進階，物件的 Deconstruct 加 out 參數使用方法。 ");
            // Deconstruct + out 不用給任何引數，它只視乎接收的變數有多少而使用哪一個多型。
            var p = new Person("John", "Quincy", "Adams", "Boston", "MA");
            // Deconstruct 的呼叫，用不用方法名，而是用物件的實體名就好。
            var (fName, _, city, _) = p; // 使用了 : 多型的 Deconstruct(2)
            // 雖然 Deconstruct(2) 的回傳，表面上是 void, 但是它卻留意着有 4 個接收 out 的地方。
            // 帶出 _ 捨棄的另一個功能是 : 為多型佔位。
            Console.WriteLine($"Hello {fName} of {city}!");
            var (fName2, lName2, city2, state2) = p; // 對比
            Console.WriteLine($"Hello {fName} {lName2} of {city}, {state2}!");


            Console.WriteLine("\n");
            Console.WriteLine("6. 捨棄 _ 的進階，在switch case 的時候，使用 _ ");
            // case 在判斷物件之後，是可以用 「代號」取代物件，並於其下方 ，以代號執行物件。
            // 有代號所有的地方，使用 _ 。
            // 熟習 CultureInfo 以便日後開發國際網頁。
            // using System.Globalization;
            object[] objects = { CultureInfo.CurrentCulture,   // 1
                                 CultureInfo.CurrentCulture.DateTimeFormat,   //  1
                                 CultureInfo.CurrentCulture.NumberFormat,    // 1
                                 new ArgumentException(),  // 3
                                 null };  // 2 
            // 呼叫方法，列印
            foreach (var obj in objects) ProvidesFormatInfo(obj);

            Console.WriteLine("\n");
            Console.WriteLine("7. 捨棄 _ 的進階，_ 當作變數被賦值。 ");
            byte[] arrT = { 0, 0, 1, 2 };
            //var _ = BitConverter.ToInt32(arrT, 0);
            //Console.WriteLine(_);
            // 如果將 _ 具現出來當變數，會引發 125 142 行的出錯，因為編釋器會分不清楚 _ 是捨棄，還是變數。
            // 但是將這125 142 註解了之後，會列印出︰33619968

            Console.WriteLine("\n");
            Console.WriteLine("8. ?? 和 ??= , 1. Null 聯合運算子 的延伸說明 ");
            // https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/null-coalescing-operator

            int? A = null, b = null, c = 1, d = null, e = null, f = 2;

            A ??= b ?? c; // 如果 A 為 null, 跑右邊。如果 b 為 null, 回傳 c 給 A 當值。
            d ??= e ??= f; // 如果 b 為 null, e 給值 d。如果 e 為 null, 回傳 f 給值 e 。
            Console.WriteLine("A = " + A);
            Console.WriteLine("b = " + b);  // b 為 null, 因為沒有被給值
            Console.WriteLine("c = " + c);
            Console.WriteLine("d = " + d); // d 是 2，所以 ??= 是從最右邊開始的。
            Console.WriteLine("e = " + e);
            Console.WriteLine("f = " + f);

            // 同︰
            // a ??= (b ?? c)
            // d ??= (e ??= f)

            Console.WriteLine("\n");
            Console.WriteLine("9.  ?? 右方可以 throw Exception 而不是給值。 左方是 null 就 扔 exception ");
            try
            {
                int test = b ?? throw new ArgumentNullException(nameof(test), "Name cannot be null");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception throw and caugth : " + ex.Message);
            }
            // 印︰Exception throw and caugth : Name cannot be null (Parameter 'test')   

            Console.WriteLine("\n");
            Console.WriteLine("10. 運算子 default , 找出各型預設值︰");
            Console.WriteLine(default(int));  // output: 0
            Console.WriteLine(default(object) is null);  // output: True

            Console.WriteLine("\n");
            Console.WriteLine("10.1 綜合泛型﹑三元運算子﹑及 新型 string 中運算︰");

            // 嗯... 在 Main Method 之中的Method 是一真都在的嗎？我好像沒用過﹕
            void DisplayDefaultOf<T>()
            {
                var val = default(T);
                Console.WriteLine($"Default value of {typeof(T)} is {(val == null ? "null" : val.ToString())}.");
            }

            DisplayDefaultOf<int?>();
            DisplayDefaultOf<System.Numerics.Complex>();
            DisplayDefaultOf<System.Collections.Generic.List<int>>();

            Console.WriteLine("\n");
            Console.WriteLine("11. ! (null 容許) 運算子");
            // https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/null-forgiving
            // 原本不允許輸入 null 為引數的地方，用 ! 強迫塞入。好像 _ 的相反。
            try
            {
                var people = new People(null!);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n由null容許運算子，導致扔出 : " + ex.Message);
            }

            Console.WriteLine("\n");
            Console.WriteLine("12. ? (null 容許) 運算子，用於型別之後，非變數名之後。");
            
            // 兩者配合使用的例子︰
            People? people2 = new People("John");
            if (IsValid(people2))
            {
                Console.WriteLine($"Found {people2!.Name}");
            }

            // 反例︰
            if (!IsValid(null!))
            {
                Console.WriteLine($"Found Nobody! ");
            }


            // Keep the console open in debug mode.
            Console.WriteLine(System.Environment.NewLine); // 空一行 = \n\n
            Console.WriteLine("Press any key to exit");
            //Console.ReadKey();

        }
        public class People
        {
            // People 的建構子，要需入一個引數，設定屬性 Name 為引數，但如果 引數為null 的話，扔 Exception
            public People(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

            public string Name { get; }
        }
        public static bool IsValid(People? p)
        {
            return p != null && !string.IsNullOrEmpty(p.Name);
        }


    }
}
