﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LookupEntities : DbContext
    {
        public LookupEntities()
            : base("name=LookupEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<lk_AddressKeyword> lk_AddressKeyword { get; set; }
        public DbSet<lk_CivilDebtCode> lk_CivilDebtCode { get; set; }
        public DbSet<lk_Court> lk_Court { get; set; }
        public DbSet<lk_CourtType> lk_CourtType { get; set; }
        public DbSet<lk_DebtReview> lk_DebtReview { get; set; }
        public DbSet<lk_Gender> lk_Gender { get; set; }
        public DbSet<lk_JudgementCode> lk_JudgementCode { get; set; }
        public DbSet<lk_Ownership> lk_Ownership { get; set; }
        public DbSet<lk_ProvinceCode> lk_ProvinceCode { get; set; }
        public DbSet<lk_Title> lk_Title { get; set; }
        public DbSet<lk_Trace> lk_Trace { get; set; }
        public DbSet<lk_Default> lk_Default { get; set; }
        public DbSet<lnk_UserProducts> lnk_UserProducts { get; set; }
        public DbSet<lk_Alert> lk_Alert { get; set; }
        public DbSet<lk_SearchType> lk_SearchType { get; set; }
        public DbSet<lk_EnquiryReason> lk_EnquiryReason { get; set; }
        public DbSet<lk_EnquireReason> lk_EnquireReason { get; set; }
    }
}
