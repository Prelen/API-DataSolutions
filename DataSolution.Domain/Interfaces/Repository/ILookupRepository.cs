using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSolution.Domain.Interfaces;
using static DataSolution.Domain.Model.Data.LookupModel;

namespace DataSolution.Domain.Interfaces.Repository
{
    public interface ILookupRepository
    {
        List<ProvinceModel> GetAllProvinces();
        List<AlertModel> GetAllAlertTypes();
        List<SearchTypeModel> GetSearchTypes();
    
    }
}
