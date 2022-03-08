using System;
using System.Collections.Generic;

namespace yield_study
{
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        public static IEnumerable<int> enumerableFuc()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        /// <summary>
        /// 在這個「集合」中，break 比 return 的權力更大，可以中斷代迭。return 是代迭的工具
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> enumerableFuc_break()
        {
            yield return 1;
            yield return 2;
            yield break;
            yield return 3;
        }

        /// <summary>
        /// 回傳代迭除非 = 4
        /// </summary>
        /// <returns></returns>
        static IEnumerable<object> Not4(List<string> inputs)
        {
            foreach (var item in inputs)
            {
                if (item != "4")
                    yield return item;
            }

            Console.Write(" {0} ", "Holle Danny");
        }

        /// <summary>
        /// 在代迭 = 4 時中斷(呼叫的那一個迴圈)
        /// </summary>
        static IEnumerable<object> Break4(List<string> inputs)
        {
            foreach (var item in inputs)
            {
                if (item != "4")
                    yield return item;
                else
                    yield break;
            }

            Console.Write(" {0} ", "Holle Danny");
        }

        /// <summary>
        /// 一般的方式
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Person> GetPersons1()
        {
            List<Person> emps = new List<Person>();
            for (int i = 0; i < 5; i++)
                emps.Add(new Person() { Id = i, Name = "User " + i });
            return emps;
        }

        /// <summary>
        /// by yield
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Person> GetPersons2()
        {
            for (int i = 0; i < 5; i++)
                yield return (new Person() { Id = i, Name = "User" + i });
        }




        static void Main(string[] args)
        {
            // https://www.cnblogs.com/blueberryzzz/p/8678700.html

            foreach (int item in enumerableFuc())
                Console.WriteLine(item);
            Console.WriteLine("************************");
            foreach (int item in enumerableFuc_break())
                Console.WriteLine(item);
            // 在這個「集合」中，break 比 return 的權力更大，可以中斷代迭。return 是代迭的工具

            // https://dotblogs.com.tw/dc690216/2010/02/19/13697
            // 這個「集合工具」和 其他集合 配合使用，成為篩選器

            List<string> docs1 = new List<string>();
            docs1.Add("1");
            docs1.Add("2");
            docs1.Add("3");
            docs1.Add("4");
            docs1.Add("5");
            Console.WriteLine("************************");
            foreach (var item in Not4(docs1))
                Console.Write(" {0} ", item);
            // 回傳代迭除非 = 4, ( 用作 continue(skip) )

            Console.WriteLine();
            Console.WriteLine("************************");
            foreach (var item in Break4(docs1))
                Console.Write(" {0} ", item);
            Console.WriteLine();

            // https://jasper-it.blogspot.com/2015/01/c-yield.html
            // yield 回傳 obiect。不從Main傳入物件。直接在「集合」中生成。
            // 作用︰節省物件回傳的程式嗎？雖然我覺沒差很多…
            Console.WriteLine("===== Without yield ====");
            IEnumerable<Person> emps1 = GetPersons1();
            foreach (var item in emps1)
                Console.WriteLine("Id={0}, Name={1}", item.Id, item.Name);

            Console.WriteLine("===== With yield ====");
            IEnumerable<Person> emps2 = GetPersons2();
            foreach (var item in emps2)
                Console.WriteLine("Id={0}, Name={1}", item.Id, item.Name);


        }
    }
}
