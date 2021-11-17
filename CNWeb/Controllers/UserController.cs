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
using System.Configuration;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Verify.V2.Service;

namespace CNWeb.Controllers
{
    public class UserController : Controller
    {
        // [GET] /User/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            Session.Clear();
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
        public ActionResult Login(LoginModel model, string returnUrl)
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
                userSession.PhoneNumber = user.PhoneNumber;
                if (user.RequiresVerification != null) userSession.RequiresVerification = user.RequiresVerification;
                if (user.Wallet != null) userSession.Wallet = (int)user.Wallet;
                
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

                if (user.RequiresVerification == true)
                {
                    return RedirectToAction("VerifyCode", "User", new { returnUrl = returnUrl, phoneNumber = user.PhoneNumber });
                }
                else { 
                    if (result == 2) // neu thanh cong phai tao session
                    {
                        if (!string.IsNullOrEmpty(returnUrl)) { return Redirect(returnUrl); }
                        else { return RedirectToAction("Home", "Admin/Admin"); }
                    }
                    else if (result == 1)
                    {
                        if (!string.IsNullOrEmpty(returnUrl)) { return Redirect(returnUrl); }
                        else { return RedirectToAction("Main", "User"); }
                    }
                    else if (result == 0)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                    }               
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu không chính xác");
                    }
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
                    Account.RequiresVerification = false;
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

        // [POST] /User/SendSms
        public ActionResult SendSms(string phoneNumber)
        {
            var accountSid = ConfigurationManager.AppSettings["SMSAccountIdentification"];
            var authToken = ConfigurationManager.AppSettings["SMSAccountPassword"];
            TwilioClient.Init(accountSid, authToken);

            var to = "+84" + phoneNumber;
 
            var result = VerificationResource.Create(
                to: to,
                channel: "sms",
                pathServiceSid: "VA28731bdedc04a483e4cdf30e3e76907b"
            );

            return Json(result.Sid, JsonRequestBehavior.AllowGet);
        }

        // [GET] /User/VerifyCode
        [HttpGet]
        public ActionResult VerifyCode(string returnUrl, string phoneNumber)
        {
            if (Session.Count == 0 )
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            return View(new VerifyCodeViewModel { ReturnUrl = returnUrl, PhoneNumber = phoneNumber });
        }

        // [POST] /User/VerifyCode
        [HttpPost]
        public ActionResult VerifyCode(VerifyCodeViewModel model)
        {
            try
            {
                var accountSid = ConfigurationManager.AppSettings["SMSAccountIdentification"];
                var authToken = ConfigurationManager.AppSettings["SMSAccountPassword"];
                TwilioClient.Init(accountSid, authToken);

                var to = "+84" + model.PhoneNumber;

                var result = VerificationCheckResource.Create(
                    to: to,
                    code: model.Code,
                    pathServiceSid: "VA28731bdedc04a483e4cdf30e3e76907b"
                );

                if (result.Status == "approved")
                {
                    return Redirect(model.ReturnUrl);
                }
                return RedirectToAction("VerifyCode", "User", new { returnUrl = model.ReturnUrl, phoneNumber = model.PhoneNumber }); // status = approved => true ; pending => false
            }
            catch
            {
                return RedirectToAction("VerifyCode", "User", new { returnUrl = model.ReturnUrl, phoneNumber = model.PhoneNumber });
            }
        }

        // [POST] /User/ChangeTwoFactor
        public JsonResult TwoFactor(string id, bool status)
        {
            var dao = new AccountModel();
            var result = dao.ChangeStatusTwoFactor(Int32.Parse(id), status);

            if (result)
            {
                return Json("Success", JsonRequestBehavior.AllowGet); 
            } else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }
    }
}