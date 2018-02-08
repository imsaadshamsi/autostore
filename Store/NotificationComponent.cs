using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Microsoft.AspNet.SignalR;

namespace Store
{
    public class NotificationComponent
    {

        public void RegisterNotification(DateTime currentTime) {
            string SqlConnectionString;
            using (var context = new AMotorsEntities())
                SqlConnectionString = context.Database.Connection.ConnectionString;

            string sqlCommand =@"Select [product_Id], [productCategory_Id],[name]      ,[model_Id]      ,[SpecialPrice]      ,[OldPrice]      ,[Price]      ,[StockQuantity]      ,[FullDescription]      ,[ShortDescription]      ,[OEM_num]      ,[ST_num]      ,[Part_num]  FROM[AMotors].[dbo].[product]";

            using (SqlConnection sqlCon = new SqlConnection(SqlConnectionString)) {
                SqlCommand cmd = new SqlCommand(sqlCommand, sqlCon);

                if (sqlCon.State != System.Data.ConnectionState.Open)
                    sqlCon.Open();

                cmd.Notification = null;

                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChange;
                //we must have to execute the command here
                //using (SqlDataReader reader = cmd.ExecuteReader())
                //{
                //    // nothing need to add here now
                //}
            }
        }

        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //or you can also check => if (e.Info == SqlNotificationInfo.Insert) , if you want notification only for inserted record
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;

                //from here we will send notification message to client
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("added");
                //re-register notification
                RegisterNotification(DateTime.Now);
            }
        }

        //public List<Contact> GetContacts(DateTime afterDate)
        //{
        //    using (MyPushNotificationEntities dc = new MyPushNotificationEntities())
        //    {
        //        return dc.Contacts.Where(a => a.AddedOn > afterDate).OrderByDescending(a => a.AddedOn).ToList();
        //    }
        //}
    }
}