using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UICombinationSpace : MonoBehaviour
{
    public List<UISelectedRuneSpace> uiSelectedRuneSpaceList;

    public bool isActive;
    public RuneGrade combinationGrade;

    public void Initialize()
    {
        uiSelectedRuneSpaceList = GetComponentsInChildren<UISelectedRuneSpace>(true).ToList();

        isActive = false;
    }

    public UISelectedRuneSpace AddSelectedRune(RuneData runeData, UIRuneForCombination uiRuneForCombination)
    {
        if(!isActive)
        {
            combinationGrade = runeData.Grade;
            UpdateRuneCombinableNum(runeData.Grade);
            OnShow();

            var uiRunesForCombination = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRuneCombination.uiRunesForCombination;
            uiRunesForCombination.LockUpdate(runeData.Grade);
        }

        foreach (var uiSelectedRuneSpace in uiSelectedRuneSpaceList)
        {
            if (!uiSelectedRuneSpace.gameObject.activeSelf)
                continue;

            if (uiSelectedRuneSpace.uiSelectedRune. rune != null)
                continue;

            uiSelectedRuneSpace.SetUISelectRune(runeData, uiRuneForCombination);

            if(IsFull())
            {
                MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRuneCombination.ShowCombinationButton();
            }

            return uiSelectedRuneSpace;
        }

        return null;
    }

    public void UpdateRuneCombinableNum(RuneGrade grade)
    {
        isActive = true;

        var runeCombinableNumDataSheet = DataBase.Instance.runeCombinableNumDataSheet;
        if (runeCombinableNumDataSheet.TryGetRuneCombinableNum(grade, out var runeCombinableNum))
        {
            int numIndex = 0;

            foreach(var uiSelectedRuneSpace in uiSelectedRuneSpaceList)
            {
                uiSelectedRuneSpace.Reset();

                if(numIndex >= runeCombinableNum)
                {
                    uiSelectedRuneSpace.OnHide();
                }
                else
                {
                    uiSelectedRuneSpace.OnShow();
                }

                ++numIndex;
            }
        }   
    }

    public bool IsEmpty()
    {
        foreach (var uiSelectedRuneSpace in uiSelectedRuneSpaceList)
        {
            if (!uiSelectedRuneSpace.gameObject.activeSelf)
                continue;

            if (uiSelectedRuneSpace.uiSelectedRune.rune != null)
                return false;
        }

        return true;
    }

    public bool IsFull()
    {
        foreach (var uiSelectedRuneSpace in uiSelectedRuneSpaceList)
        {
            if (!uiSelectedRuneSpace.gameObject.activeSelf)
                continue;

            if (uiSelectedRuneSpace.uiSelectedRune.rune == null)
                return false;
        }

        return true;
    }

    public void Reset()
    {
        foreach (var uiSelectedRuneSpace in uiSelectedRuneSpaceList)
        {
            uiSelectedRuneSpace.Reset();
        }

        OnHide();
        isActive = false;
    }

    public List<(int runeId, bool IsEquipped)> GetRuneIdAndIsEquippedList()
    {
        List<(int, bool)> runeIdAndIsEquippedList = new List<(int, bool)>();

        foreach (var uiSelectedRuneSpace in uiSelectedRuneSpaceList)
        {
            if (!uiSelectedRuneSpace.gameObject.activeSelf)
                continue;

            if (uiSelectedRuneSpace.uiSelectedRune.rune == null)
                continue;

            bool isEquippedRune = uiSelectedRuneSpace.uiSelectedRune.isEquippedRune;
            int runeId = uiSelectedRuneSpace.uiSelectedRune.rune.runeData.Id;

            runeIdAndIsEquippedList.Add((runeId, isEquippedRune));
        }

        return runeIdAndIsEquippedList;
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}
