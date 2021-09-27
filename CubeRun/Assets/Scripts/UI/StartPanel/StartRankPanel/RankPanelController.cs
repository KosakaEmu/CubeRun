using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class RankPanelController : MonoBehaviour
{
    private static RankPanelController instance;
    private RankPanelView m_RankPanelView;

    //private string jsonPath;
    private string jsonFileName;
    private List<RankItem> list_RankItem;
    private List<int> list_SortRankScore;
    private object[,] array_RankItem;
    
    private List<GameObject> list_tempG;

    private static int index = 0;
    //private Dictionary<int, int> dic_SortRankScore = new Dictionary<string, int>();

    public static RankPanelController Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        FindInit();
        RankDataFileExists();
        gameObject.SetActive(false);

    }
    private void FindInit()
    {
        m_RankPanelView = gameObject.GetComponent<RankPanelView>();
        m_RankPanelView.Button_Colse.onClick.AddListener(ClosePanel);
        //jsonPath = Application.dataPath + @"\Resources\Json\GameRankData.txt";
        jsonFileName = "GameRankData.txt";
        list_RankItem = new List<RankItem>();
        list_tempG = new List<GameObject>();
        list_SortRankScore = new List<int>();
        //dic_SortRankScore ;
        
    }
    /// <summary>
    /// 判断排行榜数据文件是否存在
    /// 若存在则显示数据
    /// 若不存在显示不存在排行的小提示
    /// </summary>
    private void RankDataFileExists()
    {
        FileInfo fi = new FileInfo(Application.persistentDataPath + "/" + jsonFileName);
        //JsonTool.DeletePersistentData(jsonFileName);
        if (fi.Exists)
        {
            m_RankPanelView.Tips.SetActive(false);
            CreatRankItem();
        }
        else
        {   
            //排行榜提示无名次
            m_RankPanelView.Tips.SetActive(true);
        }
    }
    private void ClosePanel()
    {
        //AudioSource.PlayClipAtPoint(m_RankPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_RankPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        gameObject.SetActive(false);
    }
    public void CreatRankItem()
    {
        list_tempG = new List<GameObject>();
        list_tempG.Clear();
        //list_RankItem = JsonTool.JsonToObject<RankItem>(jsonPath);
        
        list_RankItem = JsonTool.JsonToObjectAndroid<RankItem>(jsonFileName);
        array_RankItem = new object[list_RankItem.Count, 2];
        for (int i = 0; i < list_RankItem.Count; i++)
        {
            // Debug.Log(list_RankItem[i].RankScore);
            //list_SortRankScore.Add(list_RankItem[i].RankScore);
            //foreach (var item in dic_SortRankScore)
            //{
            //    if (item.Key == list_RankItem[i].RankScore)
            //    {

            //    }
            //Debug.Log("list_RankItem[i].RankScore:"+list_RankItem[i].RankScore);
            Debug.Log("list_RankItem[i].RankTime:" + list_RankItem[i].RankTime);
            //}
            //dic_SortRankScore.Add(list_RankItem[i].RankScore, list_RankItem[i].RankTime);

            //dic_SortRankScore.TryGetValue(list_SortRankScore[i], out tempRT);
            for (int j = 0; j < 2; j++)
            {
                if (j == 0)
                {
                    array_RankItem[i, j] = list_RankItem[i].RankScore;

                }
                else
                {
                    array_RankItem[i, j] = list_RankItem[i].RankTime;

                }
            }

        }
        object temp;
        object temp2;
        for (int i = 0; i < array_RankItem.GetLength(0); i++)
        {
            for (int j = i + 1; j < array_RankItem.GetLength(0); j++)
            {
                if (Comparer.Default.Compare(array_RankItem[i, 0], array_RankItem[j, 0]).Equals(-1))
                {
                    temp = array_RankItem[i, 0];
                    temp2 = array_RankItem[i, 1];
                    array_RankItem[i, 0] = array_RankItem[j, 0];
                    array_RankItem[i, 1] = array_RankItem[j, 1];
                    array_RankItem[j, 0] = temp;
                    array_RankItem[j, 1] = temp2;
                }
            }
        }

        int tempInt = 0;
        string tempString = null;
        int maxRankNum;
        if (array_RankItem.GetLength(0) > 20)
        {
            maxRankNum = 20;
        }
        else
        {
            maxRankNum = array_RankItem.GetLength(0);
        }
        for (int i = 0; i < maxRankNum; i++)
        {

            for (int j = 0; j < array_RankItem.GetLength(1); j++)
            {
                if (j == 0)
                {
                    tempInt = Convert.ToInt32(array_RankItem[i, j]);
                    //Debug.Log("tempInt" + tempInt);
                }
                else
                {
                    tempString = array_RankItem[i, j].ToString();
                    //Debug.Log("tempString" + tempString);

                }


            }
            //Debug.Log("tempInt" + tempInt);
            //Debug.Log("tempString" + tempString); 
            GameObject tempG = Instantiate(m_RankPanelView.Prefab_RankItem, m_RankPanelView.Tr_Gird);

            list_tempG.Add(tempG);
            list_tempG[i].GetComponent<RankItemController>().Init(tempInt, tempString);
            //Debug.Log(array_RankItem[i,0]);


        }
        //FileInfo fi = new FileInfo(Application.streamingAssetsPath+ "/GameRankData.txt");
        //if (fi.Length == 0)
        //{
        //    m_RankPanelView.Tips.SetActive(true);
        //}
        //else
        //{
            
        //    //dic_SortRankScore.Add("adada", 1);
        //    //Debug.Log(dic_SortRankScore);
        //    //for (int i = 0; i < list_SortRankScore.Count; i++)
        //    //{
        //    //    list_SortRankScore.Sort((x, y) => -x.CompareTo(y));
        //    //    Debug.Log("list_SortRankScore:" + list_SortRankScore[i]);
        //    //}
        //}
        

    }
}
