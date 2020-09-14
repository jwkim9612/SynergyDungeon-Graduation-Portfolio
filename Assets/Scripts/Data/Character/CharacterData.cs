using UnityEngine;

public class CharacterData
{
    public CharacterData(CharacterExcelData characterExcelData)
    {
        Id = characterExcelData.Id;
        Name = characterExcelData.Name;
        Tribe = characterExcelData.Tribe;
        Origin = characterExcelData.Origin;
        Tier = characterExcelData.Tier;

        Image = Resources.Load<Sprite>(characterExcelData.ImagePath);
        HeadImage = Resources.Load<Sprite>(characterExcelData.HeadImagePath);
        RuntimeAnimatorController = Resources.Load<RuntimeAnimatorController>(characterExcelData.RuntimeAnimatorControllerPath);

        var originDataSheet = DataBase.Instance.originDataSheet;
        var tribeDataSheet = DataBase.Instance.tribeDataSheet;

        if (originDataSheet == null)
        {
            Debug.LogError("Error originDataSheet is null");
            return;
        }
        if (tribeDataSheet == null)
        {
            Debug.LogError("Error tribeDataSheet is null");
            return;
        }

        if (originDataSheet.TryGetOriginData(Origin, out var originData))
        {
            OriginData = originData;
        }

        if (tribeDataSheet.TryGetTribeData(Tribe, out var tribeData))
        {
            TribeData = tribeData;
        }
    }

    public CharacterData(CharacterData characterData)
    {
        Id = characterData.Id;
        Name = characterData.Name;
        Tribe = characterData.Tribe;
        Origin = characterData.Origin;
        Tier = characterData.Tier;
        Image = characterData.Image;
        HeadImage = characterData.HeadImage;
        RuntimeAnimatorController = characterData.RuntimeAnimatorController;
        OriginData = characterData.OriginData;
        TribeData = characterData.TribeData;
    }

    public int Id;
    public string Name;
    public Tribe Tribe;
    public Origin Origin;
    public Tier Tier;
    public Sprite Image;
    public Sprite HeadImage;
    public RuntimeAnimatorController RuntimeAnimatorController;
    public OriginData OriginData;
    public TribeData TribeData;
}
