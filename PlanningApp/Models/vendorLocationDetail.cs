//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlanningApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vendorLocationDetail
    {
        public string vendorLocationID { get; set; }
        public string vendorNumber { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorAddress2 { get; set; }
        public string vendorCity { get; set; }
        public string vendorCounty { get; set; }
        public string vendorPostCode { get; set; }
        public string vendorPhoneNumber { get; set; }
        public string vendorContact { get; set; }
        public string vendorEmail { get; set; }
    
        public virtual vendorHQTable vendorHQTable { get; set; }
    }
}
