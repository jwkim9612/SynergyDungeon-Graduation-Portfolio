using LitJson;
using System;
using System.IO;
using System.Text;
using UnityEngine;

[Serializable]
public class JsonDataManager : MonoSingleton<JsonDataManager>
{
    public void Initialize()
    {

    }

    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
        Debug.Log(Application.persistentDataPath.ToString());
    }

    public string ObjectToJson(object obj)
    {
        return JsonMapper.ToJson(obj);
    }

    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        //파일이 없으면
        if (!File.Exists(string.Format("{0}/{1}.json", loadPath, fileName)))
        {
            return default;
        }

        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();

        string jsonData = Encoding.UTF8.GetString(data);
        return JsonMapper.ToObject<T>(jsonData);
    }

    public T LoadJson<T>(string String)
    {
        return JsonMapper.ToObject<T>(String);
    }
}
