using Microsoft.Extensions.Configuration;
using Server_API.DB;
using System;
using System.Data.Common;

namespace Server_API.Utility
{
    public class ConfigurationManager : LazySingleton<ConfigurationManager>
    {
        public DBConfig DBConfig => dbConfig;
        private DBConfig dbConfig;
        public string DBConnectionString => dbConnectionString;
        private string dbConnectionString;
        public string RedisConnectionString => redisConnectionString;
        private string redisConnectionString;

        public void Initialize(IConfiguration config, DbProviderFactory dbProviderFactory)
        {
            dbConnectionString = config.GetConnectionString("MariaDB");
            dbConfig = new DBConfig(dbConnectionString, dbProviderFactory);
            redisConnectionString = config.GetConnectionString("Redis");
        }
    }
}
