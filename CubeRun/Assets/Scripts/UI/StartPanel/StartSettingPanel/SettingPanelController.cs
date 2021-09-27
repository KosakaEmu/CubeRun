using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System;
public class SettingPanelController : MonoBehaviour
{
    private SettingPanelView m_SettingPanelView;
    private bool isHelpShow=false;
    private float volumeValue;
    //private string jsonPath;
    private string jsonFileName;
    private string jsonSavaFilePath;
    private List<GameSettingData> list_GSD;
    void Start()
    {
        FindInit();
        LoadVolumeData();
        gameObject.SetActive(false);
        m_SettingPanelView.Transform_HelpPanel.gameObject.SetActive(false);
    }
    private void FindInit()
    {
        m_SettingPanelView = gameObject.GetComponent<SettingPanelView>();

        m_SettingPanelView.Button_SettingClose.onClick.AddListener(CloseSettingPanel);
        m_SettingPanelView.Button_Help.onClick.AddListener(ShowOrHideHelpPanel);
        m_SettingPanelView.Button_HelpClose.onClick.AddListener(ShowOrHideHelpPanel);
        m_SettingPanelView.Slider_Volume.onValueChanged.AddListener(delegate { ControlVolume(); });
        //jsonPath = Application.dataPath + @"\Resources\Json\GameSettingData.txt";

        jsonFileName = "GameSettingData.txt";
        jsonSavaFilePath = Application.persistentDataPath + "/GameSettingData.txt";

        list_GSD = new List<GameSettingData>();
    }
    /// <summary>
    /// ��ʼ��������������
    /// </summary>
    private void LoadVolumeData()
    {
        //list_GSD= JsonTool.JsonToObject<GameSettingData>(jsonPath);
        //JsonTool.DeletePersistentData(jsonFileName);
        list_GSD = JsonTool.JsonToObjectAndroid<GameSettingData>(jsonFileName);
        m_SettingPanelView.Slider_Volume.value = (float)list_GSD[0].VolumeValue;
    }
    /// <summary>
    /// �رա����á����
    /// </summary>
    private void CloseSettingPanel()
    {
        //AudioSource.PlayClipAtPoint(m_SettingPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_SettingPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        m_SettingPanelView.transform.gameObject.SetActive(false);
        SaveSettingPanelData();
    }
    /// <summary>
    /// ���桰���á����������
    /// </summary>
    private void SaveSettingPanelData()
    {
        list_GSD.Clear();
        string temp = volumeValue.ToString();
        GameSettingData gsd = new GameSettingData(double.Parse(temp));
        list_GSD.Add(gsd);
        JsonTool.ObjectToJson(jsonFileName, list_GSD);

        //string str = JsonMapper.ToJson(list_GSD);
        //File.Delete(jsonPath);
        //File.Delete(jsonSavaFilePath);
        //StreamWriter sw = new StreamWriter(jsonPath);
        //StreamWriter sw = new StreamWriter(jsonSavaFilePath);
        //sw.Write(str);
        //sw.Close();
    }
    /// <summary>
    /// ����������С
    /// </summary>
    private void ControlVolume()
    {
        volumeValue = m_SettingPanelView.Slider_Volume.value;
        AudioManager.Instance.ControlAudioVolume(volumeValue);
    }
    /// <summary>
    /// ��ʾ���������еġ�����������
    /// </summary>
    private void ShowOrHideHelpPanel()
    {
        //AudioSource.PlayClipAtPoint(m_SettingPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_SettingPanelView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        if (isHelpShow==false)
        {
            m_SettingPanelView.Transform_HelpPanel.gameObject.SetActive(true);
            isHelpShow = true;
        }
        else
        {
            m_SettingPanelView.Transform_HelpPanel.gameObject.SetActive(false);
            isHelpShow = false;
        } 
    }
    
}
