using CNWeb.Helper;
using CNWeb.Models;
using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNWeb.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        // [GET] Admin/Blog/Blog
        [HasCredential(RoleID = "EDIT_CONTENT")]
        public ActionResult Blog()
        {
            return PartialView("Blog");
        }

        // [GET] Admin/Blog/UpdateStatus
        public ActionResult UpdateStatus(string id, bool check)
        {
            try
            {
                var model = new BlogModel();
                var res = model.ChangeStatus(Int32.Parse(id), check);
                if (res)
                {
                    return Json(new { message = "Success!!", data = "Duyệt bài thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Fail!!", data = "Duyệt bài thất bại" }, JsonRequestBehavior.AllowGet);
                }

            } catch
            {
                return PartialView("Blog");
            }            
        }

        // [GET] Admin/Blog/UpdateAllStatus
        public JsonResult UpdateAllStatus(List<String> id, bool check)
        {
            try
            {
                var model = new BlogModel();
                var res = model.ChangeAllStatus(id, check);
                if (res)
                {
                    return Json(new { message = "Success!!", data = "Duyệt bài thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Fail!!", data = "Duyệt bài thất bại" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                return Json(new { message = "Fail!!", data = "Duyệt bài thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }

        // [GET] Admin/Blog/ViewBlog/{id}
        [HasCredential(RoleID = "EDIT_CONTENT")]
        public ActionResult ViewBlog(int id)
        {
            var model = new ViewDetailBlogModel();
            var db = new DbCNWeb();
            var data = (from a in db.Blogs
                        join b in db.Users on a.CreatedBy equals b.ID
                        where a.ID == id
                        select new ListBlog()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Description = a.Description,
                            Status = a.Status,
                            Content = a.Content,
                            CreatedDay = a.CreatedDate,
                            UserName = b.UserName,
                            pathImage = b.Image
                        }).ToList();
            model.data = data;
            return View(model);
        }
    }
}