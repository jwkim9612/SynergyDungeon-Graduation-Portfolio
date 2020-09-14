using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPotionInfo : MonoBehaviour
{
    [SerializeField] private Text descriptionText = null;

    public void SetDescriptionText(PotionData potionData)
    {
        descriptionText.text = $"{potionData.Name} : {potionData.Description}";
    }

    public void SetDescriptionText(string text)
    {
        descriptionText.text = text;
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
