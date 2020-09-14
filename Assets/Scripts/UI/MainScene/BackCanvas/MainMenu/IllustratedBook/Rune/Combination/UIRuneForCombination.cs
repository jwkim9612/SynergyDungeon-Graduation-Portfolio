using UnityEngine;
using UnityEngine.UI;

public class UIRuneForCombination : UIRune
{
    [SerializeField] private Button button = null;
    [SerializeField] private GameObject equippedImage = null;
    [SerializeField] private Image lockImage = null;
    [SerializeField] private Image selectedImage = null;
    [SerializeField] private Text gradeText = null;

    public bool isSelected;

    public void Initialize()
    {
        SetButtonForUnselected();
        OnShow();

        isSelected = false;
    }

    public override void SetUIRune(RuneData newRuneData)
    {
        base.SetUIRune(newRuneData);

        gradeText.text = RuneService.GetNameStrByGrade(newRuneData.Grade);
    }

    public void ShowEquipped()
    {
        isEquippedRune = true;

        equippedImage.SetActive(true);
    }

    public void HideEquipped()
    {
        isEquippedRune = false;

        equippedImage.SetActive(false);
    }

    public void SetSelected()
    {
        isSelected = true;
        selectedImage.gameObject.SetActive(true);
    }

    public void SetUnselected()
    {
        isSelected = false;
        selectedImage.gameObject.SetActive(false);
    }

    public void Lock()
    {
        button.interactable = false;
        lockImage.gameObject.SetActive(true);
    }

    public void Unlock()
    {
        button.interactable = true;
        lockImage.gameObject.SetActive(false);
    }

    public void SetButtonForUnselected()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            var uiCombinationSpace = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRuneCombination.uiCombinationSpace;
            var uiSelectedRuneSpace = uiCombinationSpace.AddSelectedRune(rune.runeData, this);

            if(uiSelectedRuneSpace == null)
            {
                Debug.Log("꽉참용");
                return;
            }
            
            SetSelected();
            SetButtonForSelected(uiSelectedRuneSpace);
        });
    }
    public void SetButtonForSelected(UISelectedRuneSpace uiSelectedRuneSpace)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            Release(uiSelectedRuneSpace);
        });
    }

    public void Release(UISelectedRuneSpace uiSelectedRuneSpace)
    {
        SetUnselected();
        uiSelectedRuneSpace.Reset();

        SetButtonForUnselected();

        var uiRuneCombination = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRuneCombination;
        uiRuneCombination.HideCombinationButton();

        var uiCombinationSpace = uiRuneCombination.uiCombinationSpace;
        var uiRunesForCombination = uiRuneCombination.uiRunesForCombination;
        if(uiCombinationSpace.IsEmpty())
        {
            uiRunesForCombination.AllUnlock();
            uiCombinationSpace.isActive = false;
            uiCombinationSpace.OnHide();
        }
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }
}
