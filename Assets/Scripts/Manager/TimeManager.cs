using GameSparks.Api.Requests;
using System;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    public float remainingTimeOfAttendance { get; set; }

    public void Initialize()
    {
        AttendanceCheck();
    }

    private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
        return dtDateTime;
    }

    public DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }


    public double ConvertToUnixTimestamp(DateTime date)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        TimeSpan diff = date - origin;
        return Math.Floor(diff.TotalSeconds);
    }


    public void GetLastConnectTime()
    {
        new LogEventRequest()
            .SetEventKey("G_LastConnectTime")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    var scriptData = (long)response.ScriptData.GetLong("LastConnectTime");

                }
                else
                {
                    Debug.Log("Error LastConnectTime Load");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void SaveLastConnectTime()
    {
        new LogEventRequest()
            .SetEventKey("SaveLastConnectTime")
            .Send((response) =>
            {
            if (!response.HasErrors)
            {
                Debug.Log("Success SaveLastConnectTime !");
            }
            else
            {
                Debug.Log("Error SaveLastConnectTime !");
                Debug.Log(response.Errors.JSON);
            }
        });
    }

    public void AttendanceCheck(bool isInMainMenu = false)
    {
        new LogEventRequest()
            .SetEventKey("AttendanceCheck")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    bool result = (bool)(response.ScriptData.GetBoolean("Result"));
                    bool isFirstTime = (bool)(response.ScriptData.GetBoolean("IsFirstTime"));

                    if (result)
                    {
                        if(!isFirstTime)
                        {
                            long remainingTime = (long)response.ScriptData.GetLong("RemainingTime");
                            long noLoginTime = (long)response.ScriptData.GetLong("NoLoginTime");
                            Debug.Log("남은 시간 : " + remainingTime + "로그인 안한 시간 : " + noLoginTime);
                        }

                        if (isInMainMenu)
                        {
                            GoodsManager.Instance.ResetRuneOnSales(true);
                        }
                        else
                        {
                            GoodsManager.Instance.ResetRuneOnSales(false);
                        }
                        Debug.Log("출석하기 실행");
                    }
                    else
                    {
                        GoodsManager.Instance.LoadRuneOnSalesData();
                        Debug.Log("출석되어있음");
                    }

                    GetRemainingTimeOfAttendance();
                    SaveLastConnectTime();
                }
                else
                {
                    Debug.Log("Error AttendanceCheck");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void GetRemainingTimeOfAttendance()
    {
        new LogEventRequest()
            .SetEventKey("GetRemainingTimeOfAttendance")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    remainingTimeOfAttendance = (float)response.ScriptData.GetFloat("RemainingTimeOfAttendance");
                    Debug.Log("남은 시간 : " + (int)remainingTimeOfAttendance / 60/60 + "시간  " + (int)remainingTimeOfAttendance / 60%60 + "분");
                }
                else
                {
                    Debug.Log("Error GetRemainingTimeOfAttendence");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }
}
