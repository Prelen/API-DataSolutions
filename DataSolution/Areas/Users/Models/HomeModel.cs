using DataSolution.Domain.Model.Data;
using DataSolution.Domain.Model.Web.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataSolution.Areas.Users.Models
{
    public class HomeModel
    {
        public ChartOptions[] ChartValues;
        public UserModel UserInfo;
        public AuditModelExtended[] AuditInfo;
        public NotificationsModelExtended[] Notifications;
    }

    public class AuditModelExtended
    {
        public AuditModel AuditInfo { get; set; }
        public string FormattedDay { get; set; }
        public string FormattedMonth { get; set; }
    }

    public class NotificationsModelExtended
    {
        public NotificationModel NotificationInfo { get; set; }
        public string FormattedDay { get; set; }
        public string FormattedMonth { get; set; }
    }
}