using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Models.Dao;
using static CNWeb.Areas.Main.Controllers.MenuController;

namespace CNWeb.Areas.Main.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Main/Payment
        DbCNWeb db = new DbCNWeb();
        CNWeb.Code.UserSession session;
        public ActionResult Index()
        {
            session = (CNWeb.Code.UserSession)Session[CNWeb.Code.Constants.USER_SESSION];
            if (session == null)
            {
                RedirectToAction("Login", "User", new { area = "" });
            }
            int cartid = session.CartID;
            SqlParameter parameter1 = new SqlParameter("@cartid", cartid);
            var data = db.Database.SqlQuery<CartMini>("select b.ID, a.Name,a.Alias, b.Size, a.OriginPrice, a.PromotionPrice, c.Quantity from foods a, FoodOptions b, CartFoodDetails c where b.FoodID=a.id and c.FoodOptionID = b.ID and c.CartID = @cartid", parameter1).ToList();
            SqlParameter parameter2 = new SqlParameter("@cartid", cartid);
            var data1 = db.Database.SqlQuery<payment>("select a.Wallet, c.Quantity, e.OriginPrice, e.PromotionPrice from Users a, Carts b, CartFoodDetails c, FoodOptions d, Foods e where a.ID = b.CreatedByUserID and b.ID = c.CartID and c.FoodOptionID = d.ID and e.ID = d.FoodID and b.ID = @cartid", parameter2).ToList();
            int wallet = 0;
            int VAT = 0;
            int? discount = 0;
            int? finalprice = 0;
            int shipping = 15000;
            int? balanace = 0;
            int totaloriginprice = 0;
            foreach (var item in data1)
            {
                wallet = item.Wallet;
                VAT = VAT + (item.OriginPrice * item.Quantity) / 20;
                if (item.PromotionPrice != null)
                {
                    discount = discount + (item.OriginPrice * item.Quantity - item.PromotionPrice * item.Quantity);
                }
                totaloriginprice = totaloriginprice + (item.OriginPrice * item.Quantity);
            }
            finalprice = totaloriginprice - discount + shipping + VAT;
            balanace = wallet - finalprice;
            List<payment1> list = new List<payment1>();
            list.Add(new payment1()
            {
                Wallet = wallet,
                Discount = discount,
                FinalPrice = finalprice,
                Shipping = shipping,
                VAT = VAT,
                Balance = balanace,
                OriginPrice = totaloriginprice
            });
            ViewBag.payment = list;
            SqlParameter parameter3 = new SqlParameter("@cartid", cartid);
            var data2 = db.Database.SqlQuery<InfoUser1>("select a.FullName, a.Address, a.Email, a.PhoneNumber, a.Wallet from Users a, Carts b where a.ID = b.CreatedByUserID and b.ID = @cartid", parameter3).ToList();
            List<InfoUser> list1 = new List<InfoUser>();
            foreach (var item2 in data2)
            {
                list1.Add(new InfoUser()
                {
                    FullName = item2.FullName,
                    Address = item2.Address,
                    Email = item2.Email,
                    PhoneNumber = item2.PhoneNumber,
                    Wallet = item2.Wallet
                }) ;
            }
            ViewBag.info = list1;
            return View(data);
        }
        public class InfoUser1
        {
            public string FullName { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public int Wallet { get; set; }
        }
        public class payment
        {
            public int Wallet { get; set; }
            public int Quantity { get; set; }
            public int OriginPrice { get; set; }
            public int? PromotionPrice { get; set; }
        }

        public ActionResult InforUser()
        {
            var session = (CNWeb.Code.UserSession)Session[CNWeb.Code.Constants.USER_SESSION];
            if (session == null)
            {
                RedirectToAction("Login", "User", new { area = "" });
            }
            int cartid = session.CartID;
            SqlParameter parameter1 = new SqlParameter("@cartid", cartid);
            ViewBag.info = db.Database.SqlQuery<InfoUser>("select a.FullName, a.Address, a.Email, a.PhoneNumber, a.Wallet from Users a, Carts b where a.ID = b.CreatedByUserID and b.ID = @cartid", parameter1).ToList();
            SqlParameter parameter3 = new SqlParameter("@cartid", cartid);
            return View();
        }
        public ActionResult CheckPayment(string sub, string name, string phone, string add, string note, string email)
        {
            try
            {
                session = (CNWeb.Code.UserSession)Session[CNWeb.Code.Constants.USER_SESSION];
                var k = int.Parse(sub);
            var k1 = db.Users.Find(session.UserID);
                Order order = new Order();
                order.CustomerName = name;
                order.CustomerAddress = add;
                order.CustomerMessage = note;
                order.ToTalPrice = k;
                order.IDUser = session.UserID;
                order.CreatedByUserID = session.UserID;
                order.Status = 1;
                var dt = DateTime.UtcNow;
                int createdDayUnix = CNWeb.Helper.ConvertDateTime.ConvertDateTimeToUnix(dt);order.CreatedTime = createdDayUnix;
                db.Orders.Add(order);
                

            k1.Wallet -= k;
            db.SaveChanges();
                var id = order.ID;
                var carts =db.CartFoodDetails.Where(i => i.CartID == session.CartID).ToList();
                foreach(var item in carts)
                {
                    
                    OrderFoodDetail or = new OrderFoodDetail();
                    or.OrderID = id;
                    or.FoodOptionID = item.FoodOptionID;
                    or.Quantity = item.Quantity;
                    db.OrderFoodDetails.Add(or);
                    db.CartFoodDetails.Remove(item);
                    
                }
                db.SaveChanges();
            return Json(new { message = "OK", data = " Thanh toán Thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { message = "Lỗi", data = "Lỗi trong quá trình thanh toán" }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}