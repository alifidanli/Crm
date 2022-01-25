using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Dal.Models
{
    public class CustomerCall
    {
        public Guid Id { get; set; }
        public int CustomerNo { get; set; }
        public DateTime DateOfCall { get; set; }
        public TimeSpan TimeOfCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
    }
}
