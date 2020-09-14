using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    //public class WeatherForecastController : ControllerBase
    //{
    //    #region TEST
    //    /// <summary> POST: api/raid/Lobby + form data </summary>
    //    [HttpPost("TEST")]
    //    public async Task<IActionResult> TESTPost([FromForm] Protocol protocol, [FromForm] string packet)
    //    {
    //        try
    //        {
    //            var preprocessResult = await RequestPacketPreprocessor.PreProcess(Protocol.Raid_Lobby, packet, protocol);
    //            if (!preprocessResult.Valid) return Ok(preprocessResult.ErrorJObject);
    //            if (!(preprocessResult.RequestPacket is RaidLobbyRequest request)) { throw new InvalidCastException(); }

    //            return await GetTEST(request.SessionKey.AccountServerId, packet);
    //        }
    //        catch (Exception e)
    //        {
    //            DebugMX.Log.Exception(e, packet);
    //            return Ok(ResultPacketBuilder.CreateErrorPacket(WebAPIErrorCode.InternalServerError, $"Internal Server Error. exception {e.Message} / call stack {e.StackTrace}", packet));
    //        }
    //    }

    //    private async Task<IActionResult> GetTEST(long accountServerId, string requestDump)
    //    {

    //    }
    //    #endregion
    //}
}
