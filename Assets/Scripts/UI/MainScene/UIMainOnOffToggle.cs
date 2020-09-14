using UnityEngine;
using UnityEngine.UI;

public class UIMainOnOffToggle : MonoBehaviour
{
    public GameObject clickedImage;
    public GameObject unclickedImage;
    
    public void OnChangeValue()
    {
        bool onoffSwitch = gameObject.GetComponent<Toggle>().isOn;
        if(onoffSwitch)
        {
            clickedImage.SetActive(true);
            unclickedImage.SetActive(false);
        }
        else
        {
            clickedImage.SetActive(false);
            unclickedImage.SetActive(true);
        }
    }

    void Start()
    {
        OnChangeValue();
    }
}
