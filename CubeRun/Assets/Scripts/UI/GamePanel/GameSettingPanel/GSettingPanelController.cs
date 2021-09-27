using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GSettingPanelController : MonoBehaviour
{
    private GSettingPanelView m_GSettingPanelView;

    void Start()
    {
        m_GSettingPanelView = gameObject.GetComponent<GSettingPanelView>();

        m_GSettingPanelView.Button_Resume.onClick.AddListener(ResumeGame);
        m_GSettingPanelView.Button_Close.onClick.AddListener(ResumeGame);
        m_GSettingPanelView.Button_Exit.onClick.AddListener(Exit);
        m_GSettingPanelView.Button_SettingSon.onClick.AddListener(ShowSettingPanel);

        gameObject.SetActive(false);

    }
    /// <summary>
    /// ��ͣ��Ϸ
    /// </summary>
    private void ResumeGame()
    {
        //AudioSource.PlayClipAtPoint(m_GSettingPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_GSettingPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        gameObject.SetActive(false);
        GameM.Instance.ContinueGame();
    }
    /// <summary>
    /// �˳���Ϸ�ص�������
    /// </summary>
    private void Exit()
    {
        //AudioSource.PlayClipAtPoint(m_GSettingPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_GSettingPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);


        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// ��ʾ���ý���
    /// </summary>
    private void ShowSettingPanel()
    {
        //AudioSource.PlayClipAtPoint(m_GSettingPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_GSettingPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        Debug.Log("ִ������");
        m_GSettingPanelView.Transform_SettingSon.gameObject.SetActive(true);
    }
}
