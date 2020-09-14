using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICoin : MonoBehaviour
{
    [SerializeField] private Text coinText = null;
    [SerializeField] private List<Image> bonusImageList = null;

    public void Initialize()
    {
        UpdateCoinText();
        UpdateBonusImageList();

        InGameManager.instance.playerState.OnCoinChanged += UpdateCoinText;
        InGameManager.instance.playerState.OnBonusCoinChanged += UpdateBonusImageList;
    }

    public void UpdateCoinText()
    {
        coinText.text = InGameManager.instance.playerState.coin.ToString();
    }

    public void UpdateBonusImageList()
    {
        int bonusCoin = InGameManager.instance.playerState.currentBonusCoin;

        int index;
        for(index = 0; index < bonusCoin; ++index)
        {
            bonusImageList[index].gameObject.SetActive(true);
        }

        for(; index < bonusImageList.Count; ++index)
        {
            bonusImageList[index].gameObject.SetActive(false);
        }
    }
}
