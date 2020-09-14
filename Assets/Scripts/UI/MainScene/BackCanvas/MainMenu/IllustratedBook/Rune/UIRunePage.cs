using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRunePage : MonoBehaviour
{
    public UIEquippedRunes uiEquippedRunes;
    public UIRunesOnRunePage uiRunesOnRunePage;
    public UIRuneInfo uiRuneInfo;
    public UIRuneCombination uiRuneCombination;

    [SerializeField] private Image noticeImage = null;
    [SerializeField] private Button changeSortByButton = null;
    [SerializeField] private Text changeSortByText = null;
    [SerializeField] private Button showRuneCombinationButton = null;

    public void Initialize()
    {
        uiRunesOnRunePage.Initialize();
        uiRuneCombination.Initialize();
        uiEquippedRunes.Initialize();
        uiRuneInfo.Initialize();

        SetChangeSortByButton();
        SetShowRuneCombinationButton();
    }

    private void SetShowRuneCombinationButton()
    {
        showRuneCombinationButton.onClick.AddListener(() =>
        {
            uiRuneCombination.Reset();
            UIManager.Instance.ShowNew(uiRuneCombination);
        });
    }

    private void SetChangeSortByButton()
    {
        changeSortByButton.onClick.AddListener(() =>
        {
            uiRunesOnRunePage.ChangeSortBy();
            SetChangeSortByText(uiRunesOnRunePage.currentSortBy);
        });
    }

    private void SetChangeSortByText(SortBy sortBy)
    {
        switch (sortBy)
        {
            case SortBy.None:
                break;
            case SortBy.Grade:
                changeSortByText.text = RuneService.TEXT_OF_SORT_BY_GRADE;
                break;
            case SortBy.Socket:
                changeSortByText.text = RuneService.TEXT_OF_SORT_BY_SOCKET;
                break;
            default:
                break;
        }
    }

    public void ShowNotice()
    {
        noticeImage.gameObject.SetActive(true);
    }

    public void HideNotice()
    {
        noticeImage.gameObject.SetActive(false);
    }

    public void CheckNotify()
    {
        var uiRunePage = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage;

        if (RuneManager.Instance.CanCombination())
        {
            uiRunePage.ShowNotice();
        }
        else
        {
            uiRunePage.HideNotice();
        }
    }
}
