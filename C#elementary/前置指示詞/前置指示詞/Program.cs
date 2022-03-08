#define A
#define C
using System;

namespace 前置指示詞
{
    class Program
    {
        static void Main(string[] args)
        {

#if (A && B )
            Console.WriteLine("A");
#elif B
            Console.WriteLine("B");
#elif ( A && C )
            Console.WriteLine("C");
#else
            Console.WriteLine("D");
#endif
            Console.ReadLine();


        }
    }
}
