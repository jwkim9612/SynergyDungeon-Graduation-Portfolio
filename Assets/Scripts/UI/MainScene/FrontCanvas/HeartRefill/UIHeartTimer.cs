using GameSparks.Api.Requests;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIHeartTimer : MonoBehaviour
{
    [SerializeField] private Text timeText = null;
    [SerializeField] private Text titleText = null;

    private long remainingTime;

    public void Initialize()
    {
        //HeartUpdate();
    }

    public void TimeUpdate()
    {
        remainingTime = MainManager.instance.backCanvas.uiTopMenu.uiHeart.remainingTime;
        if(remainingTime > 0)
        {
            titleText.text = "다음 하트까지 남은시간";
            StartCoroutine(Co_TextHeartTimer());
        }
        else
        {
            titleText.text = "하트가 최대갯수입니다";
            timeText.text = "0 : 00";
        }
    }

    private IEnumerator Co_TextHeartTimer()
    {
        while (remainingTime > 0)
        {
            var minute = remainingTime / 60;
            var second = remainingTime % 60;

            timeText.text = $"{minute} : {second.ToString("D2")}";
            yield return new WaitForSeconds(1.0f);
            --remainingTime;
        }

        TimeUpdate();
    }
}
