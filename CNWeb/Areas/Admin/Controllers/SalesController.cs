using CNWeb.Helper;
using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNWeb.Areas.Admin.Controllers
{
    public class SalesController : Controller
    {
        DbCNWeb db = new DbCNWeb();
        // [GET]: Admin/Sales/Sales
        //[HasCredential(RoleID = "VIEW_ADMIN")]
        public ActionResult Sales()
        {

            ViewBag.AllFood = db.Foods.ToList();
            return PartialView("Sales");
        }

        // [GET] Admin/Sales/GetPaggedData
        public ActionResult GetPaggedData(int filter, string search, int pageNumber = 1, int pageSize = 5)
        {
            var dao = new PromotionModel();
            var model = dao.ListPromotion(filter, search).ToList();
            var pagedData = Pagination.PagedResult(model, pageNumber, pageSize);
            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }

        // [GET] Admin/Sales/GetDetailByID
        public ActionResult GetDetailPromotionByID(int id)
        {
            var dao = new PromotionModel();
            var detail = dao.GetDetailPromotionByID(id);
            return Json(detail, JsonRequestBehavior.AllowGet);
        }
        //Admin/Sales/Add
        [HttpPost]
        public ActionResult Add()
        {
            try
            {
                Promotion km = new Promotion();
                km.Name = Request.Form["namepro"];
                km.Content = Request.Form["des"];
                km.TypeID = 1;
                km.ActiveDay = Helper.ConvertDateTime.ConvertDateTimeToUnix(DateTime.ParseExact(Request.Form["datetime1"], "dd/MM/yyyy", null));
                km.EndDay = Helper.ConvertDateTime.ConvertDateTimeToUnix(DateTime.ParseExact(Request.Form["datetime2"], "dd/MM/yyyy", null));
                km.ID = db.Promotions.Max(i => i.ID) + 1;
                List<int> k = new List<int>();
                km.Status = 0;
                db.Promotions.Add(km);
                for (int i = 0; i < db.Foods.ToList().Count; i++)
                {
                    if (Request.Form[$"Checkbox[{i}]"] == "on")
                    {
                        k.Add(i);
                    }
                }
                foreach(int j in k)
                {
                    PromotionFoodDetail z = new PromotionFoodDetail();
                    z.FoodID = db.Foods.ToList()[j].ID;
                    z.PromotionID = km.ID;
                    z.PercentReduction = int.Parse(Request.Form["reduct"]);
                    db.PromotionFoodDetails.Add(z);
                }
                db.SaveChanges();
                return Json(new { message = "Success!!", data = "Thêm thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { message = "Fail!!", data = "Lỗi khi thêm " }, JsonRequestBehavior.AllowGet);
            }

        }
        // [PUT] /Admin/Sales/ActivePromotion
        public JsonResult ActivePromotion(int id, int status)
        {
            try
            {
                var dao = new PromotionModel();
                var res = dao.ChangeStatusPromotion(id, status);
                if (res)
                {
                    return Json(new { message = "Success!!", data = "Kích hoạt thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Fail!!", data = "Kích hoạt thất bại" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                return Json(new { message = "Fail!!", data = "Kích hoạt thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Delete(int id)
        {
            try
            {
                DbCNWeb db = new DbCNWeb();
                var k = db.Promotions.Find(id);

                db.Promotions.Remove(k);
                db.SaveChanges();
                return Json(new { message = "Success!!", data = "Xóa thành công" }, JsonRequestBehavior.AllowGet);



            }
            catch
            {
                return Json(new { message = "Fail!!", data = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}