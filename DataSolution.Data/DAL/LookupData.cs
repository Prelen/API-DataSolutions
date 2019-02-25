using DataSolution.Data.Data;
using DataSolution.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataSolution.Domain.Model.Data.LookupModel;

namespace DataSolution.Data.DAL
{
    public class LookupData
    {
        public List<Province> GetAllProvinces()
        {
            List<Province> provinces = new List<Province>();

            try
            {
                using (LookupEntities lookup = new LookupEntities())
                {
                    provinces = (from p in lookup.lk_ProvinceCode
                                 select new Province
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
    }
}
