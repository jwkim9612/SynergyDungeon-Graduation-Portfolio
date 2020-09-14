using Shared.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace Server_API.Model
{
    public class Session
    {
        public long SessionDBId { get; set; }
        public long AccountId { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastConnectDate { get; set; }
        public int ConnectionTime { get; set; }

        public SessionKey SessionKey => new SessionKey
        {
            SessionServerId = SessionDBId,
            AccountServerId = AccountId,
        };

        public SessionDB DBInfo => new SessionDB
        {
            SessionKey = SessionKey,
            LastConnect = LastConnectDate,
            ConnectionTime = ConnectionTime,
        };

        public override string ToString()
        {
            return string.Format($"[ SessionId : {SessionDBId} / AccountServerId : {AccountId} / Creation : {CreationDate} / LastConnect : {LastConnectDate} / ConnectionTime : {ConnectionTime} ]");
        }
    }
}
