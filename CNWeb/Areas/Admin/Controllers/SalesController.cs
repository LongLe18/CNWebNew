using CNWeb.Helper;
using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNWeb.Areas.Admin.Controllers
{
    public class SalesController : Controller
    {
        // [GET]: Admin/Sales/Sales
        public ActionResult Sales()
        {
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
    }
}