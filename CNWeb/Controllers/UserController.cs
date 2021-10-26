using CNWeb.Code;
using CNWeb.Models;
using Models.Dao;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.EF;
using System.Data.SqlClient;

namespace CNWeb.Controllers
{
    public class UserController : Controller
    {
        // [GET] /User/Login
        public ActionResult Login()
        {
            return View();
        }

        // [GET] /User/Main
        public ActionResult Main()
        {
            return RedirectToAction("Index", "Main", new { Area = "Main" });
            
        }

        // [POST] /User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            var dao = new AccountModel();
            var result = dao.Login(model.UserName, model.Password);

            if (ModelState.IsValid)
            {
                var user = dao.GetById(model.UserName);
                var userSession = new UserSession();
                userSession.UserName = user.UserName;
                userSession.UserID = user.ID;
                userSession.FullName = user.FullName;
                userSession.Country = user.Country;
                userSession.Wallet = (int)user.Wallet;
                var db = new DbCNWeb();
                SqlParameter parameter1 = new SqlParameter("@id", user.ID);
                
                userSession.CartID = db.Database.SqlQuery<int>("select carts.id from carts, users where users.ID = carts.createdbyuserid and users.id = @id", parameter1).FirstOrDefault();
                userSession.City = user.City;
                userSession.Career = user.Career;
                userSession.pathImage = user.Image; // path image
                userSession.Level = user.Level;

                DateTime birthday = Helper.ConvertDateTime.ConvertUnixToDateTime(Convert.ToInt32(user.BirthDay));
                userSession.BirthDay = DateTime.Today.Year - birthday.Year;

                var listCredentials = dao.GetListCredential(model.UserName);
                Session.Add(Constants.SESSION_CREDENTIALS, listCredentials);
                Session.Add(Constants.USER_SESSION, userSession);
                if (result == 2) // neu thanh cong phai tao session
                {
                    return RedirectToAction("Home", "Admin/Admin");
                }
                else if (result == 1)
                {
                    return RedirectToAction("Main", "User");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }               
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không chính xác");
                }
                return View(model);
            }
            return View();
        }

        // [GET] /User/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [POST] /User/Register
        public ActionResult Register(User Account)  // Đăng ký cho khách hàng
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add login here
                    Account.Level = Constant.LEVEL_USER;
                    Account.IDChucVu = Constant.ID_USER;
                    var dt = DateTime.UtcNow;
                    int createdDayUnix = CNWeb.Helper.ConvertDateTime.ConvertDateTimeToUnix(dt);
                    Account.CreatedDay = createdDayUnix;
                    var model = new AccountModel();

                    int res = model.Register(Account);  // add to database
                    if (res == 1)
                    {
                        ViewBag.Message = "Đăng ký tài khoản thành công";
                    } else if (res == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản đã tồn tại");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký tài khoản không thành công");
                    }

                }
                return View(Account);
            }
            catch
            {
                return View();
            }
        }
    }
}