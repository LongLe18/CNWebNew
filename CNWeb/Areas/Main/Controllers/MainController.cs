using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNWeb.Areas.Main.Controllers
{
    public class MainController : Controller
    {
        // [Authorize]
        // GET: Main/Main/Index
        public ActionResult Index()
        {
            var db = new DbCNWeb();
            var hotFood = db.Foods.Where(i => i.HotFlag == true).Take(4).ToList() ;
            ViewBag.hotFood = hotFood;
            var menuFood = db.Foods.Take(12).ToList();
            ViewBag.menuFood = menuFood;
            return View();
        }
    }
}