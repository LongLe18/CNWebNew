using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNWeb.Models;
using Models.Dao;
using Models.EF;

namespace CNWeb.Areas.Main.Controllers
{
    public class ProfileController : Controller
    {
        DbCNWeb db = new DbCNWeb();
        // GET: Main/Profile
        public ActionResult Index()
        {
            var session = (CNWeb.Code.UserSession)Session[CNWeb.Code.Constants.USER_SESSION];
            if (session == null) return RedirectToAction("Login", "User", new { area = "" });
            var model = db.Orders.Where(i => i.IDUser == session.UserID).ToList();
            return View(model);
        }
        public JsonResult SearchOrder(string id)
        {
            try
            {

                var session = (CNWeb.Code.UserSession)Session[CNWeb.Code.Constants.USER_SESSION];
                var model = new OrderModel();
                var model1 = model.SearchOrder_(id, session.UserID);

                return Json(new { message = "OK", data = model1 }, JsonRequestBehavior.AllowGet);
                //if (res)
                //{
                //    return Json(new { message = "Success!!", data = "Lấy dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { message = "Fail!!", data = "Lấy dữ liệu thất bại" }, JsonRequestBehavior.AllowGet);
                //}

            }
            catch
            {
                return Json(new { message = "Fail!!", data = "Không lấy được dữ liệu" }, JsonRequestBehavior.AllowGet);
            }
        }
        public class OrderDetail
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public string Size { get; set; }
            public string Alias { get; set; }
        }
        public JsonResult OrderDetail1(string id)
        {

            try
            {
                var db = new DbCNWeb();
                SqlParameter parameter = new SqlParameter("@id", int.Parse(id));
                List<OrderDetail> n = db.Database.SqlQuery<OrderDetail>("select a.Alias, a.Name, b.Size, c.Quantity from Foods as a, FoodOptions as b, OrderFoodDetails as c where b.FoodID = a.ID and c.FoodOptionID = b.ID and c.OrderID = @id", parameter).ToList();
                return Json(new { message = "OK", data = n }, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                return Json(new { message = "Fail!!", data = "Không lấy được dữ liệu" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}