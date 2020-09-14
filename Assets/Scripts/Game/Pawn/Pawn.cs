using Shared.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public delegate void OnAttackDelegate();
    public delegate void OnHitDelegate();
    public delegate void OnIsDeadDelegate();
    public OnAttackDelegate OnAttack { get; set; }
    public OnHitDelegate OnHit { get; set; }
    public OnIsDeadDelegate OnIsDead { get; set; }

    public PawnType pawnType { get; set; }
    public bool isDead { get; set; }
    public AbilityData ability;
    protected int currentHP;
    public Vector3 originPosition { get; set; }
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public bool isOnBattlefield { get; set; }

    public Pawn target { get; set; }

    public List<UIFloatingText> uiFloatingTextList { get; set; } = null;
    protected int floatingTextIndex;

    public void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        floatingTextIndex = 0;

        OnHit += PlayAnimationByTakeHit;
        InGameManager.instance.gameState.OnBattle += SetInBattle;
    }

    // 공격
    public void Attack(Pawn target)
    {
        if (target == null)
        {
            Debug.Log("target is null");
            return;
        }

        if (HasAnimation())
        {
            AttackProcessing();
        }
        else
        {
            StartCoroutine(Co_AttackAndAnimation());
        }

        //InGameManager.instance.battleLogService.AddBattleLog(name + "(이)가 " + target.name + "(이)에게 " + finalDamage + "데미지를 입혔습니다.");
    }

    // 애니메이터에서 사용
    private void TeleportToTarget()
    {
        spriteRenderer.sortingOrder = InGameService.DEFAULT_PAWN_ORDER_IN_LAYER + 1;
        originPosition = gameObject.transform.position;
        gameObject.transform.position = target.gameObject.transform.position;
    }

    // 애니메이터에서 사용
    private void BackToOriginPosition()
    {
        gameObject.transform.position = originPosition;
        spriteRenderer.sortingOrder = InGameService.DEFAULT_PAWN_ORDER_IN_LAYER;
    }

    // 랜덤 공격
    public virtual void RandomAttack()
    {
    }

    /// <summary>
    /// 데미지를 받는 함수
    /// </summary>
    /// <param name="damage">받은 데미지</param>
    /// <returns>최종적으로 입은 데미지</returns>
    public int TakeDamage(int attack, int magicAttack, bool isCritical)
    {
        int attackDamage = Mathf.Clamp(attack - ability.Defence, 0, attack);
        int magicAttackDamage = Mathf.Clamp(magicAttack - ability.MagicDefence, 0, magicAttack);

        int finalDamage = attackDamage + magicAttackDamage;

        if (isCritical)
        {
            finalDamage = Mathf.Clamp((finalDamage * 2), 1, finalDamage);
            PlayCriticalHitText(finalDamage);
        }
        else
        {
            finalDamage = Mathf.Clamp(finalDamage, 1, finalDamage);
            PlayHitText(finalDamage);
        }

        currentHP = Mathf.Clamp((currentHP - finalDamage), 0, currentHP);

        if (currentHP <= 0)
        {
            isDead = true;
        }

        OnHit();
        return finalDamage;
    }

    // 스텟 리셋
    public virtual void ResetStat()
    {
        isDead = false;
    }

    // HP퍼센트를 반환
    public float GetHPRatio()
    {
        return currentHP / (float)ability.Health;
    }

    // 공격이 성공했는지를 반환 (회피율 계산)
    protected bool GetAttackSuccessful(Pawn target)
    {
        long currentAccuracy = ability.Accuracy - target.ability.Evasion;
        long randomAccuracyNum = RandomService.GetRandomLong();

        if (currentAccuracy <= randomAccuracyNum)
            return false;
        else
            return true;
    }

    // 크리티컬이 발동 됐는지를 반환
    protected bool IsCriticalAttack()
    {
        long currentCritical = ability.Critical;
        long randomCriticalNum = RandomService.GetRandomLong();

        if (currentCritical <= randomCriticalNum)
            return false;
        else
            return true;
    }

    // 사이즈 셋팅
    public void SetSize(float size)
    {
        spriteRenderer.transform.localScale = new Vector3(size, size, size);
    }

    // 이미지 셋팅
    public void SetImage(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    
    // 플로팅 텍스트 셋팅
    public void SetUIFloatingTextList(List<UIFloatingText> uiFloatingTextList)
    {
        this.uiFloatingTextList = uiFloatingTextList;
    }

    // 플로팅 텍스트 초기화
    public void InitializeUIFloatingTextList()
    {
        if (uiFloatingTextList != null)
        {
            foreach(var uiFloatingText in uiFloatingTextList)
            {
                uiFloatingText.Initialize();
            }
        }
    }

    // 공격 애니메이션 시간을 반환
    public float GetAttackAnimationLength()
    {
        if (animator == null)
        {
            return 1.0f;
        }

        if (animator.runtimeAnimatorController != null)
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                if (ac.animationClips[i].name == "Attack")
                {
                    return ac.animationClips[i].length;
                }
            }
        }
        else
        {
            return 1.0f;
        }

        Debug.LogError("Error GetAttackAnimationLength");
        return -1;
    }

    // 공격 애니메이션 실행
    public abstract void PlayAttackAnimationAndGetTarget();

    public abstract bool IsActivated();

    public abstract void ResetAbility();

    // 공격이 성공했는지 확인후 타겟에 데미지를 주는 공격 처리
    public void AttackProcessing()
    {
        if (GetAttackSuccessful(target))
        {
            if (IsCriticalAttack())
                target.TakeDamage(ability.Attack, ability.MagicalAttack, true);
            else
                target.TakeDamage(ability.Attack, ability.MagicalAttack, false);
        }
        else
            target.PlayMissText();
    }

    // 기본 데미지 플로팅 텍스트 실행
    private void PlayHitText(float damage)
    {
        uiFloatingTextList[floatingTextIndex].SetText(damage.ToString(), Color.red);
        uiFloatingTextList[floatingTextIndex].SetTextSize(InGameService.DEFAULT_DAMAGE_FONT_SIZE);
        PlayFloatingText();
    }

    // 크리티컬 플로팅 텍스트 실행
    private void PlayCriticalHitText(float damage)
    {
        Color orange = new Color(1.0f, 0.64f, 0.0f);
        uiFloatingTextList[floatingTextIndex].SetText(damage.ToString(), orange);
        uiFloatingTextList[floatingTextIndex].SetTextSize(InGameService.CRITICAL_DAMAGE_FONT_SIZE);
        PlayFloatingText();
    }

    // Miss 플로팅 텍스트 실행
    public void PlayMissText()
    {
        uiFloatingTextList[floatingTextIndex].SetText("Miss", Color.gray);
        uiFloatingTextList[floatingTextIndex].SetTextSize(InGameService.MISS_FONT_SIZE);
        PlayFloatingText();
    }

    // 플로팅 텍스트 실행
    private void PlayFloatingText()
    {
        uiFloatingTextList[floatingTextIndex].Play();
        ++floatingTextIndex;

        if (floatingTextIndex >= uiFloatingTextList.Count)
            floatingTextIndex = 0;
    }

    // 공격과 애니메이션 실행
    protected virtual IEnumerator Co_AttackAndAnimation()
    {
        yield return new WaitForEndOfFrame();
    }

    private void SetInBattle()
    {
        if (!isOnBattlefield)
            return;

        if (animator.runtimeAnimatorController != null)
        {
            animator.SetBool("InBattle", true);
        }
    }

    private void PlayAnimationByTakeHit()
    {
        if(isDead)
        {
            PlayDeadAnimation();
        }
        else
        {
            PlayTakeHitAnimation();
        }
    }

    // 데미지를 받았을 때 애니메이션
    protected void PlayTakeHitAnimation()
    {
        if (animator.runtimeAnimatorController != null)
        {
            animator.SetBool("TakeHit", true);
        }
    }
    
    // 죽었을 때 애니메이션
    protected void PlayDeadAnimation()
    {
        if (animator.runtimeAnimatorController != null)
        {
            animator.SetBool("Dead", true);
        }
    }

    // Hit 애니메이션에서 사용함.
    private void TakeHitEnd()
    {
        animator.SetBool("TakeHit", false);
    }

    // Dead 애니메이션에서 사용함.
    private void DeadEnd()
    {
        animator.SetBool("Dead", false);

        OnHide();
    }

    // BattleIdle 애니메이션에서 사용함.
    private void UpdateBattleIdle()
    {
        bool isChanged = Random.Range(0, 2) > 0;
        if (isChanged)
        {
            animator.SetTrigger("ChangeBattleIdle");
        }
    }

    // Attack 애니메이션에서 사용함.
    private void AttackEnd()
    {
        animator.SetBool("Attack", false);
    }

    public bool HasAnimation()
    {
        if (animator != null)
        {
            if (animator.runtimeAnimatorController != null)
            {
                return true;
            }
        }

        return false;
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}
