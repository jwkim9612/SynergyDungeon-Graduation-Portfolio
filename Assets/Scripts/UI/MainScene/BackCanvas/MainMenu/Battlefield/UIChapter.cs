using UnityEngine;
using UnityEngine.UI;

public class UIChapter : MonoBehaviour
{
    [SerializeField] private Image chapterImage = null;

    public ChapterData chapterData { get; set; }

    public void SetChapterData(ChapterData newChapterData)
    {
        chapterData = newChapterData;

        SetImage(chapterData.Image);
    }

    public void SetImage(Sprite sprite)
    {
        if (sprite != null)
        {
            chapterImage.sprite = sprite;
        }
        else
        {
            Debug.Log("No Image");
        }
    }

    public void ToBlurry()
    {
        chapterImage.color = new Color(chapterImage.color.r, chapterImage.color.g, chapterImage.color.g, InGameService.SIZE_TO_BLUR);
    }
}
