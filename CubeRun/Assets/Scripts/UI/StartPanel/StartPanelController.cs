using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.IO;
public class StartPanelController : MonoBehaviour
{
    private static StartPanelController instance;

    private StartPanelModel m_StartPanelModel;
    private StartPanelView m_StartPanelView;

    private Transform m_Transform;

    private bool isSoundPlay = true;
    private bool isSettingShow = true;

    private int currentGoldNum;

    //private string jsonPath_GameData;
    //private string jsonPath_ShopItemData;
    private string jsonFileName_GoldNum;
    private string jsonFileName_ShopData;

    public static StartPanelController Instance { get => instance;  }
    public int CurrentGoldNum { get => currentGoldNum; }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        FindInit();
        InitStartPanelGoldNum();
        //LoadShopPanelItem();
        //StartGame();
    }
    private void FindInit()
    {
        m_StartPanelView = gameObject.GetComponent<StartPanelView>();
        m_StartPanelModel = gameObject.GetComponent<StartPanelModel>();

        m_Transform = gameObject.transform;

        m_StartPanelView.Button_Start.onClick.AddListener(StartGameButton);

        m_StartPanelView.Button_Shop.onClick.AddListener(ShowShopPanel);
        m_StartPanelView.Button_Setting.onClick.AddListener(ShowSettingPanel);
        m_StartPanelView.Button_Rank.onClick.AddListener(ShowRankPanel);
        m_StartPanelView.Button_Exit.onClick.AddListener(ShowExitPanel);

        m_StartPanelView.Button_SoundPlay.onClick.AddListener(PlayOrPauseSound);
        m_StartPanelView.Button_SoundPause.onClick.AddListener(PlayOrPauseSound);

        m_StartPanelView.Button_SoundPause.gameObject.SetActive(false);
        //m_StartPanelView.Transform_ShopPanel.gameObject.SetActive(false);

        //jsonPath_GameData = Application.dataPath + @"\Resources\Json\GameDataJson.txt";

        //jsonPath_GameGoldNumData = Application.dataPath + @"\Resources\Json\GameGoldNumData.txt";
        //jsonPath_ShopItemData = Application.dataPath + @"\Resources\Json\GameShopData2.txt";
        //jsonPath_GameData = Application.persistentDataPath + "/GameDataJson.json";
        jsonFileName_GoldNum = "GameGoldNumData.txt";
        //jsonFileName_ShopData = "GameShopData2.txt";
    }
    void Update() 
    {
        //StartGame();
    }
    
    /// <summary>
    /// 主界面“开始游戏”按钮控制逻辑
    /// </summary>
    private void StartGameButton()
    {
        //AudioSource.PlayClipAtPoint(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        m_Transform.gameObject.SetActive(false);
        AudioManager.Instance.StopAudio("startGameUIBGM");
        m_StartPanelView.Transform_GamePanel.gameObject.SetActive(true);
        StartTextPanelController.Instance.PlayTextEffect();

        MapManager.Instance.CreateMapTile(0);
        PlayerManager.Instance.CreatePlayer();
        Player.Instance.IsControl = false;
        
        Player.Instance.SetPlayerPosition();
        string goldNum = m_StartPanelView.Text_GoldNum.text;
        Turret.IsPlayNotFound = true;
        SetCameraMove.Instance.StopCameraAutoMove();
        SetCameraMove.Instance.SetCameraPos();
        StartPanelMapManager.Instance.GameStart();
        StartPanelMapManager.Instance.MapList.Clear();
        m_StartPanelView.Transform_GamePanel.gameObject.SendMessage("GamePanelGetGoldNum", int.Parse(goldNum.Substring(0, goldNum.Length - 4)));
        Time.timeScale = 0;
        
        //Invoke("DelayStartGame", 4f);
    }

    /// <summary>
    /// 主界面右下角“设置面板”打开
    /// </summary>
    private void ShowSettingPanel()
    {
        //AudioSource.PlayClipAtPoint(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        if (isSettingShow)
        {
            m_StartPanelView.Transform_SettingPanel.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 主界面右下角“退出游戏”面板打开
    /// </summary>
    private void ShowExitPanel()
    {
        //AudioSource.PlayClipAtPoint(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        m_StartPanelView.Transform_ExitPanel.gameObject.SetActive(true);
        
    }

    /// <summary>
    /// 主界面左下角“商店”面板打开
    /// </summary>
    private void ShowShopPanel()
    {
        //AudioSource.PlayClipAtPoint(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        m_StartPanelView.Transform_ShopPanel.gameObject.SetActive(true);
        StartPanelShopController.Instance.InitShopPanelInOpening();
        //StartPanelShopController.Instance.OpenPanelInit();
    }
    /// <summary>
    /// 主界面下方“排行榜”面板打开
    /// </summary>
    public void ShowRankPanel()
    {
        //AudioSource.PlayClipAtPoint(m_StartPanelView.AudioClip_OnClick,GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        m_StartPanelView.Transform_RankPanel.gameObject.SetActive(true);
        Transform tr= m_StartPanelView.Transform_RankPanel.Find("BackGound/Scroll/Grid").transform;
        for (int i = 0; i < tr.childCount; i++)
        {
            Destroy(tr.GetChild(i).gameObject);
        }
        FileInfo fi = new FileInfo(Application.persistentDataPath + "/" + "GameRankData.txt");
        //Debug.Log("fi.Exists:" + fi.Exists);
        
        if (fi.Exists)
        {
            RankPanelController.Instance.CreatRankItem();
        }
        
    }
    /// <summary>
    /// 主界面左上角“声音”按钮控制逻辑
    /// </summary>
    private void PlayOrPauseSound()
    {
        //AudioSource.PlayClipAtPoint(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_StartPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        if (isSoundPlay)
        {
            AudioManager.Instance.PausedStartPanelBGM();
            isSoundPlay = false;
            m_StartPanelView.Button_SoundPause.gameObject.SetActive(true);
            m_StartPanelView.Button_SoundPlay.gameObject.SetActive(false);
        }
        else
        {
            AudioManager.Instance.PlayStartPanelBGM();
            isSoundPlay = true;
            m_StartPanelView.Button_SoundPlay.gameObject.SetActive(true);
            m_StartPanelView.Button_SoundPause.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 主界面右上角金币数量数据初始化
    /// </summary>
    public void InitStartPanelGoldNum()
    {
        int temp;
        //JsonTool.DeletePersistentData(jsonFileName_GoldNum);
        temp = m_StartPanelModel.GetGoldNum(jsonFileName_GoldNum);//传入金币数json文件名 获取数据
        if (m_StartPanelModel.GetGoldNum(jsonFileName_GoldNum) >= 100)
        {
            temp = 100;

        }
        m_StartPanelView.Text_GoldNum.text = temp.ToString() + "/100";
        currentGoldNum = temp;
    }
    /// <summary>
    /// 游戏过程中购买物品后金币数量的更新
    /// </summary>
    /// <param name="useGoldNum"></param>
    public void UpdataStartPanelGoldNum(int useGoldNum)
    {
        int tempNum = int.Parse(m_StartPanelView.Text_GoldNum.text.Substring(0, m_StartPanelView.Text_GoldNum.text.Length - 4));
        tempNum = tempNum - useGoldNum;
        m_StartPanelView.Text_GoldNum.text = tempNum.ToString() + "/100";
        currentGoldNum = tempNum;
    }
    //public void LoadShopPanelItem()
    //{
    //    //JsonTool.DeletePersistentData(jsonFileName_ShopData);
        
    //    for (int i = 0; i < m_StartPanelModel.GetShopData(jsonFileName_ShopData).Count; i++)
    //    {
    //        Debug.Log("GetShopData:" + m_StartPanelModel.GetShopData(jsonFileName_ShopData)[i].ShopName);
    //    }
    //    StartPanelShopController.Instance.LoadShopItem(m_StartPanelModel.GetShopData(jsonFileName_ShopData));

    //}
}
