using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Core.Models
{
    public class Permission:BaseModel
    {
        public const int EMPLOYEE = 1;
        public const int MANAGER = 2;
        public const int DIRECTOR = 3;

        public int Id { get; set; }
        public string Name { get; set; }

    }
}
