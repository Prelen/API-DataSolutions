using DataSolution.Domain.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Interfaces.Repository
{
    public interface INotificationRepository
    {
        bool InsertNotification(NotificationModel Notification);
        List<NotificationModel> GetNotifications(int UserID);
    }
}
