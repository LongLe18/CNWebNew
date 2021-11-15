using CNWeb.Helper;
using Models.Common;
using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNWeb.Areas.Admin.Controllers
{
    public class PersonalController : Controller
    {
        // Set ViewBag: List Position
        public void SetViewBag(int? selectedID = null)
        {
            var dao = new AccountModel();
            ViewBag.IDChucVu = new SelectList(dao.ListAllChucVu(), "ID", "TenChucVu", selectedID);
        }

        // GET: Admin/Personal/Information
        [HasCredential(RoleID = "VIEW_ADMIN")]
        public ActionResult Information()
        {
            SetViewBag();
            return PartialView("Information");
        }

        // POST: Admin/Personal/PersonalInfor
        [HttpPost]
        public ActionResult PersonalInfor(int id)
        {
            var dao = new PersonalModel();
            var user = dao.GetStaffById(id);
            return Json(user);
        }

        // POST: /Admin/Personal/UpdateInforStaff
        [HttpPost]
        public ActionResult UpdateInforStaff(User Staff, HttpPostedFileBase file) // update date Infor's Staff
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
            try // Update to Db
            {
                // TODO: Insert Staff here
                if (Staff.IDChucVu == Constant.ID_MANAGER) // Quan ly
                {
                    Staff.Level = Constant.LEVEL_MANAGER;
                }
                else // Nhan Vien
                {
                    Staff.Level = Constant.LEVEL_STAFF;
                }
                var dt = DateTime.UtcNow;
                int createdDayUnix = CNWeb.Helper.ConvertDateTime.ConvertDateTimeToUnix(dt);
                Staff.CreatedDay = createdDayUnix;

                var model = new PersonalModel();
                bool res = model.UpdateInforStaff(Staff);  // update to database
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
                return PartialView("Information", Staff);
            }
        }
    }
}