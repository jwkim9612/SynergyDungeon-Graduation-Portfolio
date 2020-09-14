using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Server_API.DB
{
    public class DBConfig
    {
        public DBConfig(string connectionString, DbProviderFactory dbProviderFactory)
        {
            ConnectionString = connectionString;
            DBProviderFactory = dbProviderFactory;
        }

        public string ConnectionString { get; }
        public DbProviderFactory DBProviderFactory { get; }
    }
}
