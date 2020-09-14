using System;
using System.Collections;
using System.Collections.Generic;

public class AbilityData : IEnumerable
{
    private List<int> abilityList;

    public int Attack { get { return abilityList[0]; } set { abilityList[0] = value; } }
    public int MagicalAttack { get { return abilityList[1]; } set { abilityList[1] = value; } }
    public int Health { get { return abilityList[2]; } set { abilityList[2] = value; } }
    public int Defence { get { return abilityList[3]; } set { abilityList[3] = value; } }
    public int MagicDefence { get { return abilityList[4]; } set { abilityList[4] = value; } }
    public int Shield { get { return abilityList[5]; } set { abilityList[5] = value; } }
    public int Accuracy { get { return abilityList[6]; } set { abilityList[6] = value; } }
    public int Evasion { get { return abilityList[7]; } set { abilityList[7] = value; } }
    public int Critical { get { return abilityList[8]; } set { abilityList[8] = value; } }
    public int AttackSpeed { get { return abilityList[9]; } set { abilityList[9] = value; } }

    public AbilityData()
    {
        abilityList = new List<int>(new int[10]);
    }

    public IEnumerator GetEnumerator()
    {
        return abilityList.GetEnumerator();
    }

    public int this[int index]
    {
        get
        {
            if (index < 0 || index >= abilityList.Count)
            {
                throw new IndexOutOfRangeException();
            }
            else
                return abilityList[index];
        }
        set
        {
            if (index < 0 || index >= abilityList.Count)
            {
                throw new IndexOutOfRangeException();
            }
            else
                abilityList[index] = value;
        }
    }

    public static AbilityData operator +(AbilityData ability1, AbilityData ability2)
    {
        AbilityData abilityData = new AbilityData();
        abilityData.Attack = ability1.Attack + ability2.Attack;
        abilityData.MagicalAttack = ability1.MagicalAttack + ability2.MagicalAttack;
        abilityData.Health = ability1.Health + ability2.Health;
        abilityData.Defence = ability1.Defence + ability2.Defence;
        abilityData.MagicDefence = ability1.MagicDefence + ability2.MagicDefence;
        abilityData.Shield = ability1.Shield + ability2.Shield;
        abilityData.Accuracy = ability1.Accuracy + ability2.Accuracy;
        abilityData.Evasion = ability1.Evasion + ability2.Evasion;
        abilityData.Critical = ability1.Critical + ability2.Critical;
        abilityData.AttackSpeed = ability1.AttackSpeed + ability2.AttackSpeed;

        return abilityData;
    }

    public List<int> GetAbilityDataList()
    {
        List<int> abilityDataList = new List<int>();

        abilityDataList.Add(Attack);
        abilityDataList.Add(MagicalAttack);
        abilityDataList.Add(Health);
        abilityDataList.Add(Defence);
        abilityDataList.Add(MagicDefence);
        abilityDataList.Add(Shield);
        abilityDataList.Add(Accuracy);
        abilityDataList.Add(Evasion);
        abilityDataList.Add(Critical);
        abilityDataList.Add(AttackSpeed);

        return abilityDataList;
    }

    public void SetAbilityData(CharacterAbilityExcelData characterAbilityExcelData)
    {
        Attack = characterAbilityExcelData.Attack;
        MagicalAttack = characterAbilityExcelData.MagicalAttack;
        Health = characterAbilityExcelData.Health;
        Defence = characterAbilityExcelData.Defence;
        MagicDefence = characterAbilityExcelData.MagicDefence;
        Shield = characterAbilityExcelData.Shield;
        Accuracy = characterAbilityExcelData.Accuracy;
        Evasion = characterAbilityExcelData.Evasion;
        Critical = characterAbilityExcelData.Critical;
        AttackSpeed = characterAbilityExcelData.AttackSpeed;
    }

    public void SetAbilityData(CharacterAbilityData characterAbilityData)
    {
        Attack = characterAbilityData.abilityData.Attack;
        MagicalAttack = characterAbilityData.abilityData.MagicalAttack;
        Health = characterAbilityData.abilityData.Health;
        Defence = characterAbilityData.abilityData.Defence;
        MagicDefence = characterAbilityData.abilityData.MagicDefence;
        Shield = characterAbilityData.abilityData.Shield;
        Accuracy = characterAbilityData.abilityData.Accuracy;
        Evasion = characterAbilityData.abilityData.Evasion;
        Critical = characterAbilityData.abilityData.Critical;
        AttackSpeed = characterAbilityData.abilityData.AttackSpeed;
    }

    public void SetAbilityData(EnemyExcelData enemyExcelData)
    {
        Attack = enemyExcelData.Attack;
        MagicalAttack = enemyExcelData.MagicalAttack;
        Health = enemyExcelData.Health;
        Defence = enemyExcelData.Defence;
        MagicDefence = enemyExcelData.MagicDefence;
        Shield = enemyExcelData.Shield;
        Accuracy = enemyExcelData.Accuracy;
        Evasion = enemyExcelData.Evasion;
        Critical = enemyExcelData.Critical;
        AttackSpeed = enemyExcelData.AttackSpeed;
    }

    public void SetAbilityData(AbilityData abilityData)
    {
        Attack = abilityData.Attack;
        MagicalAttack = abilityData.MagicalAttack;
        Health = abilityData.Health;
        Defence = abilityData.Defence;
        MagicDefence = abilityData.MagicDefence;
        Shield = abilityData.Shield;
        Accuracy = abilityData.Accuracy;
        Evasion = abilityData.Evasion;
        Critical = abilityData.Critical;
        AttackSpeed = abilityData.AttackSpeed;
    }

    public void SetAbilityData(RuneExcelData runeExcelData)
    {
        Attack = runeExcelData.Attack;
        MagicalAttack = runeExcelData.MagicalAttack;
        Health = runeExcelData.Health;
        Defence = runeExcelData.Defence;
        MagicDefence = runeExcelData.MagicDefence;
        Shield = runeExcelData.Shield;
        Accuracy = runeExcelData.Accuracy;
        Evasion = runeExcelData.Evasion;
        Critical = runeExcelData.Critical;
        AttackSpeed = runeExcelData.AttackSpeed;
    }

    public void IncreaseAbilityByValue(Ability ability, int value)
    {
        switch (ability)
        {
            case Ability.None:
                break;
            case Ability.All:
                Attack += value;
                MagicalAttack += value;
                Health += value;
                Defence += value;
                MagicDefence += value;
                Shield += value;
                Accuracy += value;
                Evasion += value;
                Critical += value;
                AttackSpeed += value;
                break;
            case Ability.Attack:
                Attack += value;
                break;
            case Ability.MagicalAttack:
                MagicalAttack += value;
                break;
            case Ability.Health:
                Health += value;
                break;
            case Ability.Defence:
                Defence += value;
                break;
            case Ability.MagicDefence:
                MagicDefence += value;
                break;
            case Ability.Shield:
                Shield += value;
                break;
            case Ability.Accuracy:
                Accuracy += value;
                break;
            case Ability.Evasion:
                Evasion += value;
                break;
            case Ability.Critical:
                Critical += value;
                break;
            case Ability.AttackSpeed:
                AttackSpeed += value;
                break;
            default:
                break;
        }
    }

    public void IncreaseAbilityByPercentage(Ability ability, int percentage)
    {
        switch (ability)
        {
            case Ability.None:
                break;
            case Ability.All:
                Attack = (int)(Attack * (1 + (percentage * 0.01f)));
                MagicalAttack = (int)(MagicalAttack * (1 + (percentage * 0.01f)));
                Health = (int)(Health * (1 + (percentage * 0.01f)));
                Defence = (int)(Defence * (1 + (percentage * 0.01f)));
                MagicDefence = (int)(MagicDefence * (1 + (percentage * 0.01f)));
                Shield = (int)(Shield * (1 + (percentage * 0.01f)));
                Accuracy = (int)(Accuracy * (1 + (percentage * 0.01f)));
                Evasion = (int)(Evasion * (1 + (percentage * 0.01f)));
                Critical = (int)(Critical * (1 + (percentage * 0.01f)));
                AttackSpeed = (int)(AttackSpeed * (1 + (percentage * 0.01f)));
                break;
            case Ability.Attack:
                Attack = (int)(Attack * (1 + (percentage * 0.01f)));
                break;
            case Ability.MagicalAttack:
                MagicalAttack = (int)(MagicalAttack * (1 + (percentage * 0.01f)));
                break;
            case Ability.Health:
                Health = (int)(Health * (1 + (percentage * 0.01f)));
                break;
            case Ability.Defence:
                Defence = (int)(Defence * (1 + (percentage * 0.01f)));
                break;
            case Ability.MagicDefence:
                MagicDefence = (int)(MagicDefence * (1 + (percentage * 0.01f)));
                break;
            case Ability.Shield:
                Shield = (int)(Shield * (1 + (percentage * 0.01f)));
                break;
            case Ability.Accuracy:
                Accuracy = (int)(Accuracy * (1 + (percentage * 0.01f)));
                break;
            case Ability.Evasion:
                Evasion = (int)(Evasion * (1 + (percentage * 0.01f)));
                break;
            case Ability.Critical:
                Critical = (int)(Critical * (1 + (percentage * 0.01f)));
                break;
            case Ability.AttackSpeed:
                AttackSpeed = (int)(AttackSpeed * (1 + (percentage * 0.01f)));
                break;
            default:
                break;
        }
    }
}
