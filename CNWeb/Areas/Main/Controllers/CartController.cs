using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CNWeb.Areas.Main.Controllers.MenuController;

namespace CNWeb.Areas.Main.Controllers
{
    public class CartController : Controller
    {
        DbCNWeb db = new DbCNWeb();
        // GET: Main/Cart
        public ActionResult Index()
        {
            
            var session =(CNWeb.Code.UserSession)Session[CNWeb.Code.Constants.USER_SESSION];
            if (session == null)
            {
                RedirectToAction("Login", "User", new { area = "" });
            }
            int cartid = session.CartID;
            SqlParameter parameter1 = new SqlParameter("@cartid", cartid);
            var data = db.Database.SqlQuery<CartMini>("select b.ID, a.Name,a.Alias, b.Size, a.OriginPrice, a.PromotionPrice, c.Quantity from foods a, FoodOptions b, CartFoodDetails c where b.FoodID=a.id and c.FoodOptionID = b.ID and c.CartID = @cartid", parameter1).ToList();
            return View(data);
        }
        public JsonResult DeleteFromCart(int id)
        {
            try
            {
                var session = (CNWeb.Code.UserSession)Session[CNWeb.Code.Constants.USER_SESSION];
                int cartid = session.CartID;
                SqlParameter parameter1 = new SqlParameter("@cartid", cartid);
                SqlParameter parameter2 = new SqlParameter("@id", id);
                db.Database.ExecuteSqlCommand("Delete from CartFoodDetails where cartid = @cartid and Foodoptionid =@id", parameter1, parameter2);
                return Json(new { message= "OK", data="Thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { message = "FAIL", data = "KHÔNG Thành công" }, JsonRequestBehavior.AllowGet);
            }

        }
    }   
}