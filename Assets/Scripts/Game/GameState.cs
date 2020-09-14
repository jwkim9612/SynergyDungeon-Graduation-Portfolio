using UnityEngine;

public class GameState : MonoBehaviour
{
    public delegate void OnPrepareDelegate();
    public delegate void OnBattleDelegate();
    public delegate void OnCompleteDelegate();
    public OnPrepareDelegate OnPrepare { get; set; }
    public OnBattleDelegate OnBattle { get; set; }
    public OnCompleteDelegate OnComplete { get; set; }

    public InGameState inGameState { get; set; }

    public bool isWaveClear { get; set; } = false;
    public bool isPlayerLose { get; set; } = false;

    public void Initialize()
    {
        SetInGameState(InGameState.Prepare);
    }

    private void Update()
    {
        if (inGameState == InGameState.Battle && isWaveClear)
        {
            SetInGameState(InGameState.Complete);
        }
        else if(inGameState == InGameState.Battle && isPlayerLose)
        {
            Debug.Log("패배");
            SetInGameState(InGameState.Lose);
        }
    }

    public void SetInGameState(InGameState newInGameState)
    {
        inGameState = newInGameState;

        switch (inGameState)
        {
            case InGameState.Prepare:
                OnPrepare();
                break;

            case InGameState.Battle:
                isWaveClear = false;
                OnBattle();
                break;

            case InGameState.Complete:
                OnComplete();

                if (StageManager.Instance.IsFinalWave())
                {
                    PlayerDataManager.Instance.playerData.UpdatePlayableChapter();
                    PlayerDataManager.Instance.playerData.InitializeTopWave();
                    InGameManager.instance.playerState.GetRewards();
                    InGameManager.instance.frontCanvas.uiStageClear.SetEndStageScreen();
                    InGameManager.instance.frontCanvas.ShowStageClear();
                }
                else
                {
                    StageManager.Instance.IncreaseWaveAndSetCurrentStage(1);

                    if (StageManager.Instance.IsFinalWave())
                    {
                        SaveManager.Instance.RemoveInGameData();
                        Debug.Log("데이터 삭제!");
                    }
                    else
                    {
                        SaveManager.Instance.SetInGameData();
                        SaveManager.Instance.SaveInGameData();
                    }

                    SetInGameState(InGameState.Prepare);
                }
                break;

            case InGameState.Lose:
                PlayerDataManager.Instance.playerData.TopWave = StageManager.Instance.currentWave - 1;
                InGameManager.instance.playerState.GetRewards();
                InGameManager.instance.frontCanvas.uiGameOver.SetEndStageScreen();
                InGameManager.instance.frontCanvas.ShowGameOver();
                break;
        }

    }

    /// <summary>
    /// 웨이브 클리어함으로 바꿔주는 함수
    /// </summary>
    public void SetIsWaveClear()
    {
        isWaveClear = true;
    }

    public void SetIsPlayerLose()
    {
        isPlayerLose = true;
    }

    public bool IsInBattle()
    {
        return inGameState == InGameState.Battle;
    }
}
