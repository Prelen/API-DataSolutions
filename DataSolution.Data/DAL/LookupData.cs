using DataSolution.Data.Data;
using DataSolution.Domain.Interfaces.Repository;
using DataSolution.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataSolution.Domain.Model.Data.LookupModel;

namespace DataSolution.Data.DAL
{
    public class LookupData : ILookupRepository
    {
        public List<ProvinceModel> GetAllProvinces()
        {
            List<ProvinceModel> provinces = new List<ProvinceModel>();

            try
            {
                using (LookupEntities lookup = new LookupEntities())
                {
                    provinces = (from p in lookup.lk_ProvinceCode
                                 select new ProvinceModel
                                 {
                                     ProvinceCode = p.ProvinceCode,
                                     ProvinceName = p.ProvinceName
                                 }).ToList();

                    return provinces;

                }
                
            }
            catch (Exception ex)
            {

                var log = new Logger();
                log.LogError("N/A", "DataSolutions.Data", "GetAllProvinces", ex.Message);
            }
            return provinces;
        }

        public List<AlertModel> GetAllAlertTypes()
        {
            List<AlertModel> alerts = new List<AlertModel>();
            try
            {
                using (LookupEntities lookup = new LookupEntities())
                {
                    alerts = (from l in lookup.lk_Alert
                              select new AlertModel
                              {
                                  AlertCode = l.AlertCode,
                                  AlertDescription = l.AlertDescription
                              }).ToList();
                }
            }
            catch (Exception ex)
            {

                var log = new Logger();
                log.LogError("N/A", "DataSolutions.Data", "GetAllAlertTypes", ex.Message);
            }

            return alerts;
        }

        public List<SearchTypeModel> GetSearchTypes()
        {
            List<SearchTypeModel> searchType = new List<SearchTypeModel>();

            try
            {
                using (LookupEntities lookup = new LookupEntities())
                {
                    searchType = (from l in lookup.lk_SearchType
                                  orderby l.SearchCode descending
                                  select new SearchTypeModel
                                  {
                                      SearchCode = l.SearchCode,
                                      SearchDescription = l.SearchTypeDescription
                                  }).ToList();
                }
            }
            catch (Exception ex)
            {

                var log = new Logger();
                log.LogError("N/A", "DataSolutions.Data", "GetSearchTypes", ex.Message);
            }
            return searchType;
        }

        public List<EnquiryReasonModel> GetEnquiryReasons()
        {
            List<EnquiryReasonModel> enquiryReasons = new List<EnquiryReasonModel>();
            try
            {
                using (LookupEntities lookup = new LookupEntities())
                {
                    enquiryReasons = (from l in lookup.lk_EnquiryReason
                                      orderby l.EnquiryDescription descending
                                      select new EnquiryReasonModel
                                      {
                                          EnquiryCode = l.EnquiryCode,
                                          EnquiryDescription = l.EnquiryDescription
                                      }).ToList();
                }
            }
            catch (Exception ex)
            {

                var log = new Logger();
                log.LogError("N/A", "DataSolutions.Data", "GetEnquiryReasons", ex.Message);
            }

            return enquiryReasons;
        }

        public List<EnquiryReasonModel> GetEnquirerReason()
        {
            List<EnquiryReasonModel> results = new List<EnquiryReasonModel>();

            using (LookupEntities lookup = new LookupEntities())
            {
                results = (from l in lookup.lk_EnquireReason
                           orderby l.EnquireReason descending
                           select new EnquiryReasonModel
                           {
                               EnquiryCode = l.EnqCode,
                               EnquiryDescription = l.EnqCode
                           }).ToList();
            }

            return results;
        }

    }
}
