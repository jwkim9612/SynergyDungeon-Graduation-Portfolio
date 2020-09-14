using UnityEngine;

public class EnemyData
{
    public EnemyData(EnemyExcelData enemyExcelData)
    {
        Id = enemyExcelData.Id;
        Name = enemyExcelData.Name;
        
        abilityData = new AbilityData();
        abilityData.SetAbilityData(enemyExcelData);

        Image = Resources.Load<Sprite>(enemyExcelData.ImagePath);
        RuntimeAnimatorController = Resources.Load<RuntimeAnimatorController>(enemyExcelData.RuntimeAnimatorControllerPath);
    }

    public EnemyData(EnemyData enemyData)
    {
        Id = enemyData.Id;
        Name = enemyData.Name;
        abilityData = enemyData.abilityData;
        Image = enemyData.Image;
        RuntimeAnimatorController = enemyData.RuntimeAnimatorController;
    }

    public int Id;
    public string Name;
    public AbilityData abilityData;
    public Sprite Image;
    public RuntimeAnimatorController RuntimeAnimatorController;
}
