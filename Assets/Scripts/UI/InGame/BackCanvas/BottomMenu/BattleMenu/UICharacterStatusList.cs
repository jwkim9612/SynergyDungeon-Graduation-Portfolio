using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UICharacterStatusList : MonoBehaviour
{
    public List<UICharacterStatus> characterStatusList = null;

    public void Initialize()
    {
        characterStatusList = gameObject.GetComponentsInChildren<UICharacterStatus>().ToList();

        foreach (var characterStatus in characterStatusList)
        {
            characterStatus.Initialize();
        }
    }
}
