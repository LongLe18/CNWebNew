using Models.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CNWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
       
        private static double TimerIntervalInMilliseconds =
            Convert.ToDouble(WebConfigurationManager.AppSettings["TimerIntervalInMilliseconds"]);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           

            Debug.WriteLine(string.Concat("Application_Start Called: ", DateTime.Now.ToString()));

           
            Timer timer = new Timer(TimerIntervalInMilliseconds);

            timer.Enabled = true;

            
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            timer.Start();
        }

        
        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            DateTime MyScheduledRunTime = DateTime.Now;
            var db = new DbCNWeb();
            
            DateTime CurrentSystemTime = DateTime.Now;
            var t = CNWeb.Helper.ConvertDateTime.ConvertDateTimeToUnix(DateTime.Now);

            Debug.WriteLine(string.Concat("Timer Event Handler Called: ", CurrentSystemTime.ToString()));

           
            DateTime LatestRunTime = MyScheduledRunTime.AddMilliseconds(TimerIntervalInMilliseconds);

            // Cập nhật thông tin giá khuyến mãi theo thời gian thực
            if ((CurrentSystemTime.CompareTo(MyScheduledRunTime) >= 0) && (CurrentSystemTime.CompareTo(LatestRunTime) <= 0))
            {
                Debug.WriteLine(String.Concat("Timer Event Handling Update PromotionPrice Actions: ", DateTime.Now.ToString()));
                var a = db.Promotions.ToList();
                foreach (var item in a)
                {
                    if (t>item.ActiveDay && t < item.EndDay)
                    {
                        Debug.WriteLine(item.Name + " đang kích hoạt khuyến mãi");
                        var b = db.PromotionFoodDetails.Where(i => i.PromotionID == item.ID).ToList();
                        foreach( var i in b)
                        {
                            i.Food.PromotionPrice = i.Food.OriginPrice * (100 - i.PercentReduction) / 100;
                            
                        }
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
