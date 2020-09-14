using GameSparks.Api.Requests;
using GameSparks.Core;
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoSingleton<PotionManager>
{
    public delegate void OnPotionChangedDelegate();
    public OnPotionChangedDelegate OnPotionChanged { get; set; }

    public int potionIdInUse;

    public void Initialize()
    {
        LoadPotionData();
    }

    public void RemovePotionData()
    {
        InitializePotionData();
    }

    public void LoadPotionData()
    {
        new LogEventRequest()
            .SetEventKey("LoadPotionData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    bool result = (bool)response.ScriptData.GetBoolean("Result");
                    if (result)
                    {
                        potionIdInUse = (int)response.ScriptData.GetInt("PotionIdInUse");
                    }
                    else
                    {
                        Debug.Log("Load Potion Data Result is False!!");
                        InitializePotionData();
                    }
                }
                else
                {
                    Debug.Log("Error LoadPotionData");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    private void InitializePotionData()
    {
        new LogEventRequest()
            .SetEventKey("InitializePotionData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success Initialize PotionData !");
                    LoadPotionData();
                }
                else
                {
                    Debug.Log("Error Initialize PotionData !");
                }
            });
    }

    public void SetPotion(int potionId)
    {
        new LogEventRequest()
            .SetEventKey("SetPotionData")
            .SetEventAttribute("PotionId", potionId)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    potionIdInUse = potionId;
                    OnPotionChanged();
                }
                else
                {
                    Debug.Log("Error SetPotion PotionData !");
                }
        });
    }

    public AbilityEffectSaveData GetAbilityEffectSaveData()
    {
        AbilityEffectSaveData abilityEffectSaveData = new AbilityEffectSaveData();
        abilityEffectSaveData.DataIdList = new List<int>() { potionIdInUse };
        abilityEffectSaveData.abilityEffectData = AbilityEffectData.Potion;
        abilityEffectSaveData.remainingTurn = AbilityEffectService.NUM_OF_INFINITY;

        return abilityEffectSaveData;
    }

    public bool HasPotionInUse()
    {
        if(potionIdInUse == -1)
            return false;

        return true;
    }
}
