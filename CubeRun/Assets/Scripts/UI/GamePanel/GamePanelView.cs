using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelView : MonoBehaviour
{

    private Button button_LeftBtn;
    private Button button_RightBtn;
    private Text text_GemNum;
    private Text text_CurrentScore;
    private Button button_Setting;
    private Transform transform_SettingPanel;
    private Transform tr_CountDownTextPanel;
    private GameObject prefabs_GameOverAnima;
    private Transform tr_GameOverPanel;

    private AudioClip audioClip_OnClick;



    public Text Text_GemNum { get => text_GemNum; set => text_GemNum = value; }
    public Button Button_Setting { get => button_Setting; }
    public Transform Transform_SettingPanel { get => transform_SettingPanel;  }
    public Transform Tr_CountDownTextPanel { get => tr_CountDownTextPanel;  }
    public GameObject Prefabs_GameOverAnima { get => prefabs_GameOverAnima;  }
    public Transform Tr_GameOverPanel { get => tr_GameOverPanel;  }
    public Button Button_LeftBtn { get => button_LeftBtn;  }
    public Button Button_RightBtn { get => button_RightBtn; }
    public AudioClip AudioClip_OnClick { get => audioClip_OnClick;  }
    public Text Text_CurrentScore { get => text_CurrentScore; set => text_CurrentScore = value; }

    void Awake()
    {

        button_LeftBtn = transform.Find("LeftButton").GetComponent<Button>();
        button_RightBtn = transform.Find("RightButton").GetComponent<Button>();
        text_GemNum = transform.Find("Gold/Gem_num").GetComponent<Text>();
        text_CurrentScore = transform.Find("CurrentScore").GetComponent<Text>();
        button_Setting = transform.Find("Setting").GetComponent<Button>();
        transform_SettingPanel = transform.Find("SettingPanel").transform;
        tr_CountDownTextPanel = transform.Find("CountDownTextPanel").transform;
        prefabs_GameOverAnima = Resources.Load<GameObject>("TextEffect/GameOverAnima");
        tr_GameOverPanel = GameObject.Find("Canvas/GameOverPanel").transform;

        audioClip_OnClick = Resources.Load<AudioClip>("GameSound/ButtonClickSound");

    }


}
