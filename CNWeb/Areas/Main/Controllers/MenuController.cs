using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNWeb.Areas.Main.Controllers
{
    public class MenuController : Controller
    {
        DbCNWeb db = new DbCNWeb();
        // GET: Main/Menu
        public ActionResult Index()
        {
            List<FoodCategory> fc = new List<FoodCategory>();
            
            fc = db.FoodCategories.OrderBy(s=>s.DisplayOrder).ToList();
            ViewBag.fc = fc;
            ViewBag.foods = db.Foods.ToList();
            return View();
        }
        public ActionResult Detail(int id)
        {
            Food f = db.Foods.Where(s => s.ID == id).FirstOrDefault();
            ViewBag.food = f;
            SqlParameter parameter = new SqlParameter("@id", id);

            ViewBag.image = db.Database.SqlQuery<Image>("select Image.ID, Image.Name, Image.Alias, Image.URL, Image.Description " +
                "from Image, Foods, SlideDetails  where Foods.ID = @id and Foods.SlideID = SlideDetails.SlideID and SlideDetails.ImageID = Image.ID", parameter).ToList();
            SqlParameter parameter1 = new SqlParameter("@id", id);
            ViewBag.options = db.Database.SqlQuery<string>("select FoodOptions.Size from FoodOptions, Foods  where Foods.ID = @id and Foods.ID = FoodOptions.FoodID ", parameter1).ToList();
            return View();
        }

        public JsonResult ChangeSize(int size, int idfood)
        {
            try
            {
                string strsize = "";
                switch(size)
                {
                    case 1:
                        strsize = "Suất nhỏ";
                        break;
                    case 2:
                        strsize = "Suất vừa";
                        break;
                    case 3:
                        strsize = "Suất lớn";
                        break;
                    case 4:
                        strsize = "Suất đặc biệt";
                        break;
                }
                SqlParameter parameter = new SqlParameter("@idfood", idfood);
                SqlParameter parameter2 = new SqlParameter("@size", strsize);
                var data = db.Database.SqlQuery<int>("select BoundingPrice from FoodOptions where FoodID = @idfood and Size = @size", parameter, parameter2).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetIDFoodOption(int size, int idfood)
        {
            try
            {
                string strsize = "";
                switch (size)
                {
                    case 1:
                        strsize = "Suất nhỏ";
                        break;
                    case 2:
                        strsize = "Suất vừa";
                        break;
                    case 3:
                        strsize = "Suất lớn";
                        break;
                    case 4:
                        strsize = "Suất đặc biệt";
                        break;
                }
                SqlParameter parameter = new SqlParameter("@idfood", idfood);
                SqlParameter parameter2 = new SqlParameter("@size", strsize);
                var data = db.Database.SqlQuery<int>("select ID from FoodOptions where FoodID = @idfood and Size = @size", parameter, parameter2).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddCart(int cartid, int foodoptid, int quantity)
        {
            try
            {
                db.Database.ExecuteSqlCommand("insert into cartfooddetails values (@i, @j, @k)", new SqlParameter("@i", cartid), new SqlParameter("@j", foodoptid),
                    new SqlParameter("@k", quantity));
                db.SaveChanges();
                return Json("Thêm vào giở hàng thành công", JsonRequestBehavior.AllowGet);
            } catch
            {
                return Json("Thêm vào giở hàng không thành công", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Search(string txtSearch)
        {
            List<FoodCategory> fc = new List<FoodCategory>();

            fc = db.FoodCategories.OrderBy(s => s.DisplayOrder).ToList();
            ViewBag.fc = fc;
            ViewBag.foods = db.Foods.Where(s=>s.Name.Contains(txtSearch)).ToList();
            return View("Index");
        }
        public class CartMini
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Alias { get; set; }
            public int Quantity { get; set; }
            public string Size { get; set; }
            public int OriginPrice { get; set; }
            public int? PromotionPrice { get; set; }
        }
        public JsonResult GetListCarts(int cartid)
        {
            try
            {
                SqlParameter parameter1 = new SqlParameter("@cartid", cartid);
                var data = db.Database.SqlQuery<CartMini>("select b.ID, a.Name,a.Alias, b.Size, a.OriginPrice, a.PromotionPrice, c.Quantity from foods a, FoodOptions b, CartFoodDetails c where b.FoodID=a.id and c.FoodOptionID = b.ID and c.CartID = @cartid", parameter1).Take(5).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch
            {
                return Json("Lỗi", JsonRequestBehavior.AllowGet);
            }
        }

    }
}