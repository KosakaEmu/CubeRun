using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GamePanelController : MonoBehaviour
{
    private static GamePanelController instance;

    private GamePanelView m_GamePanelView;

    private int gamingGoldNum = 0;
    private int startPanelGoldNum;
 
    public static GamePanelController Instance { get => instance; set => instance = value; }
    

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        FindInit();

        gameObject.SetActive(false);

    }
    
    private void FindInit()
    {
        m_GamePanelView = gameObject.GetComponent<GamePanelView>();

        m_GamePanelView.Transform_SettingPanel.gameObject.SetActive(false);
        m_GamePanelView.Tr_CountDownTextPanel.gameObject.SetActive(false);

        m_GamePanelView.Button_Setting.onClick.AddListener(ShowOrHideSettingPanel);
        m_GamePanelView.Button_LeftBtn.onClick.AddListener(OnClickLeftBtn);
        m_GamePanelView.Button_RightBtn.onClick.AddListener(OnClickRightBtn);

        m_GamePanelView.Button_Setting.interactable = false;
    }
    /// <summary>
    /// 显示游戏核心玩法界面左上角设置面板
    /// 显示面板同时暂停游戏
    /// </summary>
    private void ShowOrHideSettingPanel()
    {
        //AudioSource.PlayClipAtPoint(m_GamePanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_GamePanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        m_GamePanelView.Transform_SettingPanel.gameObject.SetActive(true);
        GameM.Instance.PauseGame();//暂停游戏

    }
    /// <summary>
    /// 游戏进行界面的金币数量更新 数值由主界面传值得到
    /// </summary>
    /// <param name="goldNum">StartPanelController脚本StartGameButton()传值</param>
    public void GamePanelGetGoldNum(int goldNum)
    {
        m_GamePanelView.Text_GemNum.text = goldNum.ToString()+"/100";
        this.startPanelGoldNum = goldNum;

    }
    /// <summary>
    /// 吃到金币后更新金币数量显示 
    /// </summary>
    public void UpdataGoldNumShow()
    {
        //int index=m_GamePanelView.Text_GemNum.text.IndexOf("/");

        //string[] strArray = m_GamePanelView.Text_GemNum.text.Split('/');
        //for (int i = 0; i < strArray.Length; i++)
        //{
        //    Debug.Log(strArray[i]);

        //}
        //Debug.Log(index);

        startPanelGoldNum += 1;
        
        gamingGoldNum += 1;
        if (startPanelGoldNum >= 100)
        {
            startPanelGoldNum = 100;
        }
        
        m_GamePanelView.Text_GemNum.text = startPanelGoldNum.ToString()+"/100";
    }
    /// <summary>
    /// 播放输掉游戏动画
    /// 显示结束结算面板
    /// </summary>
    public void StartGameOverAnimaPlay()
    {
        StartCoroutine("GameOverAnimaPlay");
    }
    
    private IEnumerator GameOverAnimaPlay()
    {
        Instantiate<GameObject>(m_GamePanelView.Prefabs_GameOverAnima, transform);//创建游戏结束动画
        yield return new WaitForSecondsRealtime(3f);//动画时长3s
        ShowGameOverPanel();//显示游戏结束面板
        StopCoroutine("GameOverAnimaPlay");
    }
    /// <summary>
    /// 显示游戏结束面板
    /// 更新相关数据并存储json文件
    /// </summary>
    private void ShowGameOverPanel()
    {
        //Debug.Log("执行了吗2");
        m_GamePanelView.Tr_GameOverPanel.gameObject.SetActive(true);
        GameOverPanelController.Instance.UpdataScore();
        GameOverPanelController.Instance.UpdataGoldNum(gamingGoldNum);
        GameOverPanelController.Instance.ObjectToJsonRankTime();
        GameOverPanelController.Instance.ObjectToJsonGoldNum(startPanelGoldNum);
    }
    /// <summary>
    /// 游戏核心玩法界面左键控制角色逻辑
    /// </summary>
    private void OnClickLeftBtn()
    {
        Player.Instance.LeftBtnControl();
    }
    /// <summary>
    /// 游戏核心玩法界面右键控制角色逻辑
    /// </summary>
    private void OnClickRightBtn()
    {
        Player.Instance.RightBtnControl();
    }
    public void UpdateCurrentScore(int PlayerStep)
    {
        m_GamePanelView.Text_CurrentScore.text = PlayerStep.ToString()+"M";
    }
}
