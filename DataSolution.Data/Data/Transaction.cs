//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataSolution.Data.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int TransID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public Nullable<System.DateTime> TransStartDate { get; set; }
        public Nullable<System.DateTime> TransEndDate { get; set; }
        public Nullable<bool> IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
