using GameSparks.Api.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIBattlefield : MonoBehaviour
{
    public UIBestWave uiBestWave;
    public UIUsedPotion uiUsedPotion;
    public UIChapterTitle uiChapterTitle;
    public UIChapterImageAndButton uiChapterImageAndButton;
    public UIChooseChapter uiChooseChpater;

    [SerializeField] private Button playButton = null;

    public int selectedChapter = 1;

    public void Initialize()
    {
        uiUsedPotion.Initialize();
        uiChapterImageAndButton.Initialize();
        uiChooseChpater.Initialize();

        SetPlayButton();

        UpdateChapterInfo();
    }

    private void SetPlayButton()
    {
        playButton.onClick.AddListener(() =>
        {
            // 테스트를 위해 하트 소모 안하게 바꿈.
            // 나중에 밑에 3줄을 지우고 UseHeart 주석을 지워주면 됨.
            //StageManager.Instance.SetChapterData(selectedChapter);
            //StageManager.Instance.Initialize();
            //SceneManager.LoadScene("InGameScene");
            var uiHeart = MainManager.instance.backCanvas.uiTopMenu.uiHeart;
            if (uiHeart.HasHeart())
            {
                UseHeart();
            }
            else
            {
                //MainManager.instance.frontCanvas.uiAskGoToStore.SetText(PurchaseCurrency.Heart);
                //UIManager.Instance.ShowNew(MainManager.instance.frontCanvas.uiAskGoToStore);
                UIManager.Instance.ShowNew(MainManager.instance.frontCanvas.uiHeartRefill);
                MainManager.instance.frontCanvas.uiHeartRefill.uiHeartTimer.TimeUpdate();
            }
        });
    }

    public void UpdateChapterInfo()
    {
        uiChapterTitle.UpdateChapterTitle(selectedChapter);
        uiChapterImageAndButton.UpdateChapterImage(selectedChapter);
        uiBestWave.UpdateText(selectedChapter);
    }

    public void UseHeart()
    {
        MainManager.instance.frontCanvas.ShowEnteringDungeon();

        new LogEventRequest() 
            .SetEventKey("UseHeart")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    bool result = (bool)(response.ScriptData.GetBoolean("Result"));

                    if(result)
                    {
                        StageManager.Instance.SetChapterData(selectedChapter);
                        StageManager.Instance.Initialize();
                        SaveManager.Instance.InitializeInGameData();
                        SaveManager.Instance.SaveInGameData();
                        SceneManager.LoadScene("InGameScene");
                    }
                    else
                    {
                        MainManager.instance.frontCanvas.HideEnteringDungeon();
                        Debug.Log("Error Use Heart!!");
                    }
                }
                else
                {
                    Debug.Log("Error Use Heart"); 
                    Debug.Log(response.Errors.JSON);
                }
            });
    }
}
