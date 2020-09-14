using UnityEngine;
using UnityEngine.UI;

public class UIChapterImageAndButton : MonoBehaviour
{
    [SerializeField] private Image chapterImage = null;
    [SerializeField] private Button chapterButton = null;

    public void Initialize()
    {
        SetChapterButton();
    }

    public void SetChapterButton()
    {
        chapterButton.onClick.AddListener(() =>
        {
            var uiChooseChapter = MainManager.instance.backCanvas.uiMainMenu.uiBattlefield.uiChooseChpater;
            UIManager.Instance.ShowNew(uiChooseChapter);
        });
    }

    public void UpdateChapterImage(int selectedChapter)
    {
        var chapterDataSheet = DataBase.Instance.chapterDataSheet;
        if (chapterDataSheet.TryGetChapterImage(selectedChapter, out var sprite))
        {
            chapterImage.sprite = sprite;
        }
    }
}
