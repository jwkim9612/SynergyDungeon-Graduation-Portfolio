using System.Collections.Generic;
using System.Linq;

public class UIObtainedRunesScreen : UIControl
{
    private List<UIObtainedRune> uiObtainedRuneList;

    public void Initialize()
    {
        uiObtainedRuneList = GetComponentsInChildren<UIObtainedRune>().ToList();
    }

    public void SetUIObtainedRuneList(List<int> runeIdList)
    {
        for(int i = 0; i < uiObtainedRuneList.Count; i++)
        {
            uiObtainedRuneList[i].SetUIObtainedGoods(runeIdList[i]);
        }
    }
}
