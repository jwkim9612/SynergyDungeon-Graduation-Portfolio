using Shared.Service;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleStatus : MonoBehaviour
{
    [SerializeField] private UIBattleStart uiBattleStart = null;

    public List<Character> characters { get; set; }
    public List<Enemy> enemies { get; set; }
    public List<Pawn> pawnsAttackSequenceList { get; set; }

    private bool isCharacterAnnihilation;
    private bool isEnemyAnnihilation;

    public void BattleStart()
    {
        InitializeAttackSequence();
        InitializeAnnihilation();
        InitializePawns();

        StartCoroutine(Battle());
    }

    // 전투
    private IEnumerator Battle()
    {
        if (characters.Count == 0)
            isCharacterAnnihilation = true;

        uiBattleStart.PlayAnimation();
        yield return new WaitForSeconds(uiBattleStart.playTime + 0.5f);

        while (!isCharacterAnnihilation && !isEnemyAnnihilation)
        {
            List<Pawn> removePawnList = new List<Pawn>();

            foreach (var pawn in pawnsAttackSequenceList)
            {
                if (pawn.isDead)
                {
                    continue;
                }

                if(pawn.HasAnimation())
                {
                    pawn.PlayAttackAnimationAndGetTarget();
                }
                else
                {
                    pawn.RandomAttack();
                }

                //if(pawn is Character)
                //{
                //    // 애니메이션 다 생기면 없앰.
                //    Character character = pawn as Character;
                //    if (character.HasAnimation())
                //    {
                //        character.PlayAttackAnimationAndGetTarget();
                //    }
                //    else
                //    {
                //        character.RandomAttack();
                //    }
                //    // Character의 애니메이션 안에 RandomAttack이 있음
                //}
                //else
                //{
                //    pawn.RandomAttack();
                //}

                float attackAnimationLength = pawn.GetAttackAnimationLength();
                yield return new WaitForSeconds(attackAnimationLength + InGameService.ATTACK_DELAY);

                Pawn target = pawn.target;

                if (target.isDead)
                {
                    yield return new WaitForSeconds(InGameService.DEAD_DELAY);
                    RemoveFromAttackList(target);
                    removePawnList.Add(target);

                    if (characters.Count == 0)
                    {
                        isCharacterAnnihilation = true;
                        break;
                    }
                    else if (enemies.Count == 0)
                    {
                        isEnemyAnnihilation = true;
                        break;
                    }
                }
            }

            pawnsAttackSequenceList.RemoveAll(removePawnList.Contains);
        }

        // 배틀 종료 
        if (isCharacterAnnihilation)
        {
            SaveManager.Instance.RemoveInGameData();
            InGameManager.instance.gameState.SetIsPlayerLose();
            Debug.Log("Battle End");
            yield break;
        }
        else if (isEnemyAnnihilation)
        {
            AllCharactersPlayWinAnimation();
            yield return new WaitForSeconds(2.0f);

            InGameManager.instance.gameState.SetIsWaveClear();
            yield break;
        }
        else
        {
            Debug.Log("Error Battle End");
        }
    }

    /// <summary>
    /// 공격 순서 초기화
    /// </summary>
    private void InitializeAttackSequence()
    {
        List<Pawn> pawns = new List<Pawn>();
        pawns.AddRange(characters);
        pawns.AddRange(enemies);

        pawnsAttackSequenceList = pawns.OrderByDescending(x => x.ability.AttackSpeed).ToList();
    }

    // 전멸 현황 초기화
    private void InitializeAnnihilation()
    {
        isCharacterAnnihilation = false;
        isEnemyAnnihilation = false;
    }

    // 현재 배틀 공간에 있는 폰들 초기화
    private void InitializePawns()
    {
        foreach (var pawn in pawnsAttackSequenceList)
        {
            pawn.ResetStat();
        }
    }

    /// <summary>
    /// 어떤 타입의 폰인지 확인하고 공격해야 할 폰을 
    /// 찾은 후 공격한다.
    /// </summary>
    /// <param name="pawn"> 공격하는 폰 </param>
    /// <returns> 타겟 </returns>
    private Pawn RandomAttackAndGetTarget(Pawn pawn)
    {
        // 공격하는 폰이 캐릭터인 경우
        if (IsCharacter(pawn))
        {
            int enemyIndex = GetRandomEnemyIndex();
            pawn.Attack(enemies[enemyIndex]);
            return enemies[enemyIndex];
        }
        else
        {
            int characterIndex = GetRandomCharacterIndex();
            pawn.Attack(characters[characterIndex]);
            return characters[characterIndex];
        }
    }

    /// <summary>
    /// 폰을 리스트에서 삭제함
    /// </summary>
    /// <param name="pawn"> 지워줄 폰 </param>
    private void RemoveFromAttackList(Pawn pawn)
    {
        if (IsCharacter(pawn))
        {
            characters.Remove(pawn as Character);
        }
        else
        {
            enemies.Remove(pawn as Enemy);
        }
    }

    private bool IsCharacter(Pawn pawn)
    {
        return pawn.pawnType == PawnType.Character;
    }

    // 배틀 공간에 있는 적들중 하나를 반환
    public Enemy GetRandomEnemy()
    {
        int enemiesRandomIndex = GetRandomEnemyIndex();
        return enemies[enemiesRandomIndex];
    }

    // 배틀 공간에 있는 캐릭터중 하나를 반환
    public Character GetRandomCharacter()
    {
        int charactersRandomIndex = GetRandomCharacterIndex();
        return characters[charactersRandomIndex];
    }

    // 배틀공간에 있는 살아있는 적들중 랜덤으로 하나의 인덱스를 반환 
    private int GetRandomEnemyIndex()
    {
        if (enemies.Count <= 0)
        {
            Debug.LogError("Error GetRandomEnemyIndex");
            return -1;
        }

        return RandomService.RandRange(0, enemies.Count);
    }

    // 배틀공간에 있는 살아있는 캐릭터중 랜덤으로 하나의 인덱스를 반환 
    private int GetRandomCharacterIndex()
    {
        if (characters.Count <= 0)
        {
            Debug.LogError("Error GetRandomCharacterIndex");
            return -1;
        }

        return RandomService.RandRange(0, characters.Count);
    }

    // 모든 캐릭터의 승리 애니메이션 실행
    private void AllCharactersPlayWinAnimation()
    {
        foreach (var character in characters)
        {
            character.PlayWinAnimation();
        }
    }
}
