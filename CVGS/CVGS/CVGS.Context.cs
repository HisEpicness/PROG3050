﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CVGS
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CVGSEntities : DbContext
    {
        public CVGSEntities()
            : base("name=CVGSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<esrb_rating> esrb_rating { get; set; }
        public virtual DbSet<eventData> eventDatas { get; set; }
        public virtual DbSet<game> games { get; set; }
        public virtual DbSet<genre> genres { get; set; }
        public virtual DbSet<login> logins { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<passReset> passResets { get; set; }
        public virtual DbSet<platform> platforms { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<wishList> wishLists { get; set; }
    }
}
