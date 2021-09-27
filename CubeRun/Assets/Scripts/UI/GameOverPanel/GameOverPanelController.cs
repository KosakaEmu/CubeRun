using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;
using System;
public class GameOverPanelController : MonoBehaviour
{
    private static GameOverPanelController instance;
    private GameOverPanelView m_GameOverPanelView;
    //private List<GameDataItem> dataList = new List<GameDataItem>();
    private List<GameGoldNum> list_GoldNumData = new List<GameGoldNum>();
    
    private GameGoldNum dataGoldNum;
    private string jsonPath;
    private string jsonPath2;

    private string jsonFileName;
    private string jsonFileName2;
    private int playerStep;
    private string time;
    private static RankItem dataItem;
    private static List<RankItem> list = new List<RankItem>();
    //List<GameDataItem> dataList = new List<GameDataItem>();


    //GameDataItem dataItem;
    public static GameOverPanelController Instance { get => instance;  }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        m_GameOverPanelView = gameObject.GetComponent<GameOverPanelView>();
        gameObject.SetActive(false);
        m_GameOverPanelView.Button_Close.onClick.AddListener(ClosePanel);

        //jsonPath = Application.dataPath + @"\Resources\Json\GameRankData.txt";
        //jsonPath2 = Application.dataPath + @"\Resources\Json\GameGoldNumData.txt";
        jsonFileName = "GameRankData.txt";
        jsonFileName2 = Application.persistentDataPath + "/GameGoldNumData.txt";
    }
    private void ClosePanel()
    {
        //AudioSource.PlayClipAtPoint(m_GameOverPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_GameOverPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        gameObject.SetActive(false);
        StopAllCoroutines();
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
        Turret.IsPlayNotFound = false;
    }
    public void UpdataScore()
    {

        //int intTemp= int.Parse(m_GameOverPanelView.Text_score.text.Substring(0, m_GameOverPanelView.Text_score.text.Length));

        int intTemp = Player.Instance.PlayerStep;
        m_GameOverPanelView.Text_score.text = intTemp.ToString()+"m";
    }
    public void UpdataGoldNum(int getGoldNum)
    {
        m_GameOverPanelView.Text_GoldNum.text = getGoldNum.ToString();
        
    }
    public void ObjectToJsonGoldNum (int totalGetGoldNum)
    {


        dataGoldNum = new GameGoldNum(totalGetGoldNum);

        list_GoldNumData.Add(dataGoldNum);
       
        string str = JsonMapper.ToJson(list_GoldNumData);

        //File.Delete(jsonPath);
        StreamWriter sw = new StreamWriter(jsonFileName2);
        sw.Write(str);
        sw.Close();

    }
    public void ObjectToJsonRankTime()
    {
        
        
            
        playerStep = Player.Instance.PlayerStep;

        //int totalGoldNum = int.Parse(m_GameOverPanelView.Text_score.text.Substring(0, m_GameOverPanelView.Text_score.text.Length-1))
        //                    + tempGetGoldNum;
        //if (totalGoldNum >= 100)
        //{
        //    totalGoldNum = 100;
        //}
        DateTime nowTime = DateTime.Now;
        time= string.Format("{0}/{1}/{2}/{3}:{4}", nowTime.Year, nowTime.Month, nowTime.Day, nowTime.Hour, nowTime.Minute);
        //Debug.Log("Time(OTJRT):" + time);
        //TestItem.Temp(playerStep, time, jsonPath);
        Temp(playerStep, time, jsonFileName);
        //dataItem = new GameDataItem(playerStep, time);

        //dataList.Add(dataItem);
        //Debug.Log("-----dataList:" + dataList.Count);
        //string str= JsonMapper.ToJson(dataList);

        ////File.Delete(jsonPath);
        //StreamWriter sw = new StreamWriter(jsonPath);
        //sw.Write(str);
        //sw.Close();

    }
    
    public static void Temp(int playerStep, string time, string jsonFileName)
    {
        FileInfo fi = new FileInfo(Application.persistentDataPath+"/"+jsonFileName);
        //Debug.Log("fi lenth:" + fi.Length);
        if (!fi.Exists)
        {
            
            for (int i = 0; i < list.Count; i++)
            {
                dataItem = new RankItem(list[i].RankScore, list[i].RankTime);
            }
            dataItem = new RankItem(playerStep, time);
            
            Debug.Log("先执行没文件的"+"-RankScore-" + dataItem.RankScore+ "-RankTime-" + dataItem.RankTime);

            list.Add(dataItem);
            //Debug.Log("-----dataList:" + list.Count);
            string str = JsonMapper.ToJson(list);
            //Debug.Log("str:"+str);
            //File.Delete(Application.persistentDataPath + "/" + jsonFileName);
            //StreamWriter sw = new StreamWriter(jsonPath);

            //StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + jsonFileName, true);
            File.WriteAllText(Application.persistentDataPath + "/" + jsonFileName, str);

            //StreamWriter sw = File.AppendText(jsonPath);
            //sw.Write(str);
            //sw.Close();
            //if (File.Exists(jsonPath))
            //{
            //    sw = File.AppendText(jsonPath);
            //    sw=fi
            //    sw.Write(str);
            //    sw.Close();
            //}
            //else
            //{
            //    sw = File.CreateText(jsonPath);
            //    sw.Write(str);
            //    sw.Close();
            //}
        }
        else
        {
            //list = JsonTool.JsonToObject<RankItem>(jsonPath);
            list = JsonTool.JsonToObjectAndroid<RankItem>(jsonFileName);
            for (int i = 0; i < list.Count; i++)
            {
                dataItem = new RankItem(list[i].RankScore, list[i].RankTime);
            }
            dataItem = new RankItem(playerStep, time);
            
            list.Add(dataItem);
            for (int i = 0; i < list.Count; i++)
            {
                Debug.Log("执行有文件的:" +i+ "-RankScore-" + list[i].RankScore + "-RankTime-" + list[i].RankTime);

            }
            //Debug.Log("-----dataList:" + list.Count);
            string str = JsonMapper.ToJson(list);

            File.Delete(Application.persistentDataPath + "/" + jsonFileName);
            //StreamWriter sw = new StreamWriter(jsonPath);

            StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + jsonFileName, true);
            //StreamWriter sw = File.AppendText(jsonPath);
            sw.Write(str);
            sw.Close();
            
        }




    }

}
