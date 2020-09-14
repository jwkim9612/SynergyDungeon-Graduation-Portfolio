using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;
using UnityEngine.UI;

public class UIChooseChapter : UIControl
{
    [SerializeField] private SimpleScrollSnap simpleScrollSnap = null;
    [SerializeField] private GameObject content = null;
    [SerializeField] private UIChapter uiChapter = null;
    [SerializeField] private Text chapterTitle = null;
    [SerializeField] private Button entranceButton = null;
    [SerializeField] private Button backButton = null;

    public void Initialize()
    {
        CreateChapterList();

        SetEntranceButton();
        SetBackButton();
        SetScrollSnapOnPanelChanged();
    }

    private void SetScrollSnapOnPanelChanged()
    {
        simpleScrollSnap.onPanelChanged.AddListener(() =>
        {
            var chapterDataSheet = DataBase.Instance.chapterDataSheet;
            if(chapterDataSheet.TryGetChapterData(simpleScrollSnap.TargetPanel + 1, out var chapterData))
            {
                chapterTitle.text = chapterData.Id + ". " + chapterData.Name;
            }

            if (IsPlayableChapter(simpleScrollSnap.TargetPanel + 1))
            {
                entranceButton.interactable = true;
            }
            else
            {
                entranceButton.interactable = false;
            }
        });
    }

    private void SetEntranceButton()
    {
        entranceButton.onClick.AddListener(() =>
        {
            var uiBattlefield = MainManager.instance.backCanvas.uiMainMenu.uiBattlefield;
            uiBattlefield.selectedChapter = simpleScrollSnap.CurrentPanel + 1;
            uiBattlefield.UpdateChapterInfo();
        });
    }

    private void SetBackButton()
    {
        backButton.onClick.AddListener(() =>
        {
            var uiBattlefield = MainManager.instance.backCanvas.uiMainMenu.uiBattlefield;
            simpleScrollSnap.GoToPanel(uiBattlefield.selectedChapter - 1);
        });
    }

    private void CreateChapterList()
    {
        int dataIndex = 0;

        var chapterDataSheet = DataBase.Instance.chapterDataSheet;
        if (chapterDataSheet.TryGetChapterDatas(out var chapterDatas))
        {
            foreach (var chapterData in chapterDatas)
            {
                if (dataIndex == 0)
                {
                    uiChapter.SetChapterData(chapterData.Value);
                }
                else
                {
                    var chapter = Instantiate(uiChapter, content.transform);
                    chapter.SetChapterData(chapterData.Value);
                    if (!IsPlayableChapter(dataIndex + 1))
                    {
                        chapter.ToBlurry();
                    }
                }

                ++dataIndex;
            }
        }
        
    }

    private bool IsPlayableChapter(int chapter)
    {
        return PlayerDataManager.Instance.playerData.PlayableChapter >= chapter ? true : false;
    }
}
