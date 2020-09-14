public class AbilityService
{
    public static string GetAbilityNameByIndex(int index)
    {
        string abilityName = "";

        switch (index)
        {
            case 0:
                abilityName = "공격력";
                break;
            case 1:
                abilityName = "마법공격력";
                break;
            case 2:
                abilityName = "체력";
                break;
            case 3:
                abilityName = "방어력";
                break;
            case 4:
                abilityName = "마법방어력";
                break;
            case 5:
                abilityName = "방어막";
                break;
            case 6:
                abilityName = "적중률";
                break;
            case 7:
                abilityName = "회피율";
                break;
            case 8:
                abilityName = "크리티컬";
                break;
            case 9:
                abilityName = "공격속도";
                break;
            default:
                abilityName = "Error";
                break;
        }

        return abilityName;
    }
}
