using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameM : MonoBehaviour
{
    private static GameM instance;

    private List<GameSettingData> list_GSD;
    private string jsonPath;

    private string jsonFileName;
    

    private bool isCameraFollow=false;
    public static GameM Instance { get => instance;  }

    
    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }
    void Start()
    {
        list_GSD = new List<GameSettingData>();
        //jsonPath = Application.dataPath + @"\Resources\Json\GameSettingData.txt";
        jsonFileName =  "GameSettingData.txt";
        //PauseGame();
    }
    void Update()
    {
        if(isCameraFollow)
            SetCameraMove.Instance.CameraMove();
    }
    public void PauseGame()
    {
        Player.Instance.IsControl = false;
        Time.timeScale = 0;
        AudioManager.Instance.PausedBGM();
    }
    public void ContinueGame()
    {
        Player.Instance.IsControl = true;
        Time.timeScale = 1;
        AudioManager.Instance.ContinueBGM();
    }
    public void RestartGame()
    {

    }
    public void StartGame()
    {
        GamePanelView m_GamePanelView = GameObject.Find("GamePanel").GetComponent<GamePanelView>();
        Player.Instance.IsControl = true;
        isCameraFollow = true;
        Time.timeScale = 1;
        
        m_GamePanelView.Button_Setting.interactable = true;
        MapManager.Instance.StartFloorDownCoroutine();
        
        int f = Random.Range(0, 2);
        StartTextPanelController.Instance.StopTwoCoroutine();
        if (f == 0)
        {
            AudioManager.Instance.PlayAudio("playGameBGM01",1);
        }
        else
        {
            AudioManager.Instance.PlayAudio("playGameBGM02",2);

        }

    }
    public void ReturnMainMenu()
    {
        GamePanelController.Instance.gameObject.SetActive(false);
        StartPanelController.Instance.gameObject.SetActive(true);
        StartPanelController.Instance.InitStartPanelGoldNum();
    }
    private void LoadVolumeData()
    {
        //list_GSD = JsonTool.JsonToObject<GameSettingData>(jsonPath);
        
        list_GSD = JsonTool.JsonToObjectAndroid<GameSettingData>(jsonFileName);

        //list_GSD = JsonTool.JsonToObjectAndroid<GameSettingData>(jsonPath);

        //m_SettingPanelView.Slider_Volume.value = (float)list_GSD[0].VolumeValue;

    }
}
