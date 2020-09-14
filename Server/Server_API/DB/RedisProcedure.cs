using Server_API.Model;
using Server_API.Utility;
using Shared.NetworkProtocol;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_API.DB
{
    public class RedisProcedure
    {
        private class ConnectionMultiplexerHolder : LazySingleton<ConnectionMultiplexerHolder>
        {
            private ConnectionMultiplexer redis;
            public ConnectionMultiplexer Redis => redis;

            public ConnectionMultiplexerHolder() : base()
            {
                redis = null;
            }

            public void Initialize(string configuration)
            {
                redis = ConnectionMultiplexer.Connect(configuration);
            }
        }

        public static void Initialize(string configuration)
        {
            ConnectionMultiplexerHolder.Instance.Initialize(configuration);
        }

        #region session
        private static string SessionKey(long accountServerId)
        {
            return $"Session:{accountServerId}";
        }

        public static async Task<Session> GetSession(long accountServerId)
        {
            var db = ConnectionMultiplexerHolder.Instance.Redis.GetDatabase();
            var redisValue = await db.StringGetAsync(SessionKey(accountServerId));

            return redisValue.HasValue ? JsonService.DeserializeFromDB<Session>(redisValue) : null;
        }

        public static async Task SetSession(Session session)
        {
            var db = ConnectionMultiplexerHolder.Instance.Redis.GetDatabase();
            var sessionString = JsonService.SerializeToDB(session);
            await db.StringSetAsync(SessionKey(session.AccountId), sessionString);
            return;
        }
        #endregion
    }
}
