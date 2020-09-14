using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    public int currentChapter = 1;
    public int currentWave = 1;
    public int currentStage = 1;
    public ChapterData currentChapterData = null;

    public void Initialize()
    {
        if (SaveManager.Instance.IsLoadedData)
        {

        }
        else
        {
            currentWave = 1;
        }
    }

    public void SetChapterData(int chapter)
    {
        currentChapter = chapter;
        DataBase.Instance.chapterDataSheet.TryGetChapterData(currentChapter, out currentChapterData);
    }

    public void SetCurrentWaveAndSetCurrentStage(int wave)
    {
        currentWave = wave;
        SetCurrentStageByCurrentWave(currentWave);
    }

    public void SetCurrentStageByCurrentWave(int wave)
    {
        if (wave <= 3)
        {
            currentStage = 1;
        }
        else
        {
            currentStage = (wave + 1) / 5;
        }
    }

    public float GetRelativePercentageByStage()
    {
        float totalWave = currentChapterData.TotalWave;

        return currentWave / totalWave;
    }

    public void IncreaseWaveAndSetCurrentStage(int increaseValue)
    {
        currentWave = Mathf.Clamp(currentWave + increaseValue, 1, (currentChapterData.TotalWave));
        SetCurrentStageByCurrentWave(currentWave);
    }

    public bool IsFinalWave()
    {
        return currentWave == currentChapterData.TotalWave;
    }

}
