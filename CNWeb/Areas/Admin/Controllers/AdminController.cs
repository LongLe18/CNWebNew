using CNWeb.Controllers;
using CNWeb.Helper;
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
        [HasCredential(RoleID = "VIEW_ADMIN")]
        public ActionResult Home() // view initialize Dashboard
        {
            return View();
        }

        // [GET] Admin/Admin/Dashboard
        [HasCredential(RoleID = "VIEW_ADMIN")]
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
    }
}