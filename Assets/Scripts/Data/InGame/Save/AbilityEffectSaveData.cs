using System;
using System.Collections.Generic;

// 인게임에서 포션, 시나리오로 얻은 능력치 효과를 저장해주는 클래스
[Serializable]
public class AbilityEffectSaveData
{
    // 시나리오에서 데이터를 가져오려면 Chapter, Wave, Scenario의 아이디가 필요하므로 데이터 아이디 리스트를 만듬.
    public List<int> DataIdList { get; set; }
    public AbilityEffectData abilityEffectData { get; set; }
    public int remainingTurn { get; set; }

    public AbilityEffectSaveData() { }

    public AbilityEffectSaveData(List<int> idList, AbilityEffectData data, int remainingTurn)
    {
        DataIdList = idList;
        abilityEffectData = data;
        this.remainingTurn = remainingTurn;
    }
}