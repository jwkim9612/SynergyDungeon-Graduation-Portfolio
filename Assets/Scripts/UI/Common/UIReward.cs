using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReward : MonoBehaviour
{
    [SerializeField] private Image rewardImage = null;
    [SerializeField] private Text rewardAmount = null;
    
    public void SetImage(Sprite image)
    {
        rewardImage.sprite = image;
    }

    public void SetAmountText(string text)
    {
        rewardAmount.text = text;
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
