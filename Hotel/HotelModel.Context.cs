﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HotelDbEntities : DbContext
    {
        public HotelDbEntities()
            : base("name=HotelDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Amenity> Amenities { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Maid> Maids { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
    }
}
