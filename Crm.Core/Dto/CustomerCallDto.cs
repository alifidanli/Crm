using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Core.Dto
{
    public   class CustomerCallDto : BaseDto
    {
        public Guid Id { get; set; }
        public CustomerDto Customer { get; set; }
        public DateTime DateOfCall { get; set; }
        public TimeSpan TimeOfCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
