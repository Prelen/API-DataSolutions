using DataSolution.Domain.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Interfaces.Repository
{
    public interface IAuditRepository
    {
        bool InsertAudit(AuditModel Audit);
        List<AuditModel> GetLast10Audit(int UserID);
    }
}
