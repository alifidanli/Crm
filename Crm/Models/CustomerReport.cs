//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crm.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerReport
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public Nullable<System.DateTime> DateOfCall { get; set; }
        public Nullable<System.TimeSpan> TimeOfCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
