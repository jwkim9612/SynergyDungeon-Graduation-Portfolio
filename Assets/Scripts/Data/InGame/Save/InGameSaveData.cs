using System;
using System.Collections.Generic;

[Serializable]
public class InGameSaveData
{
    public int Chapter { get; set; }
    public int Wave { get; set; }
    public int Coin { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    public List<CharacterInfo> CharacterAreaInfoList { get; set; }
    public List<CharacterInfo> PrepareAreaInfoList { get; set; }
    public List<AbilityEffectSaveData> AbilityEffectSaveDataList { get; set; }
    public int EventProbability { get; set; }
}
