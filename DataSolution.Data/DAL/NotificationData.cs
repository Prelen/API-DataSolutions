using AutoMapper;
using DataSolution.Data.Data;
using DataSolution.Domain.Interfaces.Repository;
using DataSolution.Domain.Model.Data;
using DataSolution.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Data.DAL
{
   public class NotificationData : INotificationRepository
    {
        public bool InsertNotification(NotificationModel Notification)
        {
            bool result = false;
            try
            {
                using (NotificationEntities notificationEntities = new NotificationEntities())
                {
                    Mapper.Initialize(
                        cfg =>
                        {
                            cfg.CreateMap<NotificationModel, Notification>();
                        }
                        );

                    var notification = Mapper.Map<Notification>(Notification);
                    notificationEntities.Notifications.Add(notification);
                    notificationEntities.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                var log = new Logger();
                log.LogError(Notification.UserID.ToString(), "DataSolutions.Data", "InsertNotification", ex.Message);
            }

            return result;
        }

        public List<NotificationModel> GetNotifications(int UserID)
        {
            List<NotificationModel> userNotification = new List<NotificationModel>();
            try
            {
                using (NotificationEntities notificationEntities = new NotificationEntities())
                {
                    var notification = (from n in notificationEntities.Notifications
                                        where n.UserID == UserID && n.IsDimissed == false 
                                        select new NotificationModel {
                                            DateCreated = n.DateCreated,
                                            IsDimissed = n.IsDimissed,
                                            NotificationID = n.NotificationID,
                                            NotificationMessage = n.NotificationMessage,
                                            UserID = n.UserID
                                        });
                    userNotification = notification.ToList();

                }
            }
            catch (Exception ex)
            {

                var log = new Logger();
                log.LogError(UserID.ToString(), "DataSolutions.Data", "GetNotifications", ex.Message);
            }

            return userNotification;
         }
    }
}
