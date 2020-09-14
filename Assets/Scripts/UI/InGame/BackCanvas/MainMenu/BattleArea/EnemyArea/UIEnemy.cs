using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIEnemy : MonoBehaviour
{
    [SerializeField] private UIHPBar uiHPBar = null;
    [SerializeField] private List<UIFloatingText> uiFloatingTextList = null;
    public Enemy enemy;

    public void Initialize()
    {
        uiFloatingTextList = new List<UIFloatingText>();
        uiFloatingTextList = GetComponentsInChildren<UIFloatingText>(true).ToList();

        enemy.Initialize();
        enemy.OnHit += PlayHitParticle;
        enemy.OnHit += PlayShowHPBarForMoment;
        enemy.SetUIFloatingTextList(uiFloatingTextList);
        enemy.InitializeUIFloatingTextList();

        uiHPBar.Initialize();
        uiHPBar.controllingPawn = enemy;
        uiHPBar.SetUpdateHPBarAndAfterImage();
    }

    public void SetEnemy(EnemyData newEnemyData)
    {
        //enemy = cInstantiate(InGameServie.defaultEnemy, transform.root.parent);
        enemy.animator.runtimeAnimatorController = newEnemyData.RuntimeAnimatorController;
        enemy.SetAbility(newEnemyData);
        enemy.OnShow();

        EnemyMoveToUIEnemy();
        uiHPBar.UpdateHPBar();
    }

    public void PlayShowHPBarForMoment()
    {
        StartCoroutine(Co_ShowHPBarForMoment());
    }

    private IEnumerator Co_ShowHPBarForMoment()
    {
        uiHPBar.OnShow();
        yield return new WaitForSeconds(1.5f);
        uiHPBar.OnHide();
        yield break;
    }

    private void PlayHitParticle()
    {
        var hitParticle = GameManager.instance.particleService.hitParticle;
        var frontCanvas = InGameManager.instance.frontCanvas;
        var particlePosition = new Vector3(transform.position.x, transform.position.y, InGameService.Z_VALUE_OF_PARTICLE);

        Instantiate(hitParticle, particlePosition, transform.rotation, frontCanvas.transform);
    }
    
    public void EnemyMoveToUIEnemy()
    {
        if (enemy != null)
        {
            enemy.transform.position = this.transform.position;
        }
    }

    public IEnumerator Co_FollowEnemy()
    {
        if (enemy != null)
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();

                var positionToMove = Vector2.Lerp(enemy.transform.position, this.transform.position, 0.05f);
                enemy.transform.position = new Vector3(positionToMove.x, positionToMove.y, InGameService.Z_VALUE_OF_PAWN);

                if (Mathf.Abs((enemy.transform.position - this.transform.position).y) < 0.01)
                {
                    break;
                }
            }
        }

        yield break;
    }
}
