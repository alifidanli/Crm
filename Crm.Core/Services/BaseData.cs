using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Crm.Core.Services
{
    public class BaseData
    {
        public string ConnectionString;
        private SqlConnection _connection = null;

        public SqlConnection Initialize()
        {
            if (!string.IsNullOrEmpty(this.ConnectionString))
                this._connection = new SqlConnection(this.ConnectionString);
            else
                throw new Exception("Empty Connection String");

            return Connection;
        }

        public SqlConnection Connection => this._connection;

    }
}
