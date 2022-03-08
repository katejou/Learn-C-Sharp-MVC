using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Model_First
{
    class Program
    {
        static void Main(string[] args)
        {
            AddDataBasic();
            AddData();
            AddRangeData();

            DisplayData();

            Console.ReadKey();
        }

        /// <summary>
        /// 基本的foreach新增
        /// </summary>
        static void AddDataBasic()
        {
            BlogContext context = new BlogContext();
            if (context.Users.Any())
            {
                Console.WriteLine("樣本資料已存在，不再新增");
                return;
            }

            // 最笨的
            Users silly = new Users(); // UsersUserId = 1
            silly.UserName = "Silly";
            silly.Email = "Silly_Mail";
            //silly.Phone = null;
            context.Users.Add(silly);
            context.SaveChanges();

            // 聰明一點點
            Users smarter = new Users { UserName = "Smarter", Email = "Smarter_Mail" };
            context.Users.Add(smarter); // UsersUserId = 2
            context.SaveChanges();

            // 最方便
            // 1. 建立List<>組合
            List<Users> users = new List<Users>
            {
                new Users{ UserName="Kate", Email="kate@email.com" }, // UsersUserId = 3
                new Users{ UserName="Tom",  Email="tom@email.com" },
                new Users{ UserName="Mary", Email="mary@email.com"}
            };
            foreach (var d in users)
                context.Users.Add(d);
            context.SaveChanges();
            // 要先存好，不能和下方一同SaveChanges()，因為Id具關連性。
            List<Blog> blogs = new List<Blog>
            {
                new Blog{ UsersUserId=3, BlogName="Kate's Blog", Url="http://www.kate_blog.com.tw"},
                new Blog{ UsersUserId=4, BlogName="Tom's Blog", Url="http://www.tom_blog.com.tw"},
                new Blog{ UsersUserId=5, BlogName="Mary's Blog", Url="http://www.mary_blog.com.tw"}
            };
            foreach (var d in blogs)
                context.Blog.Add(d);
            context.SaveChanges();

            List<Post> posts = new List<Post>
            {
                new Post{ BlogBlogId=1, Title="Kate's Post", Content="Kate said"},
                new Post{ BlogBlogId=2, Title="Tom's Post", Content="Tom said"},
                new Post{ BlogBlogId=3, Title="Mary's Post", Content="Mary said"}
            };
            foreach (var d in posts)
                context.Post.Add(d);
            context.SaveChanges();

            context.Dispose();
            Console.WriteLine("樣本資料已新增成功");


        }


        /// <summary>
        /// Linq的Foreach新增
        /// </summary>
        static void AddData()
        {
            BlogContext context = new BlogContext();
            if (context.Users.Find(6) != null)
            {
                Console.WriteLine("樣本資料已存在，不再新增");
                return;
            }

            // 1. 建立List<>組合
            List<Users> users = new List<Users>
            {
                new Users{ UserName="Peter", Email="peter@email.com" }, // UsersUserId = 6
                new Users{ UserName="John",  Email="john@email.com" },
                new Users{ UserName="Larry", Email="larry@email.com"}
            };
            users.ForEach(x => context.Users.Add(x));
            context.SaveChanges();
            // 要先存好，不能和下方一同SaveChanges()，因為Id具關連性。
            List<Blog> blogs = new List<Blog>
            {
                new Blog{ UsersUserId=6, BlogName="Peter's Blog", Url="http://www.peter_blog.com.tw"},
                new Blog{ UsersUserId=7, BlogName="John's Blog", Url="http://www.john_blog.com.tw"},
                new Blog{ UsersUserId=8, BlogName="Larry's Blog", Url="http://www.larry_blog.com.tw"}
            };
            blogs.ForEach(x => context.Blog.Add(x));
            context.SaveChanges();
            // BlogBlogId 頭 10 筆已用。
            List<Post> posts = new List<Post>
            {
                new Post{ BlogBlogId=4, Title="Peter's Post", Content="Peter said"},
                new Post{ BlogBlogId=5, Title="John's Post", Content="John said"},
                new Post{ BlogBlogId=6, Title="Larry's Post", Content="Larry said"}
            };
            posts.ForEach(x => context.Post.Add(x));
            context.SaveChanges();

            context.Dispose();
            Console.WriteLine("樣本資料已新增成功");

        }


        /// <summary>
        /// AddRange的新增
        /// </summary>
        static void AddRangeData()
        {
            // 1. 建立List<>組合
            List<Users> users = new List<Users>
            {
                new Users{ UserName="Rose", Email="rose@email.com" }, // UsersUserId = 9
                new Users{ UserName="Love",  Email="love@email.com" },
                new Users{ UserName="Peach", Email="peach@email.com"}
            };
            List<Blog> blogs = new List<Blog>
            {
                new Blog{ UsersUserId=9, BlogName="Rose's Blog", Url="http://www.rose_blog.com.tw"},
                new Blog{ UsersUserId=10, BlogName="Love's Blog", Url="http://www.love_blog.com.tw"},
                new Blog{ UsersUserId=11, BlogName="Peach's Blog", Url="http://www.peach_blog.com.tw"}
            };
            // BlogBlogId 頭 13 筆已用。
            List<Post> posts = new List<Post>
            {
                new Post{ BlogBlogId=7, Title="Rose's Post", Content="Rose said"},
                new Post{ BlogBlogId=8, Title="Love's Post", Content="Love said"},
                new Post{ BlogBlogId=9, Title="Peach's Post", Content="Peach said"}
            };

            using (var context = new BlogContext())
            {
                if (context.Users.Find(9) == null)
                {
                    context.Users.AddRange(users);
                    context.SaveChanges();

                    context.Blog.AddRange(blogs);
                    context.SaveChanges();

                    context.Post.AddRange(posts);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("樣本資料已存在，不再新增");
                    return;
                }

            }

            Console.WriteLine("樣本資料已新增成功");

        }


        /// <summary>
        /// 顯示資料
        /// </summary>
        static void DisplayData()
        {
            using (var db = new BlogContext())
            {

                Console.WriteLine("\n顯示所有 Users : ");
                Console.WriteLine("+++++++++++++++++++++++++++ ");
                // 查詢全部方法一
                var allUser_1 = from u in db.Users select u;
                foreach (var u in allUser_1)
                    Console.WriteLine($"{u.UserId} , {u.UserName} , {u.Email}");
                Console.WriteLine("\n顯示部份+排序 Users : ");
                Console.WriteLine("+++++++++++++++++++++++++++ ");
                // 查詢局部方法一
                var someUser_1 = from u in db.Users
                                 where u.UserId >= 3 && u.UserId <= 5
                                 orderby u.UserId descending
                                 select u;
                foreach (var u in someUser_1)
                    Console.WriteLine($"{u.UserId} , {u.UserName} , {u.Email}");
                Console.WriteLine("\n使用Foreach方法來篩選 : ");
                Console.WriteLine("+++++++++++++++++++++++++++ ");
                // 查詢全部方法二
                var allUser_2 = db.Users.ToList();
                // Foreach方法來篩選(全部)
                allUser_2.ForEach(
                    u =>
                    {
                        if (u.UserName.Contains("Peter")
                        || u.UserName == "John"
                        || u.UserName == "Larry")
                        {
                            Console.WriteLine($"{u.UserId} , {u.UserName} , {u.Email}");
                        }
                    }
                );

                Console.WriteLine("\n顯示所有 Blog : ");
                Console.WriteLine("+++++++++++++++++++++++++++ ");
                // 查詢全部方法一
                var allBlog_1 = from b in db.Blog select b;
                // .ToList().Foreach方法 
                allBlog_1.ToList().ForEach(
                    b =>
                    {
                       Console.WriteLine($"{b.BlogId} , {b.BlogName} , {b.Url}");
                    } 
                );
                Console.WriteLine("\n顯示所有 Post 來自什麼 Blog : ");
                Console.WriteLine("+++++++++++++++++++++++++++ ");
                // 查詢全部方法二
                var allPost_2 = db.Post.ToList();
                // Foreach方法 
                allPost_2.ForEach(
                    p =>
                    {
                        Console.WriteLine($"BlogID : {p.BlogBlogId} ,\n" +
                            $"BlogName : {p.Blog.BlogName}\n" +
                            $"User : {p.Blog.Users.UserName}\n" +
                            $"{p.PostId} , {p.Title} , {p.Content}\n");
                    }
                );


            }
        }

    }
}
