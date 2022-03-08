using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "hi";
            if(a is "hi" or "5")
            {
                Console.WriteLine("Hello World!");
            };
           
        }
    }
}
