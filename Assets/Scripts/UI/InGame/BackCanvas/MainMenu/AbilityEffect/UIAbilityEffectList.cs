using GameSparks.Api.Requests;
using System.Collections.Generic;
using UnityEngine;

public class UIAbilityEffectList : MonoBehaviour
{
    [SerializeField] private UIAbilityEffect uiAbilityEffect = null;
    [SerializeField] private UIAbilityEffectInfo uiAbilityEffectInfo = null;
    public List<UIAbilityEffect> uiAbilityEffectList;
    private bool isInBattle;

    [SerializeField] private Camera cam = null;

    public void Initialize()
    {
        uiAbilityEffectInfo.Initialize();

        isInBattle = false;

        InGameManager.instance.gameState.OnComplete += UpdateAbilityEffectListByWaveComplete;
        InGameManager.instance.gameState.OnBattle += MoveForBattle;
        InGameManager.instance.gameState.OnPrepare += MoveForPrepare;

        uiAbilityEffectList = new List<UIAbilityEffect>();

        SetData();
    }

    private void SetData()
    {
        if (SaveManager.Instance.IsLoadedData)
        {
            InitializeByInGameSaveData(SaveManager.Instance.inGameSaveData.AbilityEffectSaveDataList);
        }
        else
        {
            SetPotion();
        }
    }

    // 사용한 포션이 있으면 적용시켜주는 함수 
    private void SetPotion()
    {
        if (PotionManager.Instance.HasPotionInUse())
        {
            if (DataBase.Instance.potionDataSheet.TryGetPotionData(PotionManager.Instance.potionIdInUse, out var potionData))
            {
                AddAbilityEffect(potionData);
                PotionManager.Instance.RemovePotionData();
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!TransformService.ContainPos(transform as RectTransform, Input.mousePosition, cam))
            {
                if (uiAbilityEffectInfo.gameObject.activeSelf)
                {
                    uiAbilityEffectInfo.OnHide();
                }
            }
        }
    }

    private void MoveForBattle()
    {
        if(!isInBattle)
        {
            RectTransform rect = transform as RectTransform;
            rect.Translate(new Vector3(0.0f, AbilityEffectService.Y_INCREASE_VALUE_FOR_BATTLE, 0.0f));
            isInBattle = true;
        }
    }

    private void MoveForPrepare()
    {
        if(isInBattle)
        {
            RectTransform rect = transform as RectTransform;
            rect.Translate(new Vector3(0.0f, AbilityEffectService.Y_INCREASE_VALUE_FOR_PREPARE, 0.0f));
            isInBattle = false;
        }
    }

    public void InitializeByInGameSaveData(List<AbilityEffectSaveData> abilityEffectSaveDataList)
    {
        foreach (var abilityEffectSaveData in abilityEffectSaveDataList)
        {
            switch (abilityEffectSaveData.abilityEffectData)
            {
                case AbilityEffectData.Potion:
                    if(DataBase.Instance.potionDataSheet.TryGetPotionData(abilityEffectSaveData.DataIdList[0], out var potionData))
                    {
                        AddAbilityEffect(potionData);
                    }
                    break;
                default:
                    Debug.Log("Error InitializeByInGameSaveData");
                    break;
            }
        }

        UpdateAbilityEffectList();
    }

    /// <summary>
    /// 인게임 세이브 데이터에 필요한 AbilityEffect 저장 데이터를 반환
    /// </summary>
    /// <returns>AbilityEffect 인게임 저장 데이터</returns>
    public List<AbilityEffectSaveData> GetSaveData()
    {
        List<AbilityEffectSaveData> abilityEffectSaveDataList = new List<AbilityEffectSaveData>();

        foreach (var uiabilityEffect in uiAbilityEffectList)
        {
            var dataIdList = uiabilityEffect.abilityEffect.dataIdList;
            var abilityEffectData = uiabilityEffect.abilityEffect.abilityEffectData;
            var remainingTurn = uiabilityEffect.abilityEffect.remainingTurn;

            var abilityEffectSaveData = new AbilityEffectSaveData(dataIdList, abilityEffectData, remainingTurn);
            abilityEffectSaveDataList.Add(abilityEffectSaveData);
        }

        return abilityEffectSaveDataList;
    }

    public void AddAbilityEffect(PotionData potionData)
    {
        var uiabilityEffect = Instantiate(this.uiAbilityEffect, transform);
        uiabilityEffect.Initialize();
        uiabilityEffect.SetabilityEffect(potionData);
        uiabilityEffect.OnShow();

        uiAbilityEffectList.Add(uiabilityEffect);
    }

    // 효과들의 남은 턴을 하나씩 줄이고 남은 턴이 없는 효과들은 삭제
    private void UpdateAbilityEffectListByWaveComplete()
    {
        List<int> removeIndexList = new List<int>();

        for (int i = 0; i < uiAbilityEffectList.Count; i++)
        {
            uiAbilityEffectList[i].UpdateAbilityEffectByWaveComplete();
            if(uiAbilityEffectList[i].IsOver())
            {
                RemoveAbilityEffectSaveData(i);
                removeIndexList.Add(i);
            }
        }

        // list의 앞에서부터 삭제하면 앞으로 땡겨지기때문에 문제가 발생한다.
        // 따라서 Reverse 함수로 역순으로 바꿔준 후 삭제.
        removeIndexList.Reverse();

        for (int i = 0; i < removeIndexList.Count; i++)
        {
            Destroy(uiAbilityEffectList[removeIndexList[i]].gameObject);
            uiAbilityEffectList.RemoveAt(removeIndexList[i]);
        }
    }

    // 리스트에 있는 모든 효과들을 업데이트(남은 턴)
    private void UpdateAbilityEffectList()
    {
        foreach (var uiAbilityEffect in uiAbilityEffectList)
        {
            uiAbilityEffect.UpdateAbilityEffect();
        }
    }

    // 해당 인덱스에 있는 효과를 삭제(인게임 세이브 데이터)
    private void RemoveAbilityEffectSaveData(int index)
    {
        new LogEventRequest()
            .SetEventKey("RemoveAbilityEffectSaveData")
            .SetEventAttribute("Index", index)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success RemoveAbilityEffectSaveData!");
                }
                else
                {
                    Debug.Log("Error RemoveAbilityEffectSaveData!");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }
}
