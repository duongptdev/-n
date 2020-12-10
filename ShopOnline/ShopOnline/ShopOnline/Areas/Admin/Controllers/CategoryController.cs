using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        private ShopOnlineDbContext db = new ShopOnlineDbContext();
        public ActionResult Index()
        {
            var itemTypes = db.Category.Include(i => i.Menu);
            return View(itemTypes.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.MenuID = new SelectList(db.Menu, "ID", "Text");
            return View();
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuID = new SelectList(db.Menu, "ID", "Text", category.MenuID);
            return View(category);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuID = new SelectList(db.Menu, "ID", "Text", category.MenuID);
            return View(category);
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var currentCulture = Session[CommonConstants.CurrentCulture];

                var id = new CategoryDao().Insert(category);
                if (id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {

                }
            }
            ViewBag.MenuID = new SelectList(db.Menu, "ID", "Text", category.MenuID);
            return View(category);
        }

    }
}