using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ResourceTool 
{
    public static Dictionary<string, GameObject> LoadFolderAssets(string foldName,Dictionary<string, GameObject> dic)
    {

        GameObject[] tempPrefabs =Resources.LoadAll<GameObject>(foldName);
        for (int i = 0; i < tempPrefabs.Length; i++)
        {
            dic.Add(tempPrefabs[i].name, tempPrefabs[i]);
        }
        return dic;
    }
    public static GameObject GetAssetByName(string fileName,Dictionary<string,GameObject>dic)
    {
        GameObject temp;
        dic.TryGetValue(fileName, out temp);
        return temp;
    }






}
