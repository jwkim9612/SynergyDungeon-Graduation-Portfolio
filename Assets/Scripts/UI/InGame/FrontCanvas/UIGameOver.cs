using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : UIEndStage
{
    [SerializeField] private Text currentWaveText = null;

    public void SetCurrentWaveText()
    {
        int currentWave = StageManager.Instance.currentWave;
        currentWaveText.text = $"현재 웨이브 : {currentWave}";
    }

    public new void SetEndStageScreen()
    {
        base.SetEndStageScreen();
        SetCurrentWaveText();
    }
}
