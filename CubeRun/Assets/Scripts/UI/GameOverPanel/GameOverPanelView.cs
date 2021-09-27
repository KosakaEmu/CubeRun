using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelView : MonoBehaviour
{

    private Button button_Close;
    private Text text_score;
    private Text text_GoldNum;
    private AudioClip audioClip_OnClick;

    public Button Button_Close { get => button_Close;  }
    public Text Text_score { get => text_score; set => text_score = value; }
    public Text Text_GoldNum { get => text_GoldNum; set => text_GoldNum = value; }
    public AudioClip AudioClip_OnClick { get => audioClip_OnClick;  }

    void Awake()
    {
        button_Close = transform.Find("BackGound/Close").GetComponent<Button>();
        text_score = transform.Find("BackGound/ScoreNum").GetComponent<Text>();
        text_GoldNum = transform.Find("BackGound/GoldNum").GetComponent<Text>();
        audioClip_OnClick = Resources.Load<AudioClip>("GameSound/ButtonClickSound");

    }

}
