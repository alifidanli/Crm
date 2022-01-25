using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm.Models
{
    public class Report
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public DateTime DateOfCall { get; set; }
        public TimeSpan TimeOfCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}