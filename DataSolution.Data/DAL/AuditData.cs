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
    public class AuditData : IAuditRepository
    {
        
        public bool InsertAudit(AuditModel Audit)
        {
            bool result = false;
            try
            {
                using (AuditEntities auditEntities = new AuditEntities())
                {
                    Mapper.Initialize(
                       cfg =>
                        {
                            cfg.CreateMap<AuditModel, Audit>();
                    }
                    );
                    var audit = Mapper.Map<Audit>(Audit);
                    auditEntities.Audits.Add(audit);
                    auditEntities.SaveChanges();
                }

                 result = true;
            }
            catch (Exception ex)
            {

                var log = new Logger();
                log.LogError(Audit.UserID.ToString(), "DataSolutions.Data", "InsertAudit", ex.Message);
            }

            return result;

        }

        public List<AuditModel> GetLast10Audit(int UserID)
        {
            List<AuditModel> auditList = new List<AuditModel>();
            try
            {
                using (AuditEntities audit = new AuditEntities())
                {
                    var audits = (from a in audit.Audits
                                  where a.UserID == UserID
                                  select  new AuditModel
                                  {
                                    ActivityDescription = a.ActivityDescription,
                                    AuditDate = a.AuditDate,
                                    AuditID = a.AuditID,
                                    UserID = a.UserID
                                  }).Take(10);

                    auditList = audits.ToList();
                }
            }
            catch (Exception ex)
            {

                var log = new Logger();
                log.LogError(UserID.ToString(), "DataSolutions.Data", "InsertAudit", ex.Message);
            }

            return auditList;
        }
    }
}
