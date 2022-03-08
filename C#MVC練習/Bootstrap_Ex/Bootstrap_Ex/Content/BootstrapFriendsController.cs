using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bootstrap_Ex.Data;
using Bootstrap_Ex.Models;

namespace Bootstrap_Ex.Content
{
    public class BootstrapFriendsController : Controller
    {
        private FriendContext db = new FriendContext();

        // GET: BootstrapFriends
        public ActionResult Index()
        {
            return View(db.BootstrapFriends.ToList());
        }

        // GET: BootstrapFriends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BootstrapFriend bootstrapFriend = db.BootstrapFriends.Find(id);
            if (bootstrapFriend == null)
            {
                return HttpNotFound();
            }
            return View(bootstrapFriend);
        }

        // GET: BootstrapFriends/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BootstrapFriends/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Phone")] BootstrapFriend bootstrapFriend)
        {
            if (ModelState.IsValid)
            {
                db.BootstrapFriends.Add(bootstrapFriend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bootstrapFriend);
        }

        // GET: BootstrapFriends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BootstrapFriend bootstrapFriend = db.BootstrapFriends.Find(id);
            if (bootstrapFriend == null)
            {
                return HttpNotFound();
            }
            return View(bootstrapFriend);
        }

        // POST: BootstrapFriends/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Phone")] BootstrapFriend bootstrapFriend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bootstrapFriend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bootstrapFriend);
        }

        // GET: BootstrapFriends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BootstrapFriend bootstrapFriend = db.BootstrapFriends.Find(id);
            if (bootstrapFriend == null)
            {
                return HttpNotFound();
            }
            return View(bootstrapFriend);
        }

        // POST: BootstrapFriends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BootstrapFriend bootstrapFriend = db.BootstrapFriends.Find(id);
            db.BootstrapFriends.Remove(bootstrapFriend);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
