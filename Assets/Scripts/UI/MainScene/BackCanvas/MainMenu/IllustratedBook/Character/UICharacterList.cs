using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterList : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup girdLayoutGroup = null;
    [SerializeField] private UICharacterSlot characterSlot = null;

    private List<UICharacterSlot> characterSlots { get; set; }

    public Tier currentTier { get; set; } = Tier.None;
    public Tribe currentTribe { get; set; } = Tribe.None;
    public Origin currentOrigin { get; set; } = Origin.None;

    public void Initialize()
    {
        CreateCharacterList();
        Destroy(characterSlot.gameObject);
    }

    private void CreateCharacterList()
    {
        characterSlots = new List<UICharacterSlot>();

        var characterDataSheet = DataBase.Instance.characterDataSheet;
        if (characterDataSheet == null)
        {
            Debug.LogError("Error characterDataSheet is null");
        }

        if (characterDataSheet.TryGetCharacterDatas(out var characterDatas))
        {
            foreach (var characterData in characterDatas)
            {
                var slot = Instantiate(characterSlot, girdLayoutGroup.transform);
                slot.SetCharacterData(characterData.Value);
                characterSlots.Add(slot);
            }
        }

        characterSlots = characterSlots.OrderBy(x => x.characterData.Tier).ToList();
        for(int i = 0; i < characterSlots.Count; ++i)
        {
            characterSlots[i].gameObject.transform.SetSiblingIndex(i);
        }
    }

    public void Sort()
    {
        foreach (var characterSlot in characterSlots)
        {
            // 현재 정렬값이 캐릭터의 정렬값과 같지 않거나, 모든(None)값이 아니면
            if (!(characterSlot.characterData.Tier == currentTier || Tier.None == currentTier))
            {
                characterSlot.gameObject.SetActive(false);
                continue;
            }

            if (!(characterSlot.characterData.Tribe == currentTribe || Tribe.None == currentTribe))
            {
                characterSlot.gameObject.SetActive(false);
                continue;
            }

            if (!(characterSlot.characterData.Origin == currentOrigin || Origin.None == currentOrigin))
            {
                characterSlot.gameObject.SetActive(false);
                continue;
            }

            characterSlot.gameObject.SetActive(true);
        }
    }
}
