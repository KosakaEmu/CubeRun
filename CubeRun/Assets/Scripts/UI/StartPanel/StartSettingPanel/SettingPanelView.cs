using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelView : MonoBehaviour
{
    private Button button_SettingClose;
    private Button button_HelpClose;
    private Button button_Help;
    private Slider slider_Volume;
    private Transform transform_HelpPanel;
    private AudioClip audioClip_OnClick;

    public Button Button_SettingClose { get => button_SettingClose; }
    public Button Button_HelpClose { get => button_HelpClose; }
    public Button Button_Help { get => button_Help; }
    public Slider Slider_Volume { get => slider_Volume; }
    public Transform Transform_HelpPanel { get => transform_HelpPanel;  }
    public AudioClip AudioClip_OnClick { get => audioClip_OnClick; }

    void Awake()
    {
        button_SettingClose = transform.Find("BackGound/Close").GetComponent<Button>();
        button_Help = transform.Find("BackGound/HelpBtn").GetComponent<Button>();
        button_HelpClose = transform.Find("BackGound/HelpPanel/Help/Close").GetComponent<Button>();

        slider_Volume = transform.Find("BackGound/Volume").GetComponent<Slider>();

        transform_HelpPanel = transform.Find("BackGound/HelpPanel").GetComponent<Transform>();

        audioClip_OnClick = Resources.Load<AudioClip>("GameSound/ButtonClickSound");

    }
   
}
