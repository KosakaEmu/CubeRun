using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GSettingPanelView : MonoBehaviour
{

    private Button button_Resume;
    private Button button_Close;
    private Button button_SettingSon;
    private Button button_Exit;

    private Transform transform_SettingSon;

    private AudioClip audioClip_OnClick;

    public Button Button_Resume { get => button_Resume;}
    public Button Button_Close { get => button_Close;  }
    public Button Button_SettingSon { get => button_SettingSon; }
    public Button Button_Exit { get => button_Exit;  }
    public Transform Transform_SettingSon { get => transform_SettingSon;  }
    public AudioClip AudioClip_OnClick { get => audioClip_OnClick; }


    void Awake()
    {
        button_Resume = gameObject.transform.Find("BackGound/Resume").GetComponent<Button>();
        button_Close = gameObject.transform.Find("BackGound/Close").GetComponent<Button>();
        button_SettingSon = gameObject.transform.Find("BackGound/Setting").GetComponent<Button>();
        button_Exit = gameObject.transform.Find("BackGound/Exit").GetComponent<Button>();

        transform_SettingSon = gameObject.transform.Find("BackGound/SettingPanelSon").transform;
        audioClip_OnClick = Resources.Load<AudioClip>("GameSound/ButtonClickSound");

    }

}
