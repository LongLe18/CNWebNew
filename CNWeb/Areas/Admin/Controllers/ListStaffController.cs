using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CNWeb.Helper;
using Models.Common;

namespace CNWeb.Areas.Admin.Controllers
{
    public class ListStaffController : Controller
    {
        // Set ViewBag: List Position
        public void SetViewBag(int? selectedID = null)
        {
            var dao = new AccountModel();
            ViewBag.ChucVu = new SelectList(dao.ListAllChucVu(), "ID", "TenChucVu", selectedID);
        }

        // [GET] Admin/ListStaff/ListStaff
        public ActionResult ListStaff()
        {
            SetViewBag();
            return PartialView("ListStaff");
        }

        // [GET] Admin/ListStaff/GetPaggedData
        public ActionResult GetPaggedData(int pageNumber = 1, int pageSize = 5)
        {
            var dao = new StaffModel();
            var model = dao.ListAllStaff().ToList();
            var pagedData = Pagination.PagedResult(model, pageNumber, pageSize);
            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }

        // [POST] Admin/ListStaff/DeleteStaffById
        [HttpPost]
        public ActionResult DeleteStaffById(int ID)
        {
            var dao = new StaffModel();
            var result = dao.DeleteStaffById(ID);
            if (result == 2)
            {
                return Json(new { message= "Success!" }, JsonRequestBehavior.AllowGet);
            } else if (result == 1)
            {
                return Json(new { message = "Fail! Nhân viên này không thể xóa!!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Fail!" }, JsonRequestBehavior.AllowGet);
            }
        }

        // [POST] Admin/ListStaff/PersonalInfor
        [HttpGet]
        public ActionResult PersonalInfor(int id)
        {
            var dao = new PersonalModel();
            var user = dao.GetStaffById(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        // [POST] Admin/ListStaff/UpdateInforStaff
        [HttpPost]
        public ActionResult UpdateInforStaff(FormCollection formCollection)
        {
            var dt = DateTime.UtcNow;
            int createdDayUnix = CNWeb.Helper.ConvertDateTime.ConvertDateTimeToUnix(dt);
            var CreatedDay = createdDayUnix;

            int id = Int32.Parse(formCollection["ID"]);
            string username = formCollection["UserName"];
            string password = formCollection["Password"];
            string fullname = formCollection["FullName"];
            string email = formCollection["Email"];
            int birthday = CreatedDay;
            if (formCollection["BirthDay"] != "") 
                birthday = Int32.Parse(formCollection["BirthDay"]);
            string career = formCollection["Career"];
            string address = formCollection["Address"];
            string city = formCollection["City"];
            string country = formCollection["Country"];
            string postalcode = formCollection["PostalCode"];
            int position = Int32.Parse(formCollection["IDChucVu"]);
            string description = formCollection["Description"];
            int level = 0;

            try // Update to Db
            {
                // TODO: Update Staff here
                if (Int32.Parse(formCollection["IDChucVu"]) == Constant.ID_MANAGER) // Quan ly
                {
                    level = Constant.LEVEL_MANAGER;
                }
                else // Nhan Vien
                {
                    level = Constant.LEVEL_STAFF;
                }
                
                var model = new StaffModel();
                bool res = model.UpdateInforStaff(id, username, password, fullname, email, birthday, career, address,
                                                    city, country, postalcode, position, description, level);  // update to database
                if (res)
                {
                    return Json(new { message = "Success!!", data = "Cập nhật nhân viên thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Fail!!", data = "Cập nhật nhân viên không thành công" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return PartialView("Information");
            }
        }
    }
}