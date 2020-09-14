public class UIRuneOnRunePage : UIRuneForRunePage
{
    public override void SetUIRune(RuneData newRuneData)
    {
        base.SetUIRune(newRuneData);

        isEquippedRune = false;
    }
}