using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_CodeFirstNewDB.Models;

namespace EF_CodeFirstNewDB
{
    class Program
    {
        static void Main(string[] args)
        {
            // 我感覺自己已經實作過這件事好幾次了，只是這次以這個角度來做。
            // 得知道算沒有在App.config 中寫好連線，也沒有在Context.cs的建構子指定連線，它依然可以Mrigrate出一個資料庫。
            using (var context = new BlogContext())
            {
                // 存
                if (context.Users.Any())
                {
                    // 新增 Entity = 資料行 = 按 class 的設定
                    User user = new User { UserName = "Kate", Email = "kate@mail.com" };
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                // 讀
                foreach (var item in context.Users)
                    Console.WriteLine($"Name : {item.UserName}, Email : {item.Email}");

                // hold
                Console.ReadKey();

            }

        }
    }
}
