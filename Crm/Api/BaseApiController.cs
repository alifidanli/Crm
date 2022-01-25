using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Crm.Api
{
    public class BaseApiController : ApiController
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["CrmConnectionString"].ConnectionString;

    }
}
