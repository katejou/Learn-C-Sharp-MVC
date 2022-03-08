using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirstExistingDB
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var context = new CodeFirst_ExistDB_Blog()) 
            {
                var post = from p in context.Post select p;
                foreach (var p in post) 
                {
                    Console.WriteLine($" User : {p.Blog.Users.UserName}, Blog : {p.Blog.BlogName}, Title : {p.Title}, Content : {p.Content}");
                }
            }

            Console.ReadKey();
        }
    }
}
