using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using LitJson;
public static class TestItem 
{
    //private static int playerStep;
    //private static string time;
    private static GameDataItem dataItem;
    private static List<GameDataItem> list = new List<GameDataItem>();
    public static void Temp(int playerStep, string time,string jsonPath)
    {
        dataItem = new GameDataItem(playerStep, time);

        list.Add(dataItem);
        Debug.Log("-----dataList:" + list.Count);
        string str = JsonMapper.ToJson(list);

        //File.Delete(jsonPath);
        StreamWriter sw = new StreamWriter(jsonPath);
        sw.Write(str);
        sw.Close();
    }
}
