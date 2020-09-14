using System;
using System.Collections.Generic;
using UnityEngine;

public enum SystemLanguage
{
    Korean, English,
}

public class LocalizeUtil
{
    private static LocalizeUtil _ins = null;
    public static LocalizeUtil Instance
    {
        get
        {
            if (_ins == null)
            {
                _ins = new LocalizeUtil();
            }
            return _ins;
        }
    }

    private string languageCode = "";

    private SystemLanguage currLanguage;
    private Dictionary<string, string> localText = new Dictionary<string, string>();

    public void Initialize()
	{
        Debug.Log("Init Language File");
        //SetLanguageCode(Application.systemLanguage);
    }

    private void LocalizeStringFromCSV(string assetName, Dictionary<string, string> dic)
    {

        List<Dictionary<string, object>> data = CSVReader.Read("Data/" + assetName);

		dic.Clear();

        for (var i = 0; i < data.Count; i++)
        {
			dic.Add(data[i]["KEY"].ToString(), data[i][languageCode].ToString());
		}

    }

    private void SetLanguageCode(SystemLanguage lang)
    {
        switch (lang)
        {
            case SystemLanguage.Korean:
                languageCode = "KO";
                break;
            default:
                languageCode = "EN";
                break;
        }

        //LocalizeStringFromCSV("AppComments", localText);
    }

    public int GetLocalizeINT(string vKey)
    {
        if (localText.ContainsKey(vKey) == true)
        {
            return Convert.ToInt32(localText[vKey]);
        }
        return 0;
    }

    public string GetLocalizeText(string vKey)
    {
		if (localText.ContainsKey(vKey) == true)
		{
			return localText[vKey];
		}
        return "";
    }

    public string GetLoadingTipText(string vKey)
    {
        if (localText.ContainsKey(vKey) == true)
        {
            return localText[vKey];
        }
        return "";
    }
}
