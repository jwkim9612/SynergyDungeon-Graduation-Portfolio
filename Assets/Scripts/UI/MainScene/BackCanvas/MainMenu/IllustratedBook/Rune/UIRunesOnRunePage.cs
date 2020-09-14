using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIRunesOnRunePage : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup girdLayoutGroup = null;
    [SerializeField] private UIRuneOnRunePage uiRuneOnRunePage = null;
    public List<UIRuneOnRunePage> uiRuneListOnRunePage { get; set; }
    public SortBy currentSortBy;

    public void Initialize()
    {
        currentSortBy = RuneService.DEFAULT_SORT_BY;

        RuneManager.Instance.OnAddRune += AddUIRune;
        GenerateOwnedRuneList();
    }

    private void GenerateOwnedRuneList()
    {
        uiRuneListOnRunePage = new List<UIRuneOnRunePage>();

        var ownedRuneListById = RuneManager.Instance.ownedRuneListById;
        if (ownedRuneListById == null)
            return;

        foreach (var ownedRune in ownedRuneListById)
        {
            for(int i = 0; i < ownedRune.Value; ++i)
            {
                var runeDataSheet = DataBase.Instance.runeDataSheet;
                if(runeDataSheet.TryGetRuneData(ownedRune.Key, out var runeData))
                {
                    var rune = Instantiate(uiRuneOnRunePage, girdLayoutGroup.transform);
                    rune.Initialize();
                    rune.SetUIRune(runeData);
                    uiRuneListOnRunePage.Add(rune);
                }
            }
        }

        MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.CheckNotify();
        Sort();
    }

    public void AddUIRune(int runeId)
    {
        var runeDataSheet = DataBase.Instance.runeDataSheet;
        if (runeDataSheet.TryGetRuneData(runeId, out var runeData))
        {
            AddUIRune(runeData);
        }

        MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.CheckNotify();
    }

    public void AddUIRune(RuneData runeData)
    {
        UIRuneOnRunePage uiRune = null;

        // 오브젝트풀링
        foreach(var uiRuneOnRunePage in uiRuneListOnRunePage)
        {
            if(uiRuneOnRunePage.gameObject.activeSelf == false)
            {
                uiRune = uiRuneOnRunePage;
                uiRune.SetUIRune(runeData);
                uiRune.gameObject.SetActive(true);
                Sort();
                return;
            }
        }

        // inactive상태인 룬이 없다면
        uiRune = Instantiate(uiRuneOnRunePage, girdLayoutGroup.transform);
        uiRune.Initialize();
        uiRune.SetUIRune(runeData);
        uiRuneListOnRunePage.Add(uiRune);

        Sort();
    }

    public void RemoveRune(int runeId)
    {
        var uiRune = uiRuneListOnRunePage.Find(x => x.rune.runeData.Id == runeId);
        uiRune.gameObject.SetActive(false);
        //Destroy(uiRune.gameObject);

        MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.CheckNotify();
        //RemoveRune(uiRune);
    }

    public void Sort()
    {
        //StartCoroutine(Co_Sort());

        UpdateRuneList();

        switch (currentSortBy)
        {
            case SortBy.None:
                break;
            case SortBy.Grade:
                uiRuneListOnRunePage = uiRuneListOnRunePage.OrderByDescending(x => x.rune.runeData.Grade).ThenBy(x => x.rune.runeData.Id).ToList();
                break;
            case SortBy.Socket:
                uiRuneListOnRunePage = uiRuneListOnRunePage.OrderBy(x => x.rune.runeData.Id).ToList();
                break;
            default:
                Debug.Log("Error Sort");
                break;
        }

        for (int i = 0; i < uiRuneListOnRunePage.Count; ++i)
        {
            uiRuneListOnRunePage[i].transform.SetSiblingIndex(i);
        }
    }

    public void ChangeSortBy()
    {
        if(currentSortBy == SortBy.Socket)
        {
            currentSortBy = SortBy.Grade;
        }
        else if(currentSortBy == SortBy.Grade)
        {
            currentSortBy = SortBy.Socket;
        }

        Sort();
    }

    public void UpdateRuneList()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (i == uiRuneListOnRunePage.Count)
            {
                uiRuneListOnRunePage.Add(null);
            }

            var uiRune = gameObject.GetComponentsInChildren<UIRuneOnRunePage>(true)[i];

            if (uiRune != uiRuneListOnRunePage[i])
            {
                uiRuneListOnRunePage[i] = uiRune;
            }
        }

        uiRuneListOnRunePage.RemoveRange(transform.childCount, uiRuneListOnRunePage.Count - transform.childCount);
    }

    // 오브젝트가 파괴되는데 시간이 걸려 제대로 업데이트가 안되어서 코루틴을 이용해 한프레임 대기 후 정렬
    //private IEnumerator Co_Sort()
    //{
    //    yield return new WaitForEndOfFrame();

    //    UpdateRuneList();

    //    switch (currentSortBy)
    //    {
    //        case SortBy.None:
    //            break;
    //        case SortBy.Grade:
    //            uiRuneListOnRunePage = uiRuneListOnRunePage.OrderByDescending(x => x.rune.runeData.Grade).ThenBy(x => x.rune.runeData.Id).ToList();
    //            break;
    //        case SortBy.Socket:
    //            uiRuneListOnRunePage = uiRuneListOnRunePage.OrderBy(x => x.rune.runeData.Id).ToList();
    //            break;
    //        default:
    //            Debug.Log("Error Sort");
    //            break;
    //    }

    //    for (int i = 0; i < uiRuneListOnRunePage.Count; ++i)
    //    {
    //        uiRuneListOnRunePage[i].transform.SetSiblingIndex(i);
    //    }

    //    Debug.Log("size = " + uiRuneListOnRunePage.Count);

    //}

    private void OnDestroy()
    {
        if (RuneManager.IsAlive)
        {
            RuneManager.Instance.OnAddRune -= AddUIRune;
        }
    }
}
