
using Common;
using Model.Dao;
using Model.EF;
using Model.ViewModel;
using ShopOnline.Common;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace ShopOnline.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private ShopOnlineDbContext db = new ShopOnlineDbContext();

        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Message = "A fresh CAPTCHA image is being displayed!";
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }
        public CaptchaImageResultController ShowCaptchaImage()
        {
            return new CaptchaImageResultController();
        }
        public ActionResult Logout()
        {
            Session[ShopOnline.Common.CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(LoginModels model, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var userName = collection["userName"];
                var passWord = collection["passWord"];
                User us = db.User.SingleOrDefault(n => n.UserName == userName && n.Password == passWord);
                if (us != null)
                {
                    Session[ShopOnline.Common.CommonConstants.USER_SESSION] = us;
                    return RedirectToAction("/");
                }
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));

                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new User();
                    userSession.UserName = user.UserName;
                    userSession.ID = user.ID;
                    Session.Add(ShopOnline.Common.CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng.");
                }
            }
            return View(model);
        }
        [HttpPost]

        public ActionResult Register(RegisterModel model)
        {                       
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    if (!string.IsNullOrEmpty(model.ProvinceID))
                    {
                        user.ProvinceID = int.Parse(model.ProvinceID);
                    }
                    if (!string.IsNullOrEmpty(model.DistrictID))
                    {
                        user.DistrictID = int.Parse(model.DistrictID);
                    }

                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View(model);
        }

        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_Data.xml"));

            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel province = null;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
        public JsonResult LoadDistrict(int provinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_Data.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceID);

            var list = new List<DistrictModel>();
            DistrictModel district = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                district = new DistrictModel();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = int.Parse(xElement.Attribute("id").Value);
                list.Add(district);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }

        public ActionResult Changepassword()
        {
            if (Session[ShopOnline.Common.CommonConstants.USER_SESSION] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var ac = (User)Session[ShopOnline.Common.CommonConstants.USER_SESSION];
            return View(new UserEntity(ac));
        }
        [HttpPost]

        public ActionResult Changepassword(FormCollection fc)
        {
            var ac = (User)Session[ShopOnline.Common.CommonConstants.USER_SESSION];
            if (Session[ShopOnline.Common.CommonConstants.USER_SESSION] != null)
            {
                string userName = fc["userName"].ToString();
                string pass = fc["pass"].ToString();
                string newpass = fc["newpass"].ToString();
                string repass = fc["repass"].ToString();
                var temp = db.User.SingleOrDefault(x => x.UserName == userName && x.Password == pass);
                if (temp != null && pass != "" && newpass != pass && newpass != "" && newpass == repass)
                {
                    temp.Password = fc["newpass"];
                    db.SaveChanges();
                    Session[ShopOnline.Common.CommonConstants.USER_SESSION] = temp;
                    return RedirectToAction("Profile", "Home");

                }



            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Error cannot change password..");
            return View(new UserEntity(ac));
        }
    }
}