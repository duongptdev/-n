using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Models;
using System.Web.Script.Serialization;
using Model.ViewModel;
using PagedList;
using PagedList.Mvc;


namespace ShopOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private ShopOnlineDbContext db = new ShopOnlineDbContext();

        public ActionResult Index(int? page, string sortOrder)
        {
            ViewBag.SortDesc = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SortAsc = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            ViewBag.SortOne = String.IsNullOrEmpty(sortOrder) ? "SortOne" : "";
            ViewBag.SortTwo = String.IsNullOrEmpty(sortOrder) ? "SortTwo" : "";
            ViewBag.SortThree = String.IsNullOrEmpty(sortOrder) ? "SortThree" : "";
            ViewBag.SortFour = String.IsNullOrEmpty(sortOrder) ? "SortFour" : "";
            ViewBag.SortFive = String.IsNullOrEmpty(sortOrder) ? "SortFive" : "";
            ViewBag.SortSix = String.IsNullOrEmpty(sortOrder) ? "SortSix" : "";
            if (page == null)
            {
                page = 1;
            }
            ViewBag.NewProduct = ListNewProduct(6);
            var model = from l in db.Product
                        select l;
            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(s => s.Price);
                    break;
                case "name_asc":
                    model = model.OrderBy(s => s.Price);
                    break;
                case "SortOne":
                    model = model.OrderBy(s => s.Price).Where(s => s.Price < 10000000);
                    break;
                case "SortTwo":
                    model = model.OrderBy(s => s.Price).Where(s => s.Price < 20000000 && s.Price > 10000000);
                    break;
                case "SortThree":
                    model = model.OrderBy(s => s.Price).Where(s => s.Price < 30000000 && s.Price > 20000000);
                    break;
                case "SortFour":
                    model = model.OrderBy(s => s.Price).Where(s => s.Price < 40000000 && s.Price > 30000000);
                    break;
                case "SortFive":
                    model = model.OrderBy(s => s.Price).Where(s => s.Price < 50000000 && s.Price > 40000000);
                    break;
                case "SortSix":
                    model = model.OrderBy(s => s.Price).Where(s => s.Price > 50000000);
                    break;
                default:
                    model = model.OrderBy(s => s.ID);
                    break;
            }
            int pagesize = 9;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pagesize));
        }


        public List<Product> ListNewProduct(int top)
        {
            return db.Product.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true).Take(top).ToList();
        }


        public ActionResult Menu()



        {
            var menu = new MenuDao().ListByGroupId(1);
            return PartialView(menu);
        }
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }
        public ActionResult Footer()
        {
            return PartialView();
        }
        public ActionResult Banner()
        {
            var bn = (from d in db.Banners select d).ToList();
            return PartialView(bn);
        }
        public ActionResult ItemMenuType()
        {
            var b = (from t in db.Category select t).ToList();
            return PartialView(b);
        }
        public ActionResult Brandtype()
        {
            var c = (from d in db.Brand select d).ToList();
            return PartialView(c);
        }
        public ActionResult ProductbyType(long id)
        {
            var pr = from d in db.Product where d.CategoryID == id && d.Status == true select d;
            return View(pr);
        }
        public ActionResult BrandbyType(long id)
        {
            var pr = from d in db.Product where d.BrandID == id && d.Status == true select d;
            return View(pr);
        }
        public ActionResult Details(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProducts(id);
            return View(product);
        }
        private List<Content> NewBlogs(int count)
        {
            return db.Content.OrderByDescending(a => a.CreatedDate).Take(count).ToList();
        }
        public ActionResult Blog()
        {

            return View(NewBlogs(5));
        }
        public ActionResult BlogDetail(long id)
        {

            var blog = from t in db.Content
                       where t.ID == id
                       select t;
            return View(blog.Single());
        }
        public ActionResult RecentBlog()
        {

            return PartialView(NewBlogs(4));
        }
        public static string getString(string s)
        {
            if (s.Length > 20)
            {
                return s.Substring(0, 20);
            }
            else
            {
                return s;
            }
        }
        public ActionResult BestSell()
        {
            var model = from p in db.Product
                        join o in db.OrderDetail on p.ID equals o.ProductID
                        select new { p, o };
            List<BestSellModel> listhl = new List<BestSellModel>();
            foreach (var info in model.ToList())
            {
                BestSellModel hl = new BestSellModel();
                hl.ID = info.p.ID;
                hl.NameProduct = info.p.Name;
                hl.Image = info.p.Image;
                hl.Price = info.p.Price;
                hl.Waranty = info.p.Warranty;
                listhl.Add(hl);
            }

            listhl.OrderByDescending(m => m.Waranty).GroupBy(m => m.NameProduct);
            return View(listhl);
        }
        public ActionResult Helmets()
        {
            long id = 1012;
            var i = from t in db.Product
                    join c in db.Category on t.CategoryID equals c.ID
                    //join d in data.Menus on c.MenuID equals d.ID
                    where c.MenuID == id && t.Status == true
                    select new { t, c };
            List<Helmets> listhl = new List<Helmets>();

            foreach (var info in i.ToList())
            {
                Helmets hl = new Helmets();
                hl.Name = info.t.Name;
                hl.Image = info.t.Image;
                hl.Quantity = info.t.Quantity;
                hl.Price = info.t.Price;
                hl.Status = info.t.Status;
                hl.Description = info.t.Description;
                listhl.Add(hl);
            }
            return View(listhl);
        }
        public ActionResult Brand()
        {
            long id = 1011;
            var i = from t in db.Product
                    join c in db.Brand on t.BrandID equals c.ID
                    //join d in data.Menus on c.MenuID equals d.ID
                    where c.MenuID == id && t.Status == true
                    select new { t, c };
            List<Helmets> listhl = new List<Helmets>();

            foreach (var info in i.ToList())
            {
                Helmets hl = new Helmets();
                hl.Name = info.t.Name;
                hl.Image = info.t.Image;
                hl.Quantity = info.t.Quantity;
                hl.Price = info.t.Price;
                hl.Status = info.t.Status;
                hl.Description = info.t.Description;
                listhl.Add(hl);
            }
            return View(listhl);
        }
        public ActionResult RiddingGear()
        {
            long id = 1009;
            var i = from t in db.Product
                    join c in db.Category on t.CategoryID equals c.ID

                    where t.Status == true
                    select new { t, c };
            List<Helmets> listhl = new List<Helmets>();

            foreach (var info in i.ToList())
            {
                Helmets hl = new Helmets();
                hl.Name = info.t.Name;
                hl.Image = info.t.Image;
                hl.Quantity = info.t.Quantity;
                hl.Price = info.t.Price;
                hl.Status = info.t.Status;
                hl.Description = info.t.Description;
                listhl.Add(hl);
            }
            return View(listhl);
        }
        public ActionResult Accsesories()
        {
            long id = 1008;
            var i = from t in db.Product
                    join c in db.Category on t.CategoryID equals c.ID

                    where t.Status == true
                    select new { t, c };
            List<Helmets> listhl = new List<Helmets>();

            foreach (var info in i.ToList())
            {
                Helmets hl = new Helmets();
                hl.Name = info.t.Name;
                hl.Image = info.t.Image;
                hl.Quantity = info.t.Quantity;
                hl.Price = info.t.Price;
                hl.Status = info.t.Status;
                hl.Description = info.t.Description;
                listhl.Add(hl);
            }
            return View(listhl);
        }
        public ActionResult ListOrderClient()
        {
            var ac = (User)Session[ShopOnline.Common.CommonConstants.USER_SESSION];
            var temp = db.Order.Where(p => p.User.UserName == ac.UserName && p.DaHuy == false);
            List<OrderEntity> listproduct = new List<OrderEntity>();
            foreach (var item in temp)
            {
                OrderEntity pr = new OrderEntity();
                pr.TypeOf_OrderEntity(item);
                listproduct.Add(pr);
            }
            return View(listproduct);
        }
        public ActionResult ListOrderDetailClient(long? id)
        {
            var temp = db.OrderDetail.Where(d => d.OrderID == id);
            List<OrderDetailEntity> listdetail = new List<OrderDetailEntity>();
            foreach (var item in temp)
            {
                OrderDetailEntity or = new OrderDetailEntity();
                or.TypeOf_OrderEntity(item);
                listdetail.Add(or);
            }


            return PartialView(listdetail);

        }

        public ActionResult CancelOrder(long? id)
        {
            var temp = db.OrderDetail.Where(d => d.OrderID == id);
            List<OrderDetailEntity> listdetail = new List<OrderDetailEntity>();
            foreach (var item in temp)
            {
                OrderDetailEntity or = new OrderDetailEntity();
                or.TypeOf_OrderEntity(item);
                listdetail.Add(or);
            }
            ViewBag.Date = db.Order.SingleOrDefault(a => a.ID == id).NgayGiao;
            ViewBag.id = id;
            return View(listdetail);

        }
        [HttpPost]

        public ActionResult CancelOrder(FormCollection fc)
        {

            long id = Convert.ToInt64(fc["id"]);
            var tem = db.Order.SingleOrDefault(d => d.ID == id);

            tem.DaHuy = true;

            db.SaveChanges();


            return RedirectToAction("ListOrderClient");

        }
        public ActionResult Profile()
        {
            var ac = (User)Session[ShopOnline.Common.CommonConstants.USER_SESSION];
            var t = from a in db.User where a.UserName == ac.UserName select a;
            return View(t.ToList());
        }
    }
}