using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Core.Dto
{
  public  class UserDto : BaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public PermissionDto Permission { get; set; }
    }
}
