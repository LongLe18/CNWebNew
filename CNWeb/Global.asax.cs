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

        // Added the following procedure:
        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            DateTime MyScheduledRunTime = DateTime.Now;

            
            DateTime CurrentSystemTime = DateTime.Now;

            Debug.WriteLine(string.Concat("Timer Event Handler Called: ", CurrentSystemTime.ToString()));

           
            DateTime LatestRunTime = MyScheduledRunTime.AddMilliseconds(TimerIntervalInMilliseconds);

            
            if ((CurrentSystemTime.CompareTo(MyScheduledRunTime) >= 0) && (CurrentSystemTime.CompareTo(LatestRunTime) <= 0))
            {
                Debug.WriteLine(String.Concat("Timer Event Handling MyScheduledRunTime Actions: ", DateTime.Now.ToString()));
                
            }
        }
    }
}
