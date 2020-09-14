using GameSparks.Api.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public delegate void OnTimeRewardCheckDelegate();
    public OnTimeRewardCheckDelegate OnTimeRewardCheck { get; set; }

    public float remainingTimeOfTimeReward { get; set; }

    public void TimeRewardCheck()
    {
        new LogEventRequest()
            .SetEventKey("TimeRewardCheck")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    bool canReceiptTimeReward = (bool)response.ScriptData.GetBoolean("CanReceiptTimeReward");

                    if (!canReceiptTimeReward)
                    {
                        remainingTimeOfTimeReward = (float)response.ScriptData.GetFloat("RemainingTime");
                    }
                    else
                    {
                        remainingTimeOfTimeReward = 0;
                    }

                    OnTimeRewardCheck();
                }
                else
                {
                    Debug.Log("Error TimeRewardCheck");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void TimeRewardReceipt()
    {
        new LogEventRequest()
            .SetEventKey("TimeRewardReceipt")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    bool result = (bool)response.ScriptData.GetBoolean("Result");
                    if(result)
                    {
                        Debug.Log("수령 성공!");
                        TimeRewardCheck();
                    }
                    else
                    {
                        Debug.Log("수령 실패!");
                    }
                }
                else
                {   
                    Debug.Log("Error TimeRewardCheck");
                    Debug.Log(response.Errors.JSON);
                }
    });
    }
}
