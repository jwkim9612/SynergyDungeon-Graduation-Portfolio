public class UIOrigin : UISynergy
{
    Origin origin;

    public override void Initialize()
    {
        base.Initialize();

        SetToggle();
    }

    private void SetToggle()
    {
        toggle.onValueChanged.AddListener((bool bOn) =>
        {
            if (bOn)
            {
                uiInGameSynergyInfo.SetSynergyInfo(origin);
                uiInGameSynergyInfo.OnShow();
            }
        });
    }

    public void SetOrigin(Origin newOrigin)
    {
        origin = newOrigin;
    }
}
