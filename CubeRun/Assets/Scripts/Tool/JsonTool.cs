using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using LitJson;
using System.IO;
public sealed class JsonTool
{
    private static int index = 0;
    private string testStr;

    public static List<T> JsonToObject<T>(string fileName)
    {
        //string path = Application.dataPath + @"\Resources\Json\";
        List<T> tempList = new List<T>();
        string textAsset = File.ReadAllText(fileName);
        JsonData jsonData = JsonMapper.ToObject(textAsset);

        for (int i = 0; i < jsonData.Count; i++)
        {
            T item = JsonMapper.ToObject<T>(jsonData[i].ToJson());
            tempList.Add(item);
        }

        return tempList;
    }
    public static List<T> JsonToObjectAndroid<T>(string fileName)
    {
        
        string StreamingPath = Application.streamingAssetsPath + "/" + fileName; 
        string AndroidPath = Application.persistentDataPath + "/" + fileName;
        //File.Delete(AndroidPath);
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            File.Delete(Application.persistentDataPath + "/" + "GameGoldNumData.txt");
            File.Delete(Application.persistentDataPath + "/" + "GameRankData.txt");
            File.Delete(Application.persistentDataPath + "/" + "GameSettingData.txt");
            File.Delete(Application.persistentDataPath + "/" + "GameShopData2.txt");
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
        }
        //FileInfo fi = new FileInfo(AndroidPath);
        //Debug.Log("fi.Exists:" + fi.Exists);
        //WWW w = new WWW(StreamingPath);
        
        //GameObject.Find("Canvas/StartPanel/Text2").GetComponent<Text>().text = w.text;
        //if (fi.Exists)
        //{
        //    fi.Delete();

        //    Debug.Log("执行了吗");

        //}
        FileInfo fi2 = new FileInfo(AndroidPath);
        //GameObject.Find("Canvas/StartPanel/Text2").GetComponent<Text>().text = fi2.Exists.ToString();

        if (!fi2.Exists)
        {
            UnityWebRequest request = UnityWebRequest.Get(StreamingPath);
            //这个 StreamingPath=Application.streamingAssetsPath + "/" + "GameGoldNumData.txt";
            request.SendWebRequest();
            string tempstr = null;
            Debug.Log("request.downloadHandler.isDone:" + request.downloadHandler.isDone);
            while (true)
            {
                if (!request.downloadHandler.isDone)
                {
                    continue;
                }
                Debug.Log("request.downloadHandler.isDone2:" + request.downloadHandler.isDone);

                tempstr = request.downloadHandler.text;
                break;
            }
            File.WriteAllText(AndroidPath, tempstr);
            StreamReader sr = new StreamReader(AndroidPath);
            string tempstr2 = sr.ReadToEnd();
            sr.Close();

            List<T> tempList = new List<T>();
            JsonData jsonData = JsonMapper.ToObject(tempstr2);
            for (int i = 0; i < jsonData.Count; i++)//Json转换object 存入list
            {
                T item = JsonMapper.ToObject<T>(jsonData[i].ToJson());
                tempList.Add(item);
            }
            return tempList;
        }
        else
        {
            StreamReader sr = new StreamReader(AndroidPath);
            string tempSR = sr.ReadToEnd();
            //gameObject.GetComponent<Text>().text = "aaaaaa2222222222";

            //JsonToObject(tempSR);
            sr.Close();
            List<T> tempList = new List<T>();
            //string textAsset = File.ReadAllText(text);
            JsonData jsonData = JsonMapper.ToObject(tempSR);

            for (int i = 0; i < jsonData.Count; i++)
            {
                T item = JsonMapper.ToObject<T>(jsonData[i].ToJson());
                tempList.Add(item);
            }

            return tempList;

            //string path = Application.dataPath + @"\Resources\Json\";
            //List<T> tempList = new List<T>();

            ////StreamReader sr = new StreamReader(filePath);
            //string jsonStr = sr.ReadToEnd();
            //sr.Close();
            ////playerManager = JsonMapper.ToObject<PlayerManager>(jsonStr);
            //JsonData jsonData = JsonMapper.ToObject(jsonStr);
            //for (int i = 0; i < jsonData.Count; i++)
            //{
            //    T item = JsonMapper.ToObject<T>(jsonData[i].ToJson());
            //    tempList.Add(item);
            //}


        }
        //public List<T> JsonToObject<T>(string text)
        //{
        //    List<T> tempList = new List<T>();
        //    //string textAsset = File.ReadAllText(text);
        //    JsonData jsonData = JsonMapper.ToObject(text);

        //    for (int i = 0; i < jsonData.Count; i++)
        //    {
        //        T item = JsonMapper.ToObject<T>(jsonData[i].ToJson());
        //        tempList.Add(item);
        //    }

        //    return tempList;
        //}
        //public void ObjectToJson(string fileName,)
        //{


        //    List<GameDataItem> dataList = new List<GameDataItem>();


        //    GameDataItem dataItem;
        //    playerStep = Player.Instance.PlayerStep;

        //    //int totalGoldNum = int.Parse(m_GameOverPanelView.Text_score.text.Substring(0, m_GameOverPanelView.Text_score.text.Length-1))
        //    //                    + tempGetGoldNum;
        //    //if (totalGoldNum >= 100)
        //    //{
        //    //    totalGoldNum = 100;
        //    //}
        //    DateTime nowTime = DateTime.Now;
        //    time = string.Format("{0}/{1}/{2}/{3}:{4}", nowTime.Year, nowTime.Month, nowTime.Day, nowTime.Hour, nowTime.Minute);

        //    dataItem = new GameDataItem(playerStep, time);

        //    dataList.Add(dataItem);
        //    Debug.Log("-----dataList:" + dataList.Count);
        //    string str = JsonMapper.ToJson(dataList);

        //    //File.Delete(jsonPath);
        //    StreamWriter sw = new StreamWriter(jsonPath);
        //    sw.Write(str);
        //    sw.Close();

        //}
    }
    public static void ObjectToJson<T>(string fileName,List<T> list_data)
    {
        string filePath = Application.persistentDataPath +"/"+ fileName;
        File.Delete(filePath);
        string str = JsonMapper.ToJson(list_data);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(str);
        sw.Close();
    }
    public static void DeletePersistentData(string fileName)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;
        FileInfo fi = new FileInfo(filePath);
        
        if (fi.Exists)
        {
            File.Delete(filePath);
        }
    }
    IEnumerator LoadResourceCorotine(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        testStr = request.downloadHandler.text;
        
    }


}
