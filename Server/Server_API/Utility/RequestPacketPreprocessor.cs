using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Server_API.Model;
using Shared.NetworkProtocol;

namespace Server_API.Utility
{
    public class RequestPacketPreprocessResult
    {
        public bool Valid { get; set; }

        public RequestPacket RequestPacket { get; set; }
        public object ErrorJObject { get; set; }
    }

    public class RequestPacketPreprocessor
    {
        private static Protocol ExtractProtocol(string packetString)
        {
            var jObject = JObject.Parse(packetString);
            var protocolText = (string)jObject.GetValue("Protocol");
            if (string.IsNullOrEmpty(protocolText)) return Protocol.None;
            return Enum.TryParse<Protocol>(protocolText, true, out Protocol protocol) ? protocol : Protocol.None;
        }

        public static bool TryParseRequest(
            string packetString,
            Protocol formProtocol,
            Protocol expectedProtocol,
            out RequestPacket requestPacket,
            out object errorJObject)
        {
            requestPacket = null;
            errorJObject = null;

            try
            {
                switch (expectedProtocol)
                {
                    #region system
                    case Protocol.System_Version:
                        requestPacket = JsonService.DeserializeFromNetwork<SystemVersionRequest>(packetString);
                        return true;
                    case Protocol.Common_Cheat:
                        requestPacket = JsonService.DeserializeFromNetwork<CommonCheatRequest>(packetString);
                        return true;
                    case Protocol.Session_Info:
                        requestPacket = JsonService.DeserializeFromNetwork<SessionInfoRequest>(packetString);
                        return true;
                    #endregion

                    default:
                        throw new NotImplementedException($"protocol {expectedProtocol} has no matching request");
                }
            }
            catch (Exception e)
            {
                errorJObject = ResultPacketBuilder.CreateErrorPacket(WebAPIErrorCode.InvalidPacket, e.Message, packetString);
                return false;
            }
        }

        public static async Task<RequestPacketPreprocessResult> PreProcess(
            Protocol expectedProtocol,
            string packetString,
            Protocol formProtocol)
        {
            if (TryParseRequest(packetString, formProtocol, expectedProtocol, out RequestPacket requestPacket, out object errorJObject))
            {
                if (requestPacket.SessionKey != null && requestPacket.SessionKey.HasValue)
                {
                    Session session = await SessionService.GetValidSession(requestPacket.SessionKey.AccountServerId, requestPacket.SessionKey.SessionServerId);
                    if (session != null)
                    {
                        await SessionService.UpdateSessionByRequestPacket(session, requestPacket);
                        return new RequestPacketPreprocessResult()
                        {
                            Valid = true,
                            RequestPacket = requestPacket,
                        };
                    }
                }

                errorJObject = ResultPacketBuilder.CreateInvalidSessionPacket(packetString);
            }

            return new RequestPacketPreprocessResult()
            {
                Valid = false,
                ErrorJObject = errorJObject,
            };
        }
    }
}
