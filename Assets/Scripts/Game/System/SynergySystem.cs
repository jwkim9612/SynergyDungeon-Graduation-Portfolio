using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// 완성되면 설명 적기.
public class SynergySystem : MonoBehaviour
{
    public delegate void OnTribeChangedDelegate();
    public delegate void OnOriginChangedDelegate();
    public OnTribeChangedDelegate OnTribeChanged { get; set; }
    public OnOriginChangedDelegate OnOriginChanged { get; set; }

    public Dictionary<TribeInfo, int> deployedTribes;
    public Dictionary<OriginInfo, int> deployedOrigins;
    public Dictionary<Tribe, int> appliedTribes;
    public Dictionary<Origin, int> appliedOrigins;

    private bool IsSubCharacter;
    private Tribe currentAddTribe;
    private Origin currentAddOrigin;

    private CharacterDataSheet characterDataSheet;

    public SynergySystem()
    {
        deployedTribes = new Dictionary<TribeInfo, int>();
        deployedOrigins = new Dictionary<OriginInfo, int>();
        appliedTribes = new Dictionary<Tribe, int>();
        appliedOrigins = new Dictionary<Origin, int>();
    }

    public void Initialize()
    {
        characterDataSheet = DataBase.Instance.characterDataSheet;
        if (characterDataSheet == null)
        {
            Debug.LogError("Error characterDataSheet is null");
            return;
        }

        IsSubCharacter = false;
    }

    public void AddCharacter(CharacterInfo characterInfo)
    {
        IsSubCharacter = false;

        if (characterDataSheet.TryGetCharacterTribe(characterInfo.id, out var tribe))
        {
            TribeInfo tribeInfo = new TribeInfo(tribe, characterInfo.id);
            currentAddTribe = tribe;

            if (deployedTribes.ContainsKey(tribeInfo))
            {
                ++deployedTribes[tribeInfo];
            }
            else
            {
                deployedTribes.Add(tribeInfo, 1);
                AddAppliedTribe(tribeInfo.tribe);
            }
        }

        if (characterDataSheet.TryGetCharacterOrigin(characterInfo.id, out var origin))
        {
            OriginInfo originInfo = new OriginInfo(origin, characterInfo.id);
            currentAddOrigin = origin;

            if (deployedOrigins.ContainsKey(originInfo))
            {
                ++deployedOrigins[originInfo];
            }
            else
            {
                deployedOrigins.Add(originInfo, 1);
                AddAppliedOrigin(originInfo.origin);
            }
        }

        UpdateApplySynergy();
    }

    public void SubCharacter(CharacterInfo characterInfo)
    {
        IsSubCharacter = true;

        if (characterDataSheet.TryGetCharacterTribe(characterInfo.id, out var tribe))
        {
            TribeInfo tribeInfo = new TribeInfo(tribe, characterInfo.id);

            if (deployedTribes.ContainsKey(tribeInfo))
            {
                --deployedTribes[tribeInfo];

                if (deployedTribes[tribeInfo] == 0)
                {
                    deployedTribes.Remove(tribeInfo);
                    SubAppliedTribe(tribeInfo.tribe);
                }
            }
            else
            {
                Debug.Log("Error No Tribes");
            }
        }

        if (characterDataSheet.TryGetCharacterOrigin(characterInfo.id, out var origin))
        {
            OriginInfo originInfo = new OriginInfo(origin, characterInfo.id);

            if (deployedOrigins.ContainsKey(originInfo))
            {
                --deployedOrigins[originInfo];

                if (deployedOrigins[originInfo] == 0)
                {
                    deployedOrigins.Remove(originInfo);
                    SubAppliedOrigin(originInfo.origin);
                }
            }
            else
            {
                Debug.Log("Error No Origins");
            }
        }

        UpdateApplySynergy();
    }

    public void AddAppliedTribe(Tribe tribe)
    {
        if (appliedTribes.ContainsKey(tribe))
        {
            ++appliedTribes[tribe];
        }
        else
        {
            appliedTribes.Add(tribe, 1);
        }

        OnTribeChanged();
    }

    public void AddAppliedOrigin(Origin origin)
    {
        if (appliedOrigins.ContainsKey(origin))
        {
            ++appliedOrigins[origin];
        }
        else
        {
            appliedOrigins.Add(origin, 1);
        }

        OnOriginChanged();
    }


    public void SubAppliedTribe(Tribe tribe)
    {
        if (appliedTribes.ContainsKey(tribe))
        {
            --appliedTribes[tribe];

            if (appliedTribes[tribe] == 0)
            {
                appliedTribes.Remove(tribe);
            }
        }
        else
        {
            Debug.Log("Error No AppliedTribes");
        }

        OnTribeChanged();
    }

    public void SubAppliedOrigin(Origin origin)
    {
        if (appliedOrigins.ContainsKey(origin))
        {
            --appliedOrigins[origin];

            if (appliedOrigins[origin] == 0)
            {
                appliedOrigins.Remove(origin);
            }
        }
        else
        {
            Debug.Log("Error No AppliedOrigins");
        }

        OnOriginChanged();
    }

    public void SubCharacterFromCombinations(UICharacter uiCharacter, bool isFirstCharacter)
    {
        if (isFirstCharacter)
            return;

        if (uiCharacter.GetArea<UIBattleArea>() != null)
            InGameManager.instance.synergySystem.SubCharacter(uiCharacter.characterInfo);
    }

    public void UpdateApplySynergy()
    {
        var uiCharacterArea = InGameManager.instance.draggableCentral.uiCharacterArea;
        var uiEnemyArea = InGameManager.instance.backCanvas.uiMainMenu.uiBattleArea.uiEnemyArea;
        uiCharacterArea.ResetAllCharacterAbility();
        uiEnemyArea.ResetAllEnemyAbility();

        var characterInFieldList = uiCharacterArea.GetCharacterList();
        if (characterInFieldList == null)
            return;

        var EnemyList = uiEnemyArea.GetEnemyList();

        foreach (var appliedOrigin in appliedOrigins)
        {
            bool isChangedOrigin = false;
            if (currentAddOrigin == appliedOrigin.Key)
                isChangedOrigin = true;

            switch (appliedOrigin.Key)
            {
                case Origin.None:
                    Debug.Log("Error UpdateApplyOriginSynergy");
                    return;
                case Origin.Archer:
                    if (appliedOrigin.Value >= 5)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.Critical += 15;

                            if(!IsSubCharacter && isChangedOrigin)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    else if (appliedOrigin.Value >= 3)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.origin == Origin.Archer)
                            {
                                character.ability.Critical += 15;

                                if (!IsSubCharacter && isChangedOrigin)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    else if (appliedOrigin.Value >= 1)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.origin == Origin.Archer)
                            {
                                character.ability.Critical += 7;

                                if (!IsSubCharacter && isChangedOrigin)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    break;
                case Origin.Paladin:
                    if (appliedOrigin.Value >= 4)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.MagicDefence += 20;

                            if (!IsSubCharacter && isChangedOrigin)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    else if (appliedOrigin.Value >= 2)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.MagicDefence += 10;

                            if (!IsSubCharacter && isChangedOrigin)
                            {
                                PlayApplySynergyParticle(character.transform);
                                Debug.Log(":???");
                            }
                        }
                    }
                    break;
                case Origin.Thief:
                    if (appliedOrigin.Value >= 4)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.origin == Origin.Thief)
                            {
                                character.ability.Attack = (int)(character.ability.Attack * 1.85);

                                if (!IsSubCharacter && isChangedOrigin)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    else if (appliedOrigin.Value >= 2)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.origin == Origin.Thief)
                            {
                                character.ability.Attack = (int)(character.ability.Attack * 1.4);

                                if (!IsSubCharacter && isChangedOrigin)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    break;
                case Origin.Warrior:
                    if (appliedOrigin.Value >= 4)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.Defence += 20;

                            if (!IsSubCharacter && isChangedOrigin)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    else if (appliedOrigin.Value >= 2)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.Defence += 10;

                            if (!IsSubCharacter && isChangedOrigin)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    break;
                case Origin.Wizard:
                    if (appliedOrigin.Value >= 6)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.MagicalAttack += 80;

                            if (!IsSubCharacter && isChangedOrigin)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    else if (appliedOrigin.Value >= 4)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.MagicalAttack += 45;

                            if (!IsSubCharacter && isChangedOrigin)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    else if (appliedOrigin.Value >= 2)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.MagicalAttack += 20;

                            if (!IsSubCharacter && isChangedOrigin)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    break;
                default:
                    Debug.Log("Error UpdateApplyOriginSynergy");
                    return;
            }
        }

        foreach (var appliedTribe in appliedTribes)
        {
            bool isChangedTribe = false;
            if (currentAddTribe == appliedTribe.Key)
                isChangedTribe = true;

            switch (appliedTribe.Key)
            {
                case Tribe.None:
                    Debug.Log("Error UpdateApplyTribeSynergy");
                    return;
                case Tribe.Beast:
                    if (appliedTribe.Value >= 3)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.tribe == Tribe.Beast)
                            {
                                character.ability.AttackSpeed += 70;

                                if (!IsSubCharacter && isChangedTribe)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    else if (appliedTribe.Value >= 1)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.tribe == Tribe.Beast)
                            {
                                character.ability.AttackSpeed += 30;

                                if (!IsSubCharacter && isChangedTribe)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    break;
                case Tribe.Devil:
                    if (appliedTribe.Value >= 3)
                    {
                        foreach (var enemy in EnemyList)
                        {
                            enemy.ability.Defence = math.clamp(enemy.ability.Defence - 30, 0, enemy.ability.Defence);
                            enemy.ability.MagicDefence = math.clamp(enemy.ability.MagicDefence - 30, 0, enemy.ability.MagicDefence);
                        }
                    }
                    else if (appliedTribe.Value >= 2)
                    {
                        foreach (var enemy in EnemyList)
                        {
                            enemy.ability.Defence = math.clamp(enemy.ability.Defence - 16, 0, enemy.ability.Defence);
                            enemy.ability.MagicDefence = math.clamp(enemy.ability.MagicDefence - 16, 0, enemy.ability.MagicDefence);
                        }
                    }
                    else if (appliedTribe.Value >= 1)
                    {
                        foreach (var enemy in EnemyList)
                        {
                            enemy.ability.Defence = math.clamp(enemy.ability.Defence - 6, 0, enemy.ability.Defence);
                            enemy.ability.MagicDefence = math.clamp(enemy.ability.MagicDefence - 6, 0, enemy.ability.MagicDefence);
                        }
                    }

                    if(appliedTribe.Value >= 1)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.tribe == Tribe.Devil)
                            {
                                if (!IsSubCharacter && isChangedTribe)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    break;
                case Tribe.Dragon:
                    if (appliedTribe.Value == 4)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.IncreaseAbilityByPercentage(Ability.All, 25);

                            if (!IsSubCharacter && isChangedTribe)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    else if (appliedTribe.Value == 1)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.tribe == Tribe.Dragon)
                            {
                                character.ability.IncreaseAbilityByPercentage(Ability.All, 25);

                                if (!IsSubCharacter && isChangedTribe)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    break;
                case Tribe.Elemental:
                    if (appliedTribe.Value >= 4)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.Accuracy += 40;

                            if (!IsSubCharacter && isChangedTribe)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    else if (appliedTribe.Value >= 2)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.Accuracy += 20;

                            if (!IsSubCharacter && isChangedTribe)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    break;
                case Tribe.Elf:
                    if (appliedTribe.Value >= 4)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.tribe == Tribe.Elf)
                            {
                                character.ability.Evasion += 40;

                                if (!IsSubCharacter && isChangedTribe)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    else if (appliedTribe.Value >= 2)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.tribe == Tribe.Elf)
                            {
                                character.ability.Evasion += 20;

                                if (!IsSubCharacter && isChangedTribe)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    break;
                case Tribe.Human:
                    if (appliedTribe.Value >= 5)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.Attack += 25;
                            character.ability.MagicalAttack += 25;

                            if (!IsSubCharacter && isChangedTribe)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    else if (appliedTribe.Value >= 3)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.Attack += 10;
                            character.ability.MagicalAttack += 10;

                            if (!IsSubCharacter && isChangedTribe)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    else if (appliedTribe.Value >= 2)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.Attack += 5;
                            character.ability.MagicalAttack += 5;

                            if (!IsSubCharacter && isChangedTribe)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    break;
                case Tribe.Machine:
                    if (appliedTribe.Value >= 3)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.Health += 450;

                            if (!IsSubCharacter && isChangedTribe)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    else if (appliedTribe.Value >= 2)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            character.ability.Health += 250;

                            if (!IsSubCharacter && isChangedTribe)
                                PlayApplySynergyParticle(character.transform);
                        }
                    }
                    break;
                case Tribe.Undead:
                    if (appliedTribe.Value >= 3)
                    {
                        foreach (var enemy in EnemyList)
                        {
                            enemy.ability.Attack = math.clamp(enemy.ability.Attack - 30, 0, enemy.ability.Attack);
                            enemy.ability.MagicalAttack = math.clamp(enemy.ability.MagicDefence - 30, 0, enemy.ability.MagicDefence);
                        }
                    }
                    else if (appliedTribe.Value >= 2)
                    {
                        foreach (var enemy in EnemyList)
                        {
                            enemy.ability.Attack = math.clamp(enemy.ability.Attack - 16, 0, enemy.ability.Attack);
                            enemy.ability.MagicalAttack = math.clamp(enemy.ability.MagicalAttack - 16, 0, enemy.ability.MagicalAttack);
                        }
                    }
                    else if (appliedTribe.Value >= 1)
                    {
                        foreach (var enemy in EnemyList)
                        {
                            enemy.ability.Attack = math.clamp(enemy.ability.Attack - 6, 0, enemy.ability.Attack);
                            enemy.ability.MagicalAttack = math.clamp(enemy.ability.MagicalAttack - 6, 0, enemy.ability.MagicalAttack);
                        }
                    }

                    if (appliedTribe.Value >= 1)
                    {
                        foreach (var character in characterInFieldList)
                        {
                            if (character.tribe == Tribe.Undead)
                            {
                                if (!IsSubCharacter && isChangedTribe)
                                    PlayApplySynergyParticle(character.transform);
                            }
                        }
                    }
                    break;
                default:
                    Debug.Log("Error UpdateApplyTribeSynergy");
                    return;
            }
        }
    }

    public void PlayApplySynergyParticle(Transform transform)
    {
        var particle = Instantiate(GameManager.instance.particleService.SynergyParticle, transform);
        particle.transform.Translate(new Vector3(0.0f, 0.0f, -1.0f));
    }
}
