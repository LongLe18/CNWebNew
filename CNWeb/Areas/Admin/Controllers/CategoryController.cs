using Models.Common;
using CNWeb.Helper;
using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNWeb.Code;

namespace CNWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // {GET] Admin/Category/Category
        [HasCredential(RoleID = "VIEW_ADMIN")]
        public ActionResult Category()
        {
            return PartialView("Category");
        }

        // [GET] Admin/Category/GetPaggedData
        public ActionResult GetPaggedData(int pageNumber = 1, int pageSize = 5)
        {
            var dao = new FoodModel();
            var model = dao.ListFoodCategory().ToList();
            var pagedData = Pagination.PagedResult(model, pageNumber, pageSize);
            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }

        // [GET] Admin/Category/CategoryInfo
        public ActionResult CategoryInfor(int id)
        {
            var dao = new FoodModel();
            var food = dao.GetFoodCateByID(id);
            return Json(food, JsonRequestBehavior.AllowGet);
        }

        // [POST] Admin/Category/UpdateCategory
        [HttpPost]
        public ActionResult UpdateCategory(FormCollection formCollection)
        {
            var dt = DateTime.UtcNow;
            int UpdatedDayUnix = CNWeb.Helper.ConvertDateTime.ConvertDateTimeToUnix(dt);
            var UpdatedDay = UpdatedDayUnix;

            int id = Int32.Parse(formCollection["ID"]);
            string name = formCollection["Name"];
            int status = Int32.Parse(formCollection["Status"]);
            string updatedby = formCollection["FullName"];
            
            try // Update to Db
            {
                // TODO: Update Staff here
                var model = new FoodModel();
                bool res = model.UpdateCateFood(id, name, UpdatedDay, updatedby,status);  // update to database
                if (res)
                {
                    return Json(new { message = "Success!!", data = "Cập nhật danh mục thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Fail!!", data = "Cập nhật danh mục không thành công" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return PartialView("Information");
            }
        }

        // [POST] Admin/Category/AddCategory
        [HttpPost]
        public ActionResult AddCategory(FormCollection formCollection)
        {
            var dt = DateTime.UtcNow;
            int UpdatedDayUnix = CNWeb.Helper.ConvertDateTime.ConvertDateTimeToUnix(dt);
            var UpdatedDay = UpdatedDayUnix;

            string name = formCollection["Name"];
            int status = Int32.Parse(formCollection["Status"]);
            string updatedby = formCollection["FullName"];

            try // Update to Db
            {
                // TODO: Update Staff here
                var model = new FoodModel();
                bool res = model.AddCateFood(name, UpdatedDay, updatedby, UpdatedDay, updatedby, status);  // update to database
                if (res)
                {
                    return Json(new { message = "Success!!", data = "Thêm danh mục thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Fail!!", data = "Thêm danh mục không thành công" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return PartialView("Information");
            }
        }
    }
}