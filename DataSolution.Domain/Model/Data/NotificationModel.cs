using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Model.Data
{
    public class NotificationModel
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDimissed { get; set; }
    }
}
