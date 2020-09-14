using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using geniikw.DataSheetLab;

public class UISynergySlot : MonoBehaviour
{
    [SerializeField] private UISynergyInfo synergyInfo = null;
    [SerializeField] private Image synergyImage = null;
    [SerializeField] private Text synergyName = null;

    public TribeData tribeData = null;
    public OriginData originData = null;

    public void SetSynergyData(TribeData newTribeData)
    {
        originData = null;
        tribeData = newTribeData;

        Setimage(tribeData.Image);
        SetName(tribeData.Tribe.ToString());
    }

    public void SetSynergyData(OriginData newOriginData)
    {
        tribeData = null;
        originData = newOriginData;

        Setimage(originData.Image);
        SetName(originData.Origin.ToString());
    }

    public void Setimage(Sprite sprite)
    {
        if(sprite != null)
        {
            synergyImage.sprite = sprite;
        }
        else
        {
            Debug.Log("No Synergy Slot Image");
        }
    }

    public void SetName(string name)
    {
        synergyName.text = name;
    }

    public void OnClicked()
    {
        if(tribeData != null)
        {
            synergyInfo.SetSynergyData(tribeData, synergyName.text, tribeData.Description, tribeData.Image);
        }
        else if(originData != null)
        {
            synergyInfo.SetSynergyData(originData, synergyName.text, originData.Description, originData.Image);
        }
        else
        {
            Debug.Log("No SynergyData");
        }

        UIManager.Instance.ShowNew(synergyInfo);
    }
}
