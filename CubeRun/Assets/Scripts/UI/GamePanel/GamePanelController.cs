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
    /// ��ʾ��Ϸ�����淨�������Ͻ��������
    /// ��ʾ���ͬʱ��ͣ��Ϸ
    /// </summary>
    private void ShowOrHideSettingPanel()
    {
        //AudioSource.PlayClipAtPoint(m_GamePanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_GamePanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        m_GamePanelView.Transform_SettingPanel.gameObject.SetActive(true);
        GameM.Instance.PauseGame();//��ͣ��Ϸ

    }
    /// <summary>
    /// ��Ϸ���н���Ľ���������� ��ֵ�������洫ֵ�õ�
    /// </summary>
    /// <param name="goldNum">StartPanelController�ű�StartGameButton()��ֵ</param>
    public void GamePanelGetGoldNum(int goldNum)
    {
        m_GamePanelView.Text_GemNum.text = goldNum.ToString()+"/100";
        this.startPanelGoldNum = goldNum;

    }
    /// <summary>
    /// �Ե���Һ���½��������ʾ 
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
    /// ���������Ϸ����
    /// ��ʾ�����������
    /// </summary>
    public void StartGameOverAnimaPlay()
    {
        StartCoroutine("GameOverAnimaPlay");
    }
    
    private IEnumerator GameOverAnimaPlay()
    {
        Instantiate<GameObject>(m_GamePanelView.Prefabs_GameOverAnima, transform);//������Ϸ��������
        yield return new WaitForSecondsRealtime(3f);//����ʱ��3s
        ShowGameOverPanel();//��ʾ��Ϸ�������
        StopCoroutine("GameOverAnimaPlay");
    }
    /// <summary>
    /// ��ʾ��Ϸ�������
    /// ����������ݲ��洢json�ļ�
    /// </summary>
    private void ShowGameOverPanel()
    {
        //Debug.Log("ִ������2");
        m_GamePanelView.Tr_GameOverPanel.gameObject.SetActive(true);
        GameOverPanelController.Instance.UpdataScore();
        GameOverPanelController.Instance.UpdataGoldNum(gamingGoldNum);
        GameOverPanelController.Instance.ObjectToJsonRankTime();
        GameOverPanelController.Instance.ObjectToJsonGoldNum(startPanelGoldNum);
    }
    /// <summary>
    /// ��Ϸ�����淨����������ƽ�ɫ�߼�
    /// </summary>
    private void OnClickLeftBtn()
    {
        Player.Instance.LeftBtnControl();
    }
    /// <summary>
    /// ��Ϸ�����淨�����Ҽ����ƽ�ɫ�߼�
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
