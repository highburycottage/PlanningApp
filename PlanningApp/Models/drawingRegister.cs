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
    
    public partial class drawingRegister
    {
        public int drawingNumber { get; set; }
        public string description { get; set; }
        public string revisionID { get; set; }
        public string issued { get; set; }
        public string comments { get; set; }
        public string printSize { get; set; }
        public Nullable<bool> drawingValid { get; set; }
        public string originatorID { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }
    }
}
