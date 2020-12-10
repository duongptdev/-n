using Common;
using Model.Dao;
using Model.EF;
using Model.ViewModel;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private ShopOnlineDbContext db = new ShopOnlineDbContext();
        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);
        }
        public ActionResult ThemVaoGio(int SanPhamID)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.ProductID == SanPhamID) == null) // ko co sp nay trong gio hang
            {
                Product sp = db.Product.Find(SanPhamID);  // tim sp theo sanPhamID

                CartItem newItem = new CartItem()
                {
                    ProductID = SanPhamID,
                    Name = sp.Name,
                    Quantity = 1,
                    Image = sp.Image,
                    Price =(int)sp.Price

                };  // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.ProductID == SanPhamID);
                cardItem.Quantity++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return RedirectToAction("Index", "Home", new { id = SanPhamID });
            //return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult SuaSoLuong(int SanPhamID, int soluongmoi)
        {
            // tìm carditem muon sua
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemSua = giohang.FirstOrDefault(m => m.ProductID == SanPhamID);
            if (itemSua != null)
            {
                itemSua.Quantity = soluongmoi;
            }
            return RedirectToAction("Index");

        }
        public ActionResult XoaKhoiGio(int SanPhamID)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.ProductID == SanPhamID);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }
        private Double ToTalPrice()
        {
            double ToTal = 0;
            List<CartItem> lstCart = Session["giohang"] as List<CartItem>;
            if (lstCart != null)
            {
                ToTal = lstCart.Sum(n => n.Price);
            }
            return ToTal;
        }
        public List<CartItem> GetCart()
        {
            List<CartItem> lstCart = Session["giohang"] as List<CartItem>;
            if (lstCart == null)
            {

                lstCart = new List<CartItem>();
                Session["giohang"] = lstCart;
            }
            return lstCart;
        }
        [HttpGet]
        public ActionResult PayMent()
        {
            if (Session[ShopOnline.Common.CommonConstants.USER_SESSION] == null || Session[ShopOnline.Common.CommonConstants.USER_SESSION].ToString() == "")
            {
                return Redirect("/dang-nhap");
            }
            if (Session["giohang"] == null)
            {
                return Redirect("/gio-hang");
            }
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
         
            return View(giohang);
        }
        [HttpPost]
        public ActionResult PayMent(string shipName, string mobile, string address, string email,string nameproduct)
        {
            var order = new Order();
            User us = (User)Session[ShopOnline.Common.CommonConstants.USER_SESSION];
            
            List <CartItem> cart = GetCart();
            order.UserID = us.ID;
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMobile = mobile;
            order.ShipName = shipName;
            order.ShipEmail = email;
            order.Price = (decimal)ToTalPrice();
            order.TinhTrangGiaoHang = false;
            order.DaThanhToan = false;
            order.DaHuy = false;
            order.DaXoa = false;


            var id = new OrderDao().Insert(order);
            try
            {            
                var detailDao = new Model.Dao.OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.ProductID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Price;
                    orderDetail.Quantity = item.Quantity;
                    order.NameProduct = item.Name;
                    db.OrderDetail.Add(orderDetail);
                    var it = db.Product.Find(item.ProductID);
                    it.Warranty= (it.Warranty) + item.Quantity;
                    it.Quantity = (it.Quantity) - item.Quantity;
                    db.SaveChanges();
                    nameproduct = item.Name;
                    //detailDao.Insert(orderDetail);
                    total +=(item.Price * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/neworder.html"));
                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{NameProduct}}", nameproduct);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(email, "Đơn hàng mới từ ShopOnline", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ ShopOnline", content);
            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }
   
        public ActionResult Success()
        {
            return View();
        }
    }
}