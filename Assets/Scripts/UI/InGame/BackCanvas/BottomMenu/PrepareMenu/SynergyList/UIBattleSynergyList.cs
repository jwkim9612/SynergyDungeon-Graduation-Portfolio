using System.Collections.Generic;
using UnityEngine;

public class UIBattleSynergyList : MonoBehaviour
{
    [SerializeField] private UIInGameSynergyInfo uiInGameSynergyInfo = null;
    private List<UITribe> uiTribes;
    private List<UIOrigin> uiOrigins;
    private SynergySystem synergySystem;

    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject inBattleParent = null;
    [SerializeField] private GameObject inPrepareParent = null;

    public void Initialize()
    {
        uiInGameSynergyInfo.Initialize();

        InitializeTribeList();
        InitializeOriginList();

        InGameManager.instance.gameState.OnBattle += MoveForBattle;
        InGameManager.instance.gameState.OnPrepare += MoveForPrepare;

        synergySystem = InGameManager.instance.synergySystem;
        synergySystem.OnTribeChanged += UpdateTribes;
        synergySystem.OnOriginChanged += UpdateOrigins;
        synergySystem.OnTribeChanged += UpdateSynergyListSize;
        synergySystem.OnOriginChanged += UpdateSynergyListSize;

        if (SaveManager.Instance.IsLoadedData)
        {
            InitializeByInGameSaveData(SaveManager.Instance.inGameSaveData.CharacterAreaInfoList);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!TransformService.ContainPos(transform as RectTransform, Input.mousePosition, cam))
            {
                if(uiInGameSynergyInfo.gameObject.activeSelf)
                {
                    uiInGameSynergyInfo.OnHide();
                }
            }
        }
    }

    private void MoveForBattle()
    {
        TransformService.SetParentAndMoveRelativeToParent(this.transform, inBattleParent);
    }

    private void MoveForPrepare()
    {
        TransformService.SetParentAndMoveRelativeToParent(this.transform, inPrepareParent);
    }

    private void InitializeTribeList()
    {
        uiTribes = new List<UITribe>();

        var tribes = this.GetComponentsInChildren<UITribe>();
        foreach (var tribe in tribes)
        {
            uiTribes.Add(tribe);
            tribe.Initialize();
            tribe.uiInGameSynergyInfo = this.uiInGameSynergyInfo;
            tribe.gameObject.SetActive(false);
        }
    }

    private void InitializeOriginList()
    {
        uiOrigins = new List<UIOrigin>();

        var origins = this.GetComponentsInChildren<UIOrigin>();
        foreach (var origin in origins)
        {
            uiOrigins.Add(origin);
            origin.Initialize();
            origin.uiInGameSynergyInfo = this.uiInGameSynergyInfo;
            origin.gameObject.SetActive(false);
        }
    }

    public void UpdateTribes()
    {
        int tribeIndex = 0;

        var tribes = synergySystem.appliedTribes;
        foreach(var tribe in tribes)
        {
            switch (tribe.Key)
            {
                case Tribe.None:
                    break;
                case Tribe.Beast:
                    if(tribe.Value <= 0)
                        uiTribes[tribeIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiTribes[tribeIndex].synergyImage.color = Color.white;
                    break;
                case Tribe.Devil:
                    if (tribe.Value <= 0)
                        uiTribes[tribeIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiTribes[tribeIndex].synergyImage.color = Color.white;
                    break;
                case Tribe.Dragon:
                    if (tribe.Value == 1 || tribe.Value == 4)
                        uiTribes[tribeIndex].synergyImage.color = Color.white;
                    else
                        uiTribes[tribeIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    break;
                case Tribe.Elemental:
                    if (tribe.Value <= 1)
                        uiTribes[tribeIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiTribes[tribeIndex].synergyImage.color = Color.white;
                    break;
                case Tribe.Elf:
                    if (tribe.Value <= 1)
                        uiTribes[tribeIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiTribes[tribeIndex].synergyImage.color = Color.white;
                    break;
                case Tribe.Human:
                    if (tribe.Value <= 1)
                        uiTribes[tribeIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiTribes[tribeIndex].synergyImage.color = Color.white;
                    break;
                case Tribe.Machine:
                    if (tribe.Value <= 1)
                        uiTribes[tribeIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiTribes[tribeIndex].synergyImage.color = Color.white;
                    break;
                case Tribe.Undead:
                    if (tribe.Value <= 0)
                        uiTribes[tribeIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiTribes[tribeIndex].synergyImage.color = Color.white;
                    break;
                default:
                    break;
            }

            var tribeDataSheet = DataBase.Instance.tribeDataSheet;
            if (tribeDataSheet == null)
            {
                Debug.LogError("Error tribeDataSheet is null");
                return;
            }

            if (tribeDataSheet.TryGetTribeData(tribe.Key, out var tribeData))
            {
                uiTribes[tribeIndex].SetImage(tribeData.Image);
                uiTribes[tribeIndex].SetTribe(tribeData.Tribe);
            }

            uiTribes[tribeIndex].gameObject.SetActive(true);
            ++tribeIndex;
        }

        for(int i = tribeIndex; i < uiTribes.Count; ++i)
        {
            uiTribes[i].gameObject.SetActive(false);
        }
    }

    public void UpdateOrigins()
    {
        int originIndex = 0;

        var origins = synergySystem.appliedOrigins;
        foreach (var origin in origins)
        {
            switch (origin.Key)
            {
                case Origin.None:
                    break;
                case Origin.Archer:
                    if (origin.Value <= 0)
                        uiOrigins[originIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiOrigins[originIndex].synergyImage.color = Color.white;
                    break;
                case Origin.Paladin:
                    if (origin.Value <= 1)
                        uiOrigins[originIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiOrigins[originIndex].synergyImage.color = Color.white;
                    break;
                case Origin.Thief:
                    if (origin.Value <= 1)
                        uiOrigins[originIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiOrigins[originIndex].synergyImage.color = Color.white;
                    break;
                case Origin.Warrior:
                    if (origin.Value <= 1)
                        uiOrigins[originIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiOrigins[originIndex].synergyImage.color = Color.white;
                    break;
                case Origin.Wizard:
                    if (origin.Value <= 1)
                        uiOrigins[originIndex].synergyImage.color = Color.gray - new Color(0, 0, 0, 0.5f);
                    else
                        uiOrigins[originIndex].synergyImage.color = Color.white;
                    break;
                default:
                    break;
            }

            var originDataSheet = DataBase.Instance.originDataSheet;
            if(originDataSheet == null)
            {
                Debug.LogError("Error originDataSheet is null");
                return;
            }

            if(originDataSheet.TryGetOriginData(origin.Key, out var originData))
            {

                uiOrigins[originIndex].SetImage(originData.Image);
                uiOrigins[originIndex].SetOrigin(originData.Origin);
            }

            uiOrigins[originIndex].gameObject.SetActive(true);
            ++originIndex;
        }

        for (int i = originIndex; i < uiOrigins.Count; ++i)
        {
            uiOrigins[i].gameObject.SetActive(false);
        }
    }

    private void UpdateSynergyListSize()
    {
        int activateSynergyCount = GetActivateSynergyCount();

        RectTransform rect = transform as RectTransform;

        float xOfanchorMax = InGameService.LEFT_PADDING_OF_SYNERGY_LIST_AT_ANCHOR + (InGameService.LENGTH_OF_SYNERGY_ICON_AT_ANCHOR * activateSynergyCount);
        //rect.anchorMax = new Vector2(xOfanchorMax, 1.0f);
        rect.anchorMax = new Vector2(xOfanchorMax, 0.97f);

        ///////////// anchor x 포지션 0으로 만들어주기.
        var position = rect.anchoredPosition;
        position.x = 0.0f;
        rect.anchoredPosition = position;
    }

    private int GetActivateSynergyCount()
    {
        int count = 0;

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                ++count;
            }
        }

        return count;
    }

    private void InitializeByInGameSaveData(List<CharacterInfo> characterInfoList)
    {
        foreach(var characterInfo in characterInfoList)
        {
            if (characterInfo == null)
            {
                continue;
            }

            synergySystem.AddCharacter(characterInfo);
        }
    }
}
