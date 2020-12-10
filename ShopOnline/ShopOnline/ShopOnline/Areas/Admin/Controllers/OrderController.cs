using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
      private  ShopOnlineDbContext db = new ShopOnlineDbContext();
        // GET: Admin/Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChuaThanhToan()
        {

            // lấy danh sách đơn hàng chưa duyệt
            var lstDonHang = db.Order.Where(n => n.DaThanhToan == false&& n.DaHuy==false).OrderBy(n => n.CreatedDate);
            return View(lstDonHang);
        }
        public ActionResult ChuaGiao()
        {
            // lấy danh sách đơn hàng chưa giao
            var lstChuaGiao = db.Order.Where(n => n.TinhTrangGiaoHang == false&& n.DaHuy==false).OrderBy(n => n.CreatedDate);
            return View(lstChuaGiao);
        }
        public ActionResult DaHuy()
        {
            var lstDaHuy = db.Order.Where(n => n.DaHuy == true).OrderBy(n => n.CreatedDate);
            return View(lstDaHuy);
        }
        public ActionResult DaGiaoDaThanhToan()
        {
            var lstDaGiaoDaThanhToan = db.Order.Where(n => n.TinhTrangGiaoHang == true && n.DaThanhToan == true&& n.DaHuy==false);
            return View(lstDaGiaoDaThanhToan);
        }
        [HttpGet]
        public ActionResult DuyetDonHang(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order model = db.Order.SingleOrDefault(n => n.ID == Id);
            if (model == null)
            {
                return HttpNotFound();
            }
            //lấy danh sách chi tiết
            var lstChiTietDH = db.OrderDetail.Where(n => n.OrderID == Id);
            ViewBag.ListChiTietDH = lstChiTietDH;

            return View(model);
        }
        [HttpPost]
        public ActionResult DuyetDonHang(Order ddh)
        {
            //truy vấn cơ sở dử liệu
            Order ddhUpdate = db.Order.SingleOrDefault(n => n.ID == ddh.ID);
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            ddhUpdate.TinhTrangGiaoHang = ddh.TinhTrangGiaoHang;
            ddhUpdate.NgayGiao = ddh.NgayGiao;
            db.SaveChanges();
            //lấy danh sách chi tiết đơn đăt hàng để hiển thị cho ng dùng
            var lstChiTietDH = db.OrderDetail.Where(n => n.IdCTDDH == ddh.ID);
            ViewBag.ListChiTietDH = lstChiTietDH;
            return View(ddhUpdate);
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