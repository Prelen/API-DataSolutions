using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Model.Data
{
    public class LookupModel
    {
        public class AddressKeywordModel
        {
            public int AddressId { get; set; }
            public string AddressKeywordDescription { get; set; }
        }

        public class CivilDebtCodeModel
        {
            public string DebtCode { get; set; }
            public string DebtDescription { get; set; }
        }

        public class CourtModel
        {
            public string Code { get; set; }
            public string CourtName { get; set; }
        }

        public class CourtTypeModel
        {
            public string CourtTypeCode { get; set; }
            public string CourtTypeDescription { get; set; }
        }

        public class DebtReviewModel
        {
            public string DebtReviewCode { get; set; }
            public string DebtReviewDescritpion { get; set; }
        }

        public class DefaultModel
        {
            public string DefaultCode { get; set; }
            public string DefaultDescription { get; set; }
        }

        public class GenderModel
        {
            public string GenderCode { get; set; }
            public string GenderDescription { get; set; }
        }

        public class JudegementCodeModel
        {
            public string Code { get; set;}
            public string Description { get; set; }
        }

        public class OwnershipModel
        {
            public string OwnershipCode { get; set; }
            public string OwnershipDescription { get; set; }
        }

        public class ProvinceModel
        {
            public string ProvinceCode { get; set; }
            public string ProvinceName { get; set; }
        }

        public class TitleModel
        {
            public string TitleCode { get; set; }
            public string TitleDescription { get; set; }
        }

        public class TraceModel
        {
            public string TraceCode { get; set; }
            public string TraceDescription { get; set; }
        }

        public class AlertModel
        {
            public string  AlertCode { get; set; }
            public string AlertDescription { get; set; }
        }

        public class SearchTypeModel
        {
            public string SearchCode { get; set; }
            public string SearchDescription { get; set; }
        }

        public class EnquiryReasonModel
        {
            public string EnquiryCode { get; set; }
            public string EnquiryDescription { get; set; }
        }

        

       
    }
}
