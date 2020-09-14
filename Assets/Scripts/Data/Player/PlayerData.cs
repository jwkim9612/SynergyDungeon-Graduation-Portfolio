using System;

[Serializable]
public class PlayerData
{
    public int Level { get; set; }
    public int Exp { get; set; }
    public int Gold { get; set; }
    public int PlayableChapter { get; set; }
    public int Diamond { get; set; }
    public int TopWave { get; set; }

    public void UpdatePlayableChapter()
    {
        var currentPlayableChapter = PlayerDataManager.Instance.playerData.PlayableChapter;
        var currentChapter = StageManager.Instance.currentChapter;

        if (currentChapter == currentPlayableChapter)
        {
            ++PlayableChapter;
            PlayerDataManager.Instance.SavePlayerData();
        }
    }

    public void InitializeTopWave()
    {
        TopWave = 0;
    }
}