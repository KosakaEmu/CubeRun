using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelView : MonoBehaviour
{

    private Button button_SoundPlay;
    private Button button_SoundPause;
    private Button button_Start;
    private Button button_Rank;
    private Button button_Shop;
    private Button button_Setting;
    private Button button_Exit;

    private Transform transform_ShopPanel;
    private Transform transform_RankPanel;
    private Transform transform_ExitPanel;

    private Transform transform_SettingPanel;
    private Transform transform_GamePanel;

    private AudioSource audio_Rank;
    private AudioClip audioClip_OnClick;
    private Text text_GoldNum;

    public Button Button_SoundPlay { get => button_SoundPlay; }
    public Button Button_SoundPause { get => button_SoundPause;  }
    public Button Button_Start { get => button_Start; }
    public Button Button_Rank { get => button_Rank; }
    public Button Button_Shop { get => button_Shop; }
    public Button Button_Setting { get => button_Setting; }
    public Button Button_Exit { get => button_Exit; }
    public Transform Transform_ShopPanel { get => transform_ShopPanel; }
    public Transform Transform_RankPanel { get => transform_RankPanel; }
    public Transform Transform_SettingPanel { get => transform_SettingPanel; }
    public Transform Transform_GamePanel { get => transform_GamePanel;  }
    public Transform Transform_ExitPanel { get => transform_ExitPanel; }

    public Text Text_GoldNum { get => text_GoldNum; set => text_GoldNum = value; }
    public AudioSource Audio_Rank { get => audio_Rank;  }
    public AudioClip AudioClip_OnClick { get => audioClip_OnClick; }

    void Awake()
    {
        button_SoundPlay = transform.Find("Sound").GetComponent<Button>();
        button_SoundPause = transform.Find("NotSound").GetComponent<Button>();
        button_Start = transform.Find("Start").GetComponent<Button>();
        button_Rank = transform.Find("Rank").GetComponent<Button>();
        button_Shop = transform.Find("Shop").GetComponent<Button>();
        button_Setting = transform.Find("Setting").GetComponent<Button>();
        button_Exit = transform.Find("Exit").GetComponent<Button>();

        transform_ShopPanel = transform.Find("ShopPanel").transform;
        transform_RankPanel = transform.Find("RankPanel").transform;
        transform_SettingPanel = transform.Find("SettingPanel").transform;
        transform_ExitPanel = transform.Find("ExitPanel").transform;
        transform_GamePanel = transform.parent.Find("GamePanel").transform;

        text_GoldNum = transform.Find("Gold/Gem_num").GetComponent<Text>();

        audioClip_OnClick = Resources.Load<AudioClip>("GameSound/ButtonClickSound");
        //audio_Rank = transform.Find("Rank/Audio_Rank").GetComponent<AudioSource>();

    }

    
}
