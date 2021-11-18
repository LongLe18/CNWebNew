using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Models.Dao;
using Models.Common;
using CNWeb.Helper;

namespace CNWeb.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        protected void SetAlert(string message, int type)
        {
            TempData["AlertMessage"] = message;
            if (type == 1)
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == 2)
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == 3)
            {
                TempData["AlertType"] = "alert-danger";
            }
            else
            {
                TempData["AlertType"] = "alert-info";
            }
        }

        // Set ViewBag: List ChucVu
        public void SetViewBag(int? selectedID = null)
        {
            var dao = new AccountModel();
            ViewBag.IDChucVu = new SelectList(dao.ListAllChucVu(), "ID", "TenChucVu", selectedID);
        }

        // [GET] /Admin/Staff/Register
        [HasCredential(RoleID = "ADD_STAFF")]
        public ActionResult Register()
        {
            SetViewBag();
            return PartialView("Register");
        }

        // [POST] /Admin/Staff/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User Staff, HttpPostedFileBase file) // Đăng ký cho nhân viên
        {
            SetViewBag(); // get ID ChucVu tuong ung
            if (file != null && file.ContentLength > 0) // Save detached image
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/img/User"),
                                       Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    Staff.Image = "/Content/img/User/" + Staff.Image;
                }
                catch 
                {
                    ViewBag.Message = "Upload hình không thành công!";
                }
            }
            try // Insert to Db
            {
                // TODO: Insert Staff here
                if (Staff.IDChucVu == Constant.ID_MANAGER) // Quan ly
                {
                    Staff.Level = Constant.LEVEL_MANAGER;
                } else // Nhan Vien
                {
                    Staff.Level = Constant.LEVEL_STAFF;
                }
                var dt = DateTime.UtcNow;
                int createdDayUnix = CNWeb.Helper.ConvertDateTime.ConvertDateTimeToUnix(dt);
                Staff.CreatedDay = createdDayUnix;

                var model = new AccountModel();
                int res = model.Register(Staff);  // add to database
                if (res == 1)
                {
                    return RedirectToAction("Home", "Admin");
                }
                else if (res == -1)
                {
                    SetAlert("Đã tồn tại nhân viên này!!", 3);
                    return View();
                }
                else
                {
                    SetAlert("Thêm nhân viên không thành công!!", 3);
                    return View();
                }
            } 
            catch
            {
                return View();
            }
        }
    }
}