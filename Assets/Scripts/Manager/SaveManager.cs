using GameSparks.Api.Requests;
using GameSparks.Core;
using LitJson;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    private InGameSaveData _inGameSaveData;
    public InGameSaveData inGameSaveData { get { return _inGameSaveData; } }
    public bool IsLoadedData { get; set; }


    [SerializeField]
    public List<int> equippedRuneIdsSaveData;
    string equippedRuneIdsSaveDataPath;

    public void Initialize()
    {
        _inGameSaveData = new InGameSaveData();

        equippedRuneIdsSaveDataPath = Path.Combine(Application.persistentDataPath, "EquippedRuneIds.json");
        IntializeEquippedRuneIdsSaveData();
    }

    private void IntializeEquippedRuneIdsSaveData()
    {
        equippedRuneIdsSaveData = new List<int>(RuneService.NUMBER_OF_RUNE_SOCKETS);
        
        for(int i = 0; i < RuneService.NUMBER_OF_RUNE_SOCKETS; ++i)
        {
            equippedRuneIdsSaveData.Add(-1);
        }

        bool result = LoadEquippedRuneIdsSaveData();
        if (!result)
            SaveEquippedRuneIds();
    }

    public void SaveEquippedRuneIds()
    {
        JsonDataManager.Instance.CreateJsonFile(Application.persistentDataPath, "EquippedRuneIds", JsonDataManager.Instance.ObjectToJson(equippedRuneIdsSaveData));
        Debug.Log("EquippedRuneIds Save Done !");
    }

    public void SetEquippedRuneIdsSaveData(int socketId, int runeId)
    {
        equippedRuneIdsSaveData[socketId] = runeId;
    }

    public void SetEquippedRuneIdsSaveDataByRelease(int socketId)
    {
        equippedRuneIdsSaveData[socketId] = -1;
    }

    public void SetInGameData()
    {
        _inGameSaveData.Coin = InGameManager.instance.playerState.coin;
        _inGameSaveData.Level = InGameManager.instance.playerState.level ;
        _inGameSaveData.Exp = InGameManager.instance.playerState.exp;
        _inGameSaveData.Chapter = StageManager.Instance.currentChapter;
        _inGameSaveData.Wave = StageManager.Instance.currentWave;
        _inGameSaveData.CharacterAreaInfoList = InGameManager.instance.draggableCentral.uiCharacterArea.GetAllCharacterInfo();
        _inGameSaveData.PrepareAreaInfoList = InGameManager.instance.draggableCentral.uiPrepareArea.GetAllCharacterInfo();
        _inGameSaveData.AbilityEffectSaveDataList = InGameManager.instance.backCanvas.uiMainMenu.uiAbilityEffectList.GetSaveData();
        _inGameSaveData.EventProbability = 10;
    }

    public void InitializeInGameData()
    {
        _inGameSaveData.Coin = InGameService.DEFAULT_COIN;
        _inGameSaveData.Level = InGameService.DEFAULT_LEVEL;
        _inGameSaveData.Exp = InGameService.DEFAULT_EXP;
        _inGameSaveData.Chapter = StageManager.Instance.currentChapter;
        _inGameSaveData.Wave = StageManager.Instance.currentWave;
        _inGameSaveData.CharacterAreaInfoList = new List<CharacterInfo>();
        _inGameSaveData.PrepareAreaInfoList = new List<CharacterInfo>();

        if(PotionManager.Instance.HasPotionInUse())
        {
            _inGameSaveData.AbilityEffectSaveDataList = new List<AbilityEffectSaveData>() { PotionManager.Instance.GetAbilityEffectSaveData() };
        }
        else
        {
            _inGameSaveData.AbilityEffectSaveDataList = new List<AbilityEffectSaveData>();
        }

        _inGameSaveData.EventProbability = InGameService.DEFAULT_EVENT_PROBABILITY;
    }

    public bool LoadEquippedRuneIdsSaveData()
    {
        //파일이 없으면
        if (!File.Exists(equippedRuneIdsSaveDataPath))
        {
            Debug.Log(equippedRuneIdsSaveDataPath);
            return false;
        }

        FileStream fileStream = new FileStream(string.Format(equippedRuneIdsSaveDataPath), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();

        string jsonData = Encoding.UTF8.GetString(data);
        equippedRuneIdsSaveData = JsonMapper.ToObject<List<int>>(jsonData);
        return true;
    }

    public void CheckHasInGameData()
    {
        new LogEventRequest()
           .SetEventKey("HasInGameData")
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   bool result = (bool)(response.ScriptData.GetBoolean("Result"));
                   if (result)
                   {
                       MainManager.instance.frontCanvas.ShowAskInGameContinue();
                   }
                   else
                   {
                       IsLoadedData = false;
                   }
               }
               else
               {
                   Debug.Log("Error CheckHasInGameData");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    public void RemoveInGameData()
    {
        new LogEventRequest()
            .SetEventKey("RemoveInGameData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success InGame Data Remove !");
                    IsLoadedData = false;
                }
                else
                {
                    Debug.Log("Error Data Remove !");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    // 최초 게임 진입 시에 초기 값으로 해서 저장
    // 이후에는 관련된 프로퍼티 변경할 때마다 저장
    public void SaveInGameData()
    {
        new LogEventRequest()
            .SetEventKey("SaveInGameData")
            .SetEventAttribute("Chapter", _inGameSaveData.Chapter)
            .SetEventAttribute("Wave", _inGameSaveData.Wave)
            .SetEventAttribute("Coin", _inGameSaveData.Coin)
            .SetEventAttribute("Level", _inGameSaveData.Level)
            .SetEventAttribute("Exp", _inGameSaveData.Exp)
            .SetEventAttribute("CharacterAreaInfoList", JsonDataManager.Instance.ObjectToJson(_inGameSaveData.CharacterAreaInfoList))
            .SetEventAttribute("PrepareAreaInfoList", JsonDataManager.Instance.ObjectToJson(_inGameSaveData.PrepareAreaInfoList))
            .SetEventAttribute("AbilityEffectSaveDataList", JsonDataManager.Instance.ObjectToJson(_inGameSaveData.AbilityEffectSaveDataList))
            .SetEventAttribute("EventProbability", _inGameSaveData.EventProbability)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success InGame Data Save !");
                }
                else
                {
                    Debug.Log("Error Data Save !");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void LoadInGameDataAndLoadInGameScene()
    {
        new LogEventRequest()
            .SetEventKey("LoadInGameData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    GSData scriptData = response.ScriptData.GetGSData("InGameData");

                    var data = new InGameSaveData
                    {

                        Chapter = (int)scriptData.GetInt("InGameChapter"),
                        Wave = (int)scriptData.GetInt("InGameWave"),
                        Coin = (int)scriptData.GetInt("InGameCoin"),
                        Level = (int)scriptData.GetInt("InGameLevel"),
                        Exp = (int)scriptData.GetInt("InGameExp"),
                        CharacterAreaInfoList = JsonMapper.ToObject<List<CharacterInfo>>(scriptData.GetString("InGameCharacterAreaInfoList")),
                        PrepareAreaInfoList = JsonMapper.ToObject<List<CharacterInfo>>(scriptData.GetString("InGamePrepareAreaInfoList")),
                        AbilityEffectSaveDataList = JsonMapper.ToObject<List<AbilityEffectSaveData>>(scriptData.GetString("InGameAbilityEffectSaveDataList")),
                        EventProbability = (int)scriptData.GetInt("InGameEventProbability")
                    };

                    _inGameSaveData = data;
                    Debug.Log("Player Data Load Successfully !");
                    Debug.Log($"Chapter : {_inGameSaveData.Chapter}, Wave : {_inGameSaveData.Wave}, Coin : {_inGameSaveData.Coin}, Level : {_inGameSaveData.Level}, Exp : {_inGameSaveData.Exp}");

                    IsLoadedData = true;

                    StageManager.Instance.SetChapterData(data.Chapter);
                    StageManager.Instance.SetCurrentWaveAndSetCurrentStage(_inGameSaveData.Wave);
                    GameManager.instance.LoadGameAndLoadInGameScene();
                }
                else
                {
                    Debug.Log("Error Player Data Load");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }
}
