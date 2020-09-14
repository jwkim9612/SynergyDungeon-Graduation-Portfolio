using Dapper;
using MXServer_API.DB.Implementation;
using Server_API.DB.Implementation;
using Server_API.Utility;
using System;

namespace Server_API.DB
{
    public class DBProcedure : DBProcedureImplementation
    {
        public static void InitJsonMapper()
        {
            SqlMapper.AddTypeHandler(new JObjectTypeHandler());
            SqlMapper.AddTypeHandler(new JArrayTypeHandler());
        }

        private static DBConfig config = ConfigurationManager.Instance.DBConfig;

        public static DateTime GetServerTimestamp(DBConfig config)
        {
            using (var db = config.DBProviderFactory.CreateConnection())
            {
                db.ConnectionString = config.ConnectionString;
                return DBProcedureImplementation.GetServerTimestamp(db);
            }
        }
    }
}
