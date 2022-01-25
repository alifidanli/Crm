using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Core.Models
{
    public class CustomerCall:BaseModel
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime DateOfCall { get; set; }
        public TimeSpan TimeOfCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
