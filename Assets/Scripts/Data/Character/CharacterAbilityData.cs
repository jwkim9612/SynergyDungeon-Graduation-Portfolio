public class CharacterAbilityData
{
    public CharacterAbilityData(CharacterAbilityExcelData characterAbilityExcelData)
    {
        Id = characterAbilityExcelData.Id;
        Name = characterAbilityExcelData.Name;

        abilityData = new AbilityData();
        abilityData.SetAbilityData(characterAbilityExcelData);
    }

    public int Id;
    public string Name;
    public AbilityData abilityData;
}
