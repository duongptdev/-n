using Model.EF;
using ShopOnline.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ShopOnline.Areas.Admin.Controllers
{
    public class RevenueController : Controller
    {
        // GET: Admin/Revenue
        ShopOnlineDbContext db = new ShopOnlineDbContext();

        public ActionResult byProduct()
        {
            var model = db.OrderDetail
                .GroupBy(d => d.Product.Name)
                .Select(g => new ReportInfo
                {
                    Group = g.Key,                  
                    Count=g.Min(d => d.Product.Quantity)
                }).OrderBy(x =>x.Group);
            return View(model);
        }
        public ActionResult byYear()
        {
            var model = db.OrderDetail
                .GroupBy(d => d.Order.CreatedDate.Value.Year)
                .Select(g => new ReportInfo
                {
                    iGroup = g.Key,
                    Sum = g.Sum(d => (d.Price - d.Product.EntryPrice) * d.Quantity),
                    Count = g.Sum(d => d.Quantity),
                    Min = g.Min(d => d.Price),
                    Max = g.Max(d => d.Price),
                    Avg = g.Average(d => d.Price)
                   
                })
                .OrderBy(i => i.iGroup);
            return View("Index", model);
        }
        public ActionResult byDay()
        {
            var model = db.OrderDetail
                .GroupBy(d => d.Order.CreatedDate.Value.Day)
                .Select(g => new ReportInfo
                {                           
                    iGroup = g.Key,
                    Sum = g.Sum(d => (d.Price - d.Product.EntryPrice) * d.Quantity),
                    Count = g.Sum(d => d.Quantity),
                    Min = g.Min(d => d.Price),
                    Max = g.Max(d => d.Price),
                    Avg = g.Average(d => d.Price),
                })
                .OrderBy(i => i.iGroup);
            return View("Index", model);
        }

        public ActionResult byMonth()
        {
            var model = db.OrderDetail

                .GroupBy(d => d.Order.CreatedDate.Value.Month)
                .Select(g => new ReportInfo
                {
                    iGroup = g.Key,
                    Sum = g.Sum(d => (d.Price - d.Product.EntryPrice) * d.Quantity),
                    Count = g.Sum(d => d.Quantity),
                    Min = g.Min(d => d.Price),
                    Max = g.Max(d => d.Price),
                    Avg = g.Average(d => d.Price)
                })
                .OrderBy(i => i.iGroup);
            return View("Index", model);
        }


    }
}