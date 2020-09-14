using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEndStage : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton = null;
    [SerializeField] private Text levelText = null;
    [SerializeField] private Slider expSlider = null;
    [SerializeField] private List<UIReward> uiRewardList = null;

    public void Initialize()
    {
        SetBackToMenuButton();

        uiRewardList = GetComponentsInChildren<UIReward>().ToList();
    }

    private void SetBackToMenuButton()
    {
        backToMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });

    }

    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }

    public void SetExp(int level, int exp)
    {
        if(DataBase.Instance.playerExpDataSheet.TryGetSatisfyExp(level, out var satisfyExp))
        {
            expSlider.value = (float)exp / satisfyExp;
        }
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }

    public void SetEndStageScreen()
    {
        var playerData = PlayerDataManager.Instance.playerData;
        var level = playerData.Level;
        var exp = playerData.Exp;

        SetLevel(level);
        SetExp(level, exp);

        var rewardList = InGameManager.instance.playerState.rewardList;

        int index = 0;
        foreach (var reward in rewardList)
        {
            switch (reward.rewardCurrency)
            {
                case RewardCurrency.Gold:
                    uiRewardList[index].SetImage(GoodsService.GOLD_IMAGE);
                    break;
                case RewardCurrency.Exp:    continue;
            }

            uiRewardList[index].SetAmountText(reward.amount.ToString());
            uiRewardList[index].OnShow();

            ++index;

        }

        for (; index < uiRewardList.Count; ++index)
        {
            uiRewardList[index].OnHide();
        }
    }
}
