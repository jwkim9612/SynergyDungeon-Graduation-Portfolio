using GameSparks.Api.Requests;
using GameSparks.Core;
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoSingleton<RuneManager>
{
    public delegate void OnAddRuneDelegate(int runeId);
    public OnAddRuneDelegate OnAddRune { get; set; }
    public Dictionary<int, int> ownedRuneListById { get; set; }
    public Dictionary<RuneGrade, int> ownedRuneListByGrade { get; set; }
    public List<Rune> equippedRunes { get; set; }

    public void Initialize()
    {
        InitializeEquippedRunes();
        LoadOwnedRuneData();
    }

    private void InitializeEquippedRunes()
    {
        var equippedRuneIdsSaveData = SaveManager.Instance.equippedRuneIdsSaveData;

        equippedRunes = new List<Rune>();
        for (int i = 0; i < equippedRuneIdsSaveData.Count; ++i)
        {
            int equippedRuneId = equippedRuneIdsSaveData[i];
            if (equippedRuneId != -1)
            {
                var runeDataSheet = DataBase.Instance.runeDataSheet;
                if(runeDataSheet == null)
                {
                    Debug.LogError("Error runeDataSheet is null");
                    return;
                }

                if(runeDataSheet.TryGetRuneData(equippedRuneId, out var runeData))
                {
                    Rune rune = new Rune();
                    rune.SetRune(runeData);
                    equippedRunes.Add(rune);
                }
            }
            else
            {
                equippedRunes.Add(null);
            }  
        }
    }

    public void RemoveEquippedRune(int socketIndex)
    {
        if (socketIndex >= RuneService.NUMBER_OF_RUNE_SOCKETS)
            return;

        equippedRunes[socketIndex] = null;
    }

    public void SaveOwnedRunes()
    {
        new LogEventRequest()
            .SetEventKey("SaveOwnedRunes")
            .SetEventAttribute("Runes", JsonDataManager.Instance.ObjectToJson(ownedRuneListById))
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success InGame Data Save !");
                }
                else
                {
                    Debug.Log("Error OwnedRune Save !");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void AddRune(int runeId, int amount = 1)
    {
        new LogEventRequest()
            .SetEventKey("AddRune")
            .SetEventAttribute("RuneId", runeId)
            .SetEventAttribute("Amount", amount)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    AddRuneToRuneList(runeId);
                    Debug.Log("Success Add Rune!");
                }
                else
                {
                    Debug.Log("Error Add Rune!");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void RemoveRune(int runeId, bool isEquippedRune)
    {
        new LogEventRequest()
            .SetEventKey("RemoveRune")
            .SetEventAttribute("RuneId", runeId)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    RemoveRuneToRuneList(runeId);

                    var uiRunePage = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage;

                    if (isEquippedRune)
                    {
                        uiRunePage.uiEquippedRunes.RemoveRune(runeId);
                    }
                    else
                    {
                        uiRunePage.uiRunesOnRunePage.RemoveRune(runeId);
                    }

                    var uiRunesForCombination = uiRunePage.uiRuneCombination.uiRunesForCombination;
                    uiRunesForCombination.RemoveRune(runeId, isEquippedRune);
                    Debug.Log("Success RemoveRune!");
                }
                else
                {
                    Debug.Log("Error RemoveRune!");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void LoadOwnedRuneData()
    {
        new LogEventRequest()
            .SetEventKey("LoadOwnedRuneData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    bool result = (bool)response.ScriptData.GetBoolean("Result");
                    if(result)
                    {
                        GSData ownedRuneScriptData = response.ScriptData.GetGSData("OwnedRuneData");
                        JsonData ownedRuneJsonObject = JsonDataManager.Instance.LoadJson<JsonData>(ownedRuneScriptData.JSON);

                        ownedRuneListById = new Dictionary<int, int>();
                        ownedRuneListByGrade = new Dictionary<RuneGrade, int>();

                        foreach (var runeIdStrWithR in ownedRuneJsonObject.Keys)
                        {
                            string runeIdStr = runeIdStrWithR.Substring(1, 4);

                            int runeId = int.Parse(runeIdStr);
                            int NumOfRune = int.Parse(ownedRuneJsonObject[runeIdStrWithR].ToString());
                            ownedRuneListById.Add(runeId, NumOfRune);
                            AddRuneToRuneListByGrade(runeId, NumOfRune);
                        }
                    }
                    else
                    {
                        InitializeOwnedRuneData();
                    }
                }
                else
                {
                    Debug.Log("Error LoadOwnedRunes");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    private void AddRuneToRuneListByGrade(int runeId, int num = 1)
    {
        RuneGrade grade = RuneGrade.None;

        if(DataBase.Instance.runeDataSheet.TryGetRuneData(runeId, out var runeData))
        {
            grade = runeData.Grade;
        }

        if (ownedRuneListByGrade.ContainsKey(grade))
        {
            ownedRuneListByGrade[grade] += num;
        }
        else
        {
            ownedRuneListByGrade.Add(grade, num);
        }
    }

    private void RemoveRuneToRuneListByGrade(int runeId, int num = 1)
    {
        RuneGrade grade = RuneGrade.None;

        if (DataBase.Instance.runeDataSheet.TryGetRuneData(runeId, out var runeData))
        {
            grade = runeData.Grade;
        }

        if (ownedRuneListByGrade.ContainsKey(grade))
        {
            ownedRuneListByGrade[grade] -= num;
        }

        if(ownedRuneListByGrade[grade] <= 0)
        {
            ownedRuneListByGrade.Remove(grade);
        }
    }

    public void AddRuneToRuneList(int runeId)
    {
        if (ownedRuneListById.ContainsKey(runeId))
        {
            ++ownedRuneListById[runeId];
        }
        else
        {
            ownedRuneListById.Add(runeId, 1);
        }

        AddRuneToRuneListByGrade(runeId);

        OnAddRune(runeId);
    }

    public void RemoveRuneToRuneList(int runeId)
    {
        if(ownedRuneListById.ContainsKey(runeId))
        {
            if(ownedRuneListById[runeId] == 1)
            {
                ownedRuneListById.Remove(runeId);
            }
            else
            {
                --ownedRuneListById[runeId];
            }
        }
        else
        {
            Debug.Log("Error SubRune");
        }

        RemoveRuneToRuneListByGrade(runeId);
    }

    public Rune GetEquippedRuneOfOrigin(Origin origin)
    {
        Rune rune;

        switch (origin)
        {
            case Origin.Archer:
                rune = equippedRunes[RuneService.INDEX_OF_ARCHER_SOCKET];
                break;
            case Origin.Paladin:
                rune = equippedRunes[RuneService.INDEX_OF_PALADIN_SOCKET];
                break;
            case Origin.Thief:
                rune = equippedRunes[RuneService.INDEX_OF_THIEF_SOCKET];
                break;
            case Origin.Warrior:
                rune = equippedRunes[RuneService.INDEX_OF_WARRIOR_SOCKET];
                break;
            case Origin.Wizard:
                rune = equippedRunes[RuneService.INDEX_OF_WIZARD_SOCKET];
                break;
            default:
                rune = null;
                break;
        }

        return rune;
    }

    public void InitializeOwnedRuneData()
    {
        new LogEventRequest()
            .SetEventKey("InitializeOwnedRuneData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success Initialize OwnedRuneData !");
                    LoadOwnedRuneData();
                }
                else
                {
                    Debug.Log("Error Initialize OwnedRuneData !");
                }
            });
    }

    public void SetEquippedRune(Rune rune)
    {
        if (rune != null)
            equippedRunes[rune.runeData.SocketPosition] = rune;
        else
            Debug.LogError("Error SetEquippedRune");
    }

    public bool CanCombination()
    {
        foreach (var ownedRuneByGrade in ownedRuneListByGrade)
        {
            int numOfRune = ownedRuneByGrade.Value;

            if (numOfRune >= RuneService.NUMBER_OF_CAN_COMBINATION)
                return true;
        }

        return false;
    }
}
