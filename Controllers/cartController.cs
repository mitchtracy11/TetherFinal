using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tetherFinal.Models;

namespace tetherFinal.Controllers
{
    public class cartController : Controller
    {
        private tetherFinalEntities db = new tetherFinalEntities();

        // GET: /cart/
        [Authorize]
        public ActionResult Index(string clear)
        {
            if (clear != null)
            {
                checkout();
            }
            clear = null;
            double total = 0;
            var units = from o in db.Carts select o;
            string name = User.Identity.Name.ToString();

            var stuff = db.Carts.Where(o => o.Owner_ID.Contains(name));

            foreach (Cart price in stuff.ToList())
            {
                total = total + (double)price.Price;
            }
            ViewData["total"] = total;
            return View(stuff.ToList()); 
        }

        public string GetTotal()
        {
            double total = 0;
            var units = from o in db.Carts select o;
            string name = User.Identity.Name.ToString();

            var stuff = db.Carts.Where(o => o.Owner_ID.Contains(name));

                foreach (Cart price in stuff.ToList())
                {
                     total = total + (double)price.Price;
                }

                return total.ToString();
        }

        // GET: /cart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: /cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Owner_ID,ProductName,Price")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cart);
        }

        // GET: /cart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: /cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Owner_ID,ProductName,Price")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        // GET: /cart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }
        public void checkout()
        {
            string name = User.Identity.Name;
            var stuff = db.Carts.Where(o => o.Owner_ID.Contains(name));

            foreach (Cart cart in stuff)
            {
                db.Carts.Remove(cart);
                db.SaveChanges();
            }
        }
        // POST: /cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
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
