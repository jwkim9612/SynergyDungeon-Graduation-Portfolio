using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAttributeInfo : MonoBehaviour
{
    [SerializeField] private Text attributeText = null;

    public void SetAttributeText(string text)
    {
        attributeText.text = text;
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
