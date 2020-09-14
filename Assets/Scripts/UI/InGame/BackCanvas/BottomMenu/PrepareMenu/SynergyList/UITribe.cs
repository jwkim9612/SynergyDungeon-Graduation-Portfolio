public class UITribe : UISynergy
{
    Tribe tribe;

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
                uiInGameSynergyInfo.SetSynergyInfo(tribe);
                uiInGameSynergyInfo.OnShow();
            }
        });
    }

    public void SetTribe(Tribe newTribe)
    {
        tribe = newTribe;
    }
}
