using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Domain.Model.Data
{
    public class LookupModel
    {
        public class AddressKeyword
        {
            public int AddressId { get; set; }
            public string AddressKeywordDescription { get; set; }
        }

        public class CivilDebtCode
        {
            public string DebtCode { get; set; }
            public string DebtDescription { get; set; }
        }

        public class Court
        {
            public string Code { get; set; }
            public string CourtName { get; set; }
        }

        public class CourtType
        {
            public string CourtTypeCode { get; set; }
            public string CourtTypeDescription { get; set; }
        }

        public class DebtReview
        {
            public string DebtReviewCode { get; set; }
            public string DebtReviewDescritpion { get; set; }
        }

        public class Default
        {
            public string DefaultCode { get; set; }
            public string DefaultDescription { get; set; }
        }

        public class Gender
        {
            public string GenderCode { get; set; }
            public string GenderDescription { get; set; }
        }

        public class JudegementCode
        {
            public string Code { get; set;}
            public string Description { get; set; }
        }

        public class Ownership
        {
            public string OwnershipCode { get; set; }
            public string OwnershipDescription { get; set; }
        }

        public class Province
        {
            public string ProvinceCode { get; set; }
            public string ProvinceName { get; set; }
        }

        public class Title
        {
            public string TitleCode { get; set; }
            public string TitleDescription { get; set; }
        }

        public class Trace
        {
            public string TraceCode { get; set; }
            public string TraceDescription { get; set; }
        }
    }
}
