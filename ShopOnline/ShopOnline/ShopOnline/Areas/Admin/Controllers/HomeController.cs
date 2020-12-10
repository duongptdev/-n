using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        ShopOnlineDbContext db = new ShopOnlineDbContext();
        public ActionResult Index()
        {
            DateTime dateTimeNow = DateTime.Now.Date;
            dateTimeNow = dateTimeNow.AddYears(-1);

            string[] dateX = new string[12];
            string[] data = new string[12];
            for (int i = 0; i < 12; i++)
            {

                dateX[i] = dateTimeNow.Month.ToString();
                var temp = db.Order.Where(a => a.CreatedDate.Value.Month == dateTimeNow.Month).Sum(s => s.Price);
                if (temp == null)
                {
                    temp = 0;
                }
                data[i] = temp.ToString();
                dateTimeNow = dateTimeNow.AddMonths(1);
            }
            ViewBag.dateX = dateX;
            ViewBag.data = data;

            return View();
        }
   



        public decimal ThongKeDoanhThuTheoThang(int Thang, int Nam, int Day)
        {
            //thống kê tất cả doanh thu
            // list ra don dat hang có ngày tháng năm tương ứng
            var lstDDH = db.Order.Where(n => n.CreatedDate.Value.Month == Thang && n.CreatedDate.Value.Year == Nam && n.CreatedDate.Value.Day==Day && n.DaThanhToan==true);
            decimal TongTien = 0;
            foreach (var item in lstDDH)
            {
                TongTien += decimal.Parse(item.OrderDetails.Sum(n => n.Quantity * n.Price).Value.ToString());
            }
            return TongTien;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}