using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server_API.DB.Implementation
{
    public partial class DBProcedureImplementation
    {
        protected static DateTime GetServerTimestamp(IDbConnection db)
        {
            var result = db.QueryFirstOrDefault<DateTime>("SELECT CURRENT_TIMESTAMP;");
            return result;
        }
    }
}
