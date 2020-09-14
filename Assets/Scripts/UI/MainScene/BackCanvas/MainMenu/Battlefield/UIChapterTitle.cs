using UnityEngine;
using UnityEngine.UI;

public class UIChapterTitle : MonoBehaviour
{
    [SerializeField] private Text chapterTitle = null;

    public void UpdateChapterTitle(int selectedChapter)
    {
        var chapterDataSheet = DataBase.Instance.chapterDataSheet;
        if (!chapterDataSheet.TryGetChapterId(selectedChapter, out var id))
        {
            return;
        }
        if (!chapterDataSheet.TryGetChapterName(selectedChapter, out var title))
        {
            return;
        }

        chapterTitle.text = id + ". " + title;
    }
}
