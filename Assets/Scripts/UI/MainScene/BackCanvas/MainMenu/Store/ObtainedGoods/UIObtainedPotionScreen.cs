public class UIObtainedPotionScreen : UIObtainedScreenWithDescription
{
    public override void SetUIObtainedScreen(int potionId)
    {
        var potionDataSheet = DataBase.Instance.potionDataSheet;
        if (potionDataSheet.TryGetPotionData(potionId, out var potionData))
        {
            SetGoodsImage(potionData.Image);
            SetGoodsName(potionData.Name);
            SetGoodsDescription(potionData.Description);
            SetPotionGrade(potionData.Grade);
        }
    }
    private void SetPotionGrade(PotionGrade potionGrade)
    {
        this.goodsGrade.text = PotionService.GetNameStrByGrade(potionGrade);
    }
}