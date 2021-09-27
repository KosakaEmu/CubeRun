using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;
public class StartPanelModel : MonoBehaviour
{
    private List<GameGoldNum> list_GoldNumData = new List<GameGoldNum>();

    private int GoldNum=0;

    public int GetGoldNum(string fileName)
    {
        //list_GoldNumData = JsonTool.JsonToObject<GameGoldNum>(fileName);
        list_GoldNumData = JsonTool.JsonToObjectAndroid<GameGoldNum>(fileName);
        for (int i = 0; i < list_GoldNumData.Count; i++)
        {
            GoldNum = list_GoldNumData[i].GoldNum;
            
        }
        return GoldNum;
    }
    public List<ShopItem> GetShopData(string fileName)
    {
        //return JsonTool.JsonToObject<ShopItem>(fileName);
        return JsonTool.JsonToObjectAndroid<ShopItem>(fileName);

    }
}
