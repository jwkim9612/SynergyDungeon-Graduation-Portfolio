using UnityEngine;
using UnityEngine.UI;

public class UIObtainedRune : UIObtainedGoods
{
    [SerializeField] protected Text goodsGrade = null;

    public override void SetUIObtainedGoods(int runeId)
    {
        var runeDataSheet = DataBase.Instance.runeDataSheet;
        if(runeDataSheet.TryGetRuneData(runeId, out var runeData))
        {
            SetGoodsImage(runeData.Image);
            SetGoodsName(runeData.Name);
            SetRuneGrade(runeData.Grade);
        }
    }

    private void SetRuneGrade(RuneGrade runeGrade)
    {
        this.goodsGrade.text = RuneService.GetNameStrByGrade(runeGrade);
    }
}
