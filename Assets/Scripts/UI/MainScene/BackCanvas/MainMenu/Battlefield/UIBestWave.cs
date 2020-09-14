using UnityEngine;
using UnityEngine.UI;

public class UIBestWave : MonoBehaviour
{
    [SerializeField] private Text bestWaveText = null;

    public void UpdateText(int selectedChapter)
    {
        if (selectedChapter < PlayerDataManager.Instance.playerData.PlayableChapter)
            bestWaveText.text = "챕터 클리어";
        else
        {
            var chapterDataSheet = DataBase.Instance.chapterDataSheet;
            if (chapterDataSheet.TryGetChapterTotalWave(selectedChapter, out var totalWave))
            {
                var playerData = PlayerDataManager.Instance.playerData;
                bestWaveText.text = $"최고 웨이브 : {playerData.TopWave}/{totalWave}";
            }
        }
    }
}
