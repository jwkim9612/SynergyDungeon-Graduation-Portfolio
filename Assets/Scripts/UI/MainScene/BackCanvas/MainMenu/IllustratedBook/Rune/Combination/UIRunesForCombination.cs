using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIRunesForCombination : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayoutGroup = null;
    [SerializeField] private UIRuneForCombination uiRuneForCombination = null;

    public List<UIRuneForCombination> uiRuneListForCombination;
    public List<UIRuneForCombination> uiCanSelectRuneList;
    private RuneDataSheet runeDataSheet;

    public void Initialize()
    {
        runeDataSheet = DataBase.Instance.runeDataSheet;

        GenerateRunes();

        RuneManager.Instance.OnAddRune += AddRune;
    }

    private void GenerateRunes()
    {
        var uiOwnedRuneList = RuneManager.Instance.ownedRuneListById;

        foreach (var uiOwnedRune in uiOwnedRuneList)
        {
            var runeId = uiOwnedRune.Key;
            var runeCount = uiOwnedRune.Value;

            for (int i = 0; i < runeCount; ++i)
            {
                AddRune(runeId);
            }
        }

        Sort();
    }

    public void AddRune(int runeId)
    {
        if (runeDataSheet.TryGetRuneData(runeId, out var runeData))
        {
            // 오브젝트 풀링
            foreach (var uiRuneForCombination in uiRuneListForCombination)
            {
                if(uiRuneForCombination.gameObject.activeSelf == false)
                {
                    uiRuneForCombination.SetUIRune(runeData);
                    uiRuneForCombination.HideEquipped();
                    uiRuneForCombination.gameObject.SetActive(true);
                    Sort();
                    return;
                }
            }

            var uiRune = Instantiate(uiRuneForCombination, gridLayoutGroup.transform);
            uiRune.Initialize();
            uiRune.SetUIRune(runeData);
            uiRune.HideEquipped();
            uiRuneListForCombination.Add(uiRune);
            Sort();
        }
    }

    public void RemoveRune(int runeId, bool IsEquipped)
    {
        UIRuneForCombination uiRune = null;

        if (IsEquipped)
        {
            uiRune = uiRuneListForCombination.Find(x => x.rune.runeData.Id == runeId && x.isEquippedRune) as UIRuneForCombination;
        }
        else
        {
            uiRune = uiRuneListForCombination.Find(x => x.rune.runeData.Id == runeId && !x.isEquippedRune) as UIRuneForCombination;
        }

        if (uiRune != null)
        {
            uiRune.gameObject.SetActive(false);
            //Destroy(uiRune.gameObject);
            //uiRuneListForCombination.Remove(uiRune);
        }

        Sort();
    }

    public void RemoveEquipped(int runeId)
    {
        UIRuneForCombination uiRune = uiRuneListForCombination.Find(x => x.rune.runeData.Id == runeId && x.isEquippedRune) as UIRuneForCombination;
        if (uiRune != null)
        {
            uiRune.HideEquipped();
        }
        else
        {
            Debug.Log("Error RemoveEquipped");
        }
    }

    public void SetEquipped(int runeId)
    {
        UIRuneForCombination uiRune = uiRuneListForCombination.Find(x => x.rune.runeData.Id == runeId) as UIRuneForCombination;
        if (uiRune != null)
        {
            uiRune.ShowEquipped();
        }
        else
        {
            Debug.Log("Error SetEquipped");
        }
    }

    private void Sort()
    {
        UpdateRuneList();

        uiRuneListForCombination = uiRuneListForCombination.OrderByDescending(x => x.rune.runeData.Grade).ThenBy(x => x.rune.runeData.Id).ToList();

        for (int i = 0; i < uiRuneListForCombination.Count; ++i)
        {
            uiRuneListForCombination[i].transform.SetSiblingIndex(i);
        }

        //// 오브젝트가 파괴되는데 시간이 좀 걸려 한 프레임 멈췄다가 실행
        //Invoke("UpdateRuneListAndSort", Time.deltaTime);
    }

    //public void UpdateRuneListAndSort()
    //{
    //    UpdateRuneList();

    //    uiRuneListForCombination = uiRuneListForCombination.OrderByDescending(x => x.rune.runeData.Grade).ThenBy(x => x.rune.runeData.Id).ToList();

    //    for (int i = 0; i < uiRuneListForCombination.Count; ++i)
    //    {
    //        uiRuneListForCombination[i].transform.SetSiblingIndex(i);
    //    }
    //}

    public void UpdateRuneList()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (i == uiRuneListForCombination.Count)
            {
                uiRuneListForCombination.Add(null);
            }

            var uiRune = gameObject.GetComponentsInChildren<UIRuneForCombination>(true)[i];

            if (uiRune != uiRuneListForCombination[i])
            {
                uiRuneListForCombination[i] = uiRune;
            }
        }

        uiRuneListForCombination.RemoveRange(transform.childCount, uiRuneListForCombination.Count - transform.childCount);
    }

    public void LockUpdate(RuneGrade grade)
    {
        uiCanSelectRuneList = new List<UIRuneForCombination>();

        foreach (var uiRuneForCombination in uiRuneListForCombination)
        {
            RuneGrade runeGrade = uiRuneForCombination.rune.runeData.Grade;
            if(runeGrade != grade)
            {
                uiRuneForCombination.Lock();
            }
            else
            {
                uiRuneForCombination.Unlock();
                uiCanSelectRuneList.Add(uiRuneForCombination);
            }
        }
    }

    public void AllUnlock()
    {
        foreach (var uiRuneForCombination in uiRuneListForCombination)
        {
            uiRuneForCombination.Unlock();
        }
    }

    public void Reset()
    {
        AllUnlock();

        foreach (var uiRuneForCombination in uiRuneListForCombination)
        {
            uiRuneForCombination.SetUnselected();
            uiRuneForCombination.SetButtonForUnselected();
        }
    }

    private void OnDestroy()
    {
        if (RuneManager.IsAlive)
        {
            RuneManager.Instance.OnAddRune -= AddRune;
        }
    }
}
