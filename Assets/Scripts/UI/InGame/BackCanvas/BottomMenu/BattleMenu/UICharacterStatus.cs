using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterStatus : MonoBehaviour
{
    [SerializeField] private Image characterIcon = null;
    [SerializeField] private Text characterName = null;
    [SerializeField] private HorizontalLayoutGroup starGrade = null;
    [SerializeField] private UIStatusHPBar uiStatusHPbar = null;
    private List<Image> starList = null;
    private Character ControllingPawn { get; set; }
    
    public void Initialize()
    {
        uiStatusHPbar.Initialize();

        SetStarList();
    }

    private void SetStarList()
    {
        starList = starGrade.GetComponentsInChildren<Image>().ToList();
    }

    public void SetCharacterStatus(Character character)
    {
        if (character == null)
        {
            Debug.Log("Error SetCharacterStatus");
            return;
        }

        if(character.characterInfo == null)
        {
            Debug.Log($"{character} characterInfo is null!!");
            return;
        }

        ShowAll();

        var characterDataSheet = DataBase.Instance.characterDataSheet;
        if(characterDataSheet.TryGetCharacterData(character.characterInfo.id, out var data))
        {
            SetCharacterName(data.Name);

            if(data.HeadImage == null)
            {
                SetCharacterIcon(data.Image);
            }
            else
            {
                SetCharacterIcon(data.HeadImage);
            }
        }

        SetStarGrade(character.characterInfo.star);
        ControllingPawn = character;
        uiStatusHPbar.SetControllingPawn(ControllingPawn);
    }

    private void SetCharacterIcon(Sprite sprite)
    {
        if (sprite == null)
            return;

        characterIcon.sprite = sprite;
    }

    private void SetCharacterName(string name)
    {
        if (name == "")
            return;

        characterName.text = name;
    }

    private void SetStarGrade(int star)
    {
        for (int i = 0; i < starList.Count; ++i)
        {
            if (i < star)
            {
                starList[i].gameObject.SetActive(true);
            }
            else
            {
                starList[i].gameObject.SetActive(false);
            }
        }
    }

    public void ShowAll()
    {
        characterIcon.gameObject.SetActive(true);
        starGrade.gameObject.SetActive(true);
        uiStatusHPbar.gameObject.SetActive(true);
        characterName.gameObject.SetActive(true);
    }

    public void HideAll()
    {
        characterIcon.gameObject.SetActive(false);
        starGrade.gameObject.SetActive(false);
        uiStatusHPbar.gameObject.SetActive(false);
        characterName.gameObject.SetActive(false);
    }
}
