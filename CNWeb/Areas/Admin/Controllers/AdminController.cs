using CNWeb.Controllers;
using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNWeb.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        // [GET] Admin/Admin/Home
        public ActionResult Home() // view initialize Dashboard
        {
            return View();
        }

        // [GET] Admin/Admin/Dashboard
        public ActionResult Dashboard()
        {
            return PartialView("Dashboard");
        }

        // [GET] partial Views SIDEBAR
        public ActionResult SideBar()
        {
            return PartialView("_SideBar");
        }

        // [GET] partial Views TOPNAV
        public ActionResult TopNav()
        {
            return PartialView("_TopNav");
        }

        // [GET] Admin/Admin/GetUser
        public JsonResult GetNumberUser()
        {
            var dao = new DashboardModel();
            var data = dao.GetNumberUser();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // [GET] Admin/Admin/GetNumberOrder
        public JsonResult GetNumberOrder()
        {
            var dao = new DashboardModel();
            var data = dao.GetNumberOrder();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // [GET] Admin/Admin/Sales
        public JsonResult GetSales()
        {
            var dao = new OrderModel();
            var data = dao.Order().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}