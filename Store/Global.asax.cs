using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Data.SqlClient;
using Store;

namespace Store
{
    public class Global : HttpApplication
    {
        protected String SqlConnectionString { get; set; }
        protected void Application_Start(object sender, EventArgs e)
        {
            using (var context = new AMotorsEntities())
                SqlConnectionString = context.Database.Connection.ConnectionString;

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //if (!string.IsNullOrEmpty(SqlConnectionString))
                //SqlDependency.Start(SqlConnectionString);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            NotificationComponent NC = new NotificationComponent();
            var currentTime = DateTime.Now;
            HttpContext.Current.Session["LastUpdated"] = currentTime;
            NC.RegisterNotification(currentTime);
        }


        protected void Application_End()
        {
            //here we will stop Sql Dependency
          //  SqlDependency.Stop(SqlConnectionString);
        }

        /*  protected void Application_End()
          {
              if (!string.IsNullOrEmpty(SqlConnectionString))
                  SqlDependency.Start(SqlConnectionString);
          }
          */



    }
}