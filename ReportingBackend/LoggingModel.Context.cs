﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication4
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MonitoringEntities : DbContext
    {
        public MonitoringEntities()
            : base("name=MonitoringEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TestDetail> TestDetails { get; set; }
        public virtual DbSet<MostRecentLoggingEvent> MostRecentLoggingEvents { get; set; }
        public virtual DbSet<MostRecentEventDetails_vw> MostRecentEventDetails_vw { get; set; }
    }
}
