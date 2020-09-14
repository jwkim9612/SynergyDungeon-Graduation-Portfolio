using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIEquippedRunes : MonoBehaviour
{
    public List<UIEquipRune> uiEquippedRuneList;

    public void Initialize()
    {
        uiEquippedRuneList = GetComponentsInChildren<UIEquipRune>().ToList();

        InitializeEquippedRunes();
    }

    // 로컬에서 장착된 룬 리스트를 가져온 후 소유한 룬에서 하나씩 장착.
    private void InitializeEquippedRunes()
    {
        var uiRunesOnRunePage = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRunesOnRunePage;

        List<int> equippedRuneIds = SaveManager.Instance.equippedRuneIdsSaveData;
        for (int i = 0; i < equippedRuneIds.Count; ++i)
        {
            uiEquippedRuneList[i].Initialize();

            if (equippedRuneIds[i] == -1)
            {
                uiEquippedRuneList[i].Disable();
                continue;
            }

            var runeToBeEquipped = uiRunesOnRunePage.uiRuneListOnRunePage.Find(x => x.rune.runeData.Id == equippedRuneIds[i]);
            if (runeToBeEquipped != null)
            {
                EquipRune(runeToBeEquipped, true);
                runeToBeEquipped.gameObject.SetActive(false);
                //Destroy(runeToBeEquipped.gameObject);
            }
            else
            {
                uiEquippedRuneList[i].Disable();
                Debug.Log("장착되었던 룬을 찾을 수 없습니다.");
            }
        }

        uiRunesOnRunePage.Sort();
    }

    /// <summary>
    /// 룬 장착 함수
    /// </summary>
    /// <param name="runeDataToEquip"> 장착할 룬의 데이터</param>
    /// <returns>교체 되었는지의 Bool값과 교체되었다면 교체된 RuneData, 교체가 안되었다면 null값을 가진 Tuple을 리턴</returns>
    public (bool IsReplaced, Rune EquippedRune) EquipRuneAndGetReplaceResult(UIRuneOnRunePage uiRuneToEquip)
    {
        bool isReplaced;
        Rune equippedRune;

        int socketPositionOfRuneDataToEquip = uiRuneToEquip.rune.runeData.SocketPosition;
        UIEquipRune uiEquipRuneToBeEquip = uiEquippedRuneList[socketPositionOfRuneDataToEquip];

        // 장착할 위치에 룬이 없는경우
        if (uiEquipRuneToBeEquip.rune == null)
        {
            isReplaced = false;
            equippedRune = null;
        }
        else
        {
            isReplaced = true;
            equippedRune = uiEquipRuneToBeEquip.rune;
        }

        EquipRune(uiRuneToEquip);

        return (isReplaced, equippedRune);
    }

    public void EquipRune(UIRuneOnRunePage uiRuneToEquip, bool isInitialize = false)
    {
        var runeData = uiRuneToEquip.rune.runeData;
        int runeId = runeData.Id;
        int socketPositionOfRuneDataToEquip = runeData.SocketPosition;

        uiEquippedRuneList[socketPositionOfRuneDataToEquip].SetUIRune(runeData);
        RuneManager.Instance.SetEquippedRune(uiRuneToEquip.rune);

        var uiRunesForCombination = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRuneCombination.uiRunesForCombination;
        uiRunesForCombination.SetEquipped(runeId);

        //var uiRuneListOnRunePage = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRunesOnRunePage;
        //uiRuneListOnRunePage.RemoveRune(uiRuneToEquip);

        if(!isInitialize)
        {
            SaveManager.Instance.SetEquippedRuneIdsSaveData(socketPositionOfRuneDataToEquip, uiRuneToEquip.rune.runeData.Id);
            SaveManager.Instance.SaveEquippedRuneIds();
        }
    }

    public void RemoveRune(int runeId)
    {
        UIEquipRune uiEquipRune = null;

        foreach (var uiEquippedRune in uiEquippedRuneList)
        {
            if(uiEquippedRune.rune != null)
            {
                if(uiEquippedRune.rune.runeData.Id == runeId)
                {
                    uiEquipRune = uiEquippedRune;
                    break;
                }
            }
        }

        if(uiEquipRune != null)
        {
            SaveManager.Instance.SetEquippedRuneIdsSaveDataByRelease(uiEquipRune.rune.runeData.SocketPosition);
            SaveManager.Instance.SaveEquippedRuneIds();
            uiEquipRune.Disable();

            MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.CheckNotify();
        }
    }
}
