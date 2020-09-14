using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISynergyList : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup girdLayoutGroup = null;
    [SerializeField] private UISynergySlot synergySlot = null;
    [SerializeField] private UISynergyInfo synergyInfo = null;

    void Start()
    {
        CreateSynergyList();
        Destroy(synergySlot.gameObject);

        synergyInfo.Initialize();
    }

    private void CreateSynergyList()
    {
        CreateTribeList();
        CreateOriginList();
    }

    private void CreateTribeList()
    {
        var tribeDataSheet = DataBase.Instance.tribeDataSheet;
        if (tribeDataSheet == null)
        {
            Debug.LogError("Error tribeDataSheet is null");
            return;
        }

        if(tribeDataSheet.TryGetTribeDatas(out var tribeDatas))
        {
            foreach (var tribeData in tribeDatas)
            {
                var slot = Instantiate(synergySlot, girdLayoutGroup.transform);
                slot.SetSynergyData(tribeData.Value);
            }
        }
    }

    private void CreateOriginList()
    {
        var originDataSheet = DataBase.Instance.originDataSheet;
        if(originDataSheet == null)
        {
            Debug.LogError("Error originDataSheet is null");
            return;
        }

        if (originDataSheet.TryGetOriginDatas(out var originDatas))
        {
            foreach (var originData in originDatas)
            {
                var slot = Instantiate(synergySlot, girdLayoutGroup.transform);
                slot.SetSynergyData(originData.Value);
            }
        }
    }
}
