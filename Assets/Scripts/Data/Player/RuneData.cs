using UnityEngine;

public class RuneData
{
    public RuneData(RuneExcelData runeExcelData)
    {
        Id = runeExcelData.Id;
        Name = runeExcelData.Name;
        SocketPosition = runeExcelData.SocketPosition;
        Grade = runeExcelData.Grade;
        Prob = runeExcelData.Prob;
        Description = runeExcelData.Description;

        AbilityData = new AbilityData();
        AbilityData.SetAbilityData(runeExcelData);

        Image = Resources.Load<Sprite>(runeExcelData.ImagePath);
    }

    public RuneData(RuneData runeData)
    {
        Id = runeData.Id;
        Name = runeData.Name;
        SocketPosition = runeData.SocketPosition;
        Grade = runeData.Grade;
        Prob = runeData.Prob;
        Description = runeData.Description;
        AbilityData = runeData.AbilityData;
        Image = runeData.Image;
    }

    public int Id;
    public string Name;
    public int SocketPosition;
    public RuneGrade Grade;
    public int Prob;
    public string Description;
    public AbilityData AbilityData;
    public Sprite Image;
}
