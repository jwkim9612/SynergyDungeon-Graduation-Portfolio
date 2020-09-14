using UnityEngine;

public class TribeData
{
    public TribeData(TribeExcelData tribeExcelData)
    {
        Tribe = tribeExcelData.Name;
        Description = tribeExcelData.Description;

        Image = Resources.Load<Sprite>(tribeExcelData.ImagePath);
    }

    public TribeData(TribeData tribeData)
    {
        Tribe = tribeData.Tribe;
        Description = tribeData.Description;
        Image = tribeData.Image;
    }

    public Tribe Tribe;
    public string Description;
    public Sprite Image;
}
