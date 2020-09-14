using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICharacter : MonoBehaviour
{
    [SerializeField] private List<UIFloatingText> uiFloatingTextList = null;
    [SerializeField] private UIHPBar uiHPBar = null;
    public Character character;
    public bool isFightingOnBattlefield { get; set; }
    public CharacterInfo characterInfo { get; set; }
    public Image clickableImage = null;

    public void Initialize()
    {
        isFightingOnBattlefield = false;

        InitializeUIFloatingTextList();
        character.Initialize();
        character.OnIsDead += OnHide;
        character.OnHit += PlayHitParticle;
        character.OnHit += PlayShowHPBarForMoment;
        character.SetUIFloatingTextList(uiFloatingTextList);
        character.InitializeUIFloatingTextList();

        uiHPBar.Initialize();
        uiHPBar.controllingPawn = character;
        uiHPBar.SetUpdateHPBarAndAfterImage();
    }

    private void InitializeUIFloatingTextList()
    {
        uiFloatingTextList = new List<UIFloatingText>();
        uiFloatingTextList = GetComponentsInChildren<UIFloatingText>(true).ToList();
    }

    //public void SetCharacter(CharacterInfo newCharacterInfo)
    //{
    //    OnCanClick();
    //    SetCharacterInfo(newCharacterInfo);


    //    character = Instantiate(InGameService.defaultCharacter, transform.root.parent);
    //    character.Initialize();

    //    var characterDataSheet = DataBase.Instance.characterDataSheet;
    //    if (characterDataSheet.TryGetCharacterImage(characterInfo.id, out var sprite))
    //    {
    //        character.SetImage(sprite);
    //    }
    //    if (characterDataSheet.TryGetCharacterOrigin(characterInfo.id, out var origin))
    //    {
    //        int Id = characterInfo.id;
    //        int star = characterInfo.star;

    //        var characterAbilityDataSheet = DataBase.Instance.characterAbilityDataSheet;
    //        if(characterAbilityDataSheet.TryGetCharacterAbilityData(Id, star, out var abilityData))
    //        {
    //            character.SetAbility(abilityData, origin);
    //        }
    //    }

    //    character.OnIsDead += OnHide;
    //    character.OnHit += PlayHitParticle;
    //    character.OnHit += PlayShowHPBarForMoment;
    //    character.SetUIFloatingTextList(uiFloatingTextList);
    //    character.characterInfo = this.characterInfo;
    //    character.InitializeUIFloatingTextList();

    //    CharacterMoveToUICharacter();
    //    uiHPBar.Initialize();
    //    uiHPBar.controllingPawn = character;
    //    uiHPBar.SetUpdateHPBarAndAfterImage();
    //}

    public void SetCharacter(CharacterInfo newCharacterInfo)
    {
        OnCanClick();
        characterInfo = newCharacterInfo;
        character.characterInfo = newCharacterInfo;

        var characterDataSheet = DataBase.Instance.characterDataSheet;
        if (characterDataSheet.TryGetCharacterImage(characterInfo.id, out var sprite))
        {
            character.SetImage(sprite);
        }
        if (characterDataSheet.TryGetCharacterOrigin(characterInfo.id, out var origin))
        {
            if(characterDataSheet.TryGetCharacterTribe(characterInfo.id, out var tribe))
            {
                int Id = characterInfo.id;
                int star = characterInfo.star;

                var characterAbilityDataSheet = DataBase.Instance.characterAbilityDataSheet;
                if (characterAbilityDataSheet.TryGetCharacterAbilityData(Id, star, out var abilityData))
                {
                    character.SetAbility(abilityData, origin, tribe);
                }
            }
        }

        CharacterMoveToUICharacter();
        character.OnShow();
        character.isOnBattlefield = false;

        uiHPBar.UpdateHPBar();
    }

    public void SetDefaultImage()
    {
        var characterDataSheet = DataBase.Instance.characterDataSheet;
        if (characterDataSheet.TryGetCharacterImage(characterInfo.id, out var sprite))
        {
            character.RemoveRunTimeAnimatorController();
            character.SetImage(sprite);
        }
    }

    public void SetAnimationImage()
    {
        var characterDataSheet = DataBase.Instance.characterDataSheet;
        if (characterDataSheet.TryGetCharacterRunTimeAnimatorController(characterInfo.id, out var runTimeAnimatorController))
        {
            character.animator.runtimeAnimatorController = runTimeAnimatorController;
        }
    }

    public CharacterInfo DeleteCharacterBySell()
    {
        CharacterInfo deletedCharacterInfo = characterInfo;

        InGameManager.instance.playerState.IncreaseCoin(CharacterService.GetSalePrice(characterInfo));
        InGameManager.instance.characterStockSystem.AddCharacterId(characterInfo);
        InGameManager.instance.combinationSystem.SubCharacter(characterInfo);
        DisableCharacter();

        return deletedCharacterInfo;
    }

    public void DisableCharacter()
    {
        characterInfo = null;
        character.OnHide();

        OnCanNotClick();
    }

    public void UpgradeStar()
    {
        var characterDataSheet = DataBase.Instance.characterDataSheet;
        if (characterDataSheet.TryGetCharacterOrigin(characterInfo.id, out var origin))
        {
            if (characterDataSheet.TryGetCharacterTribe(characterInfo.id, out var tribe))
            {
                characterInfo.IncreaseStar();

                int id = characterInfo.id;
                int star = characterInfo.star;

                var characterAbilityDataSheet = DataBase.Instance.characterAbilityDataSheet;
                if (characterAbilityDataSheet.TryGetCharacterAbilityData(id, star, out var abilityData))
                {
                    character.SetAbility(abilityData, origin, tribe);
                }

                InGameManager.instance.combinationSystem.AddCharacter(characterInfo);
                var particle = Instantiate(GameManager.instance.particleService.upgradeParticle, transform);
                particle.transform.Translate(new Vector3(0.0f, 0.0f, -1.0f));
                // 파티클 재생 함수
            }
        }
    }

    public void OnCanClick()
    {
        clickableImage.raycastTarget = true;
    }

    public void OnCanNotClick()
    {
        clickableImage.raycastTarget = false;
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
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

    public T GetArea<T>()
    {
        return this.GetComponentInParent<UISlot>().GetComponentInParent<T>();
    }

    public void CharacterMoveToUICharacter()
    {
        if (character != null)
        {
            character.transform.position = this.transform.position;
        }
    }

    public IEnumerator Co_FollowCharacter()
    {
        if (character != null)
        {
            while(true)
            {
                yield return new WaitForEndOfFrame();

                var positionToMove = Vector2.Lerp(character.transform.position, this.transform.position, 0.05f);
                character.transform.position = new Vector3(positionToMove.x, positionToMove.y, InGameService.Z_VALUE_OF_PAWN);

                if (Mathf.Abs((character.transform.position - this.transform.position).y) < 0.01 )
                {
                    break;
                }
            }
        }
    }

    public void FollowCharacter()
    {
        if (character != null)
        {
            character.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, InGameService.Z_VALUE_OF_PAWN);
        }
    }
}
