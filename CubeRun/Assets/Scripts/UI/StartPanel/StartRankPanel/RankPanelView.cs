using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RankPanelView : MonoBehaviour
{
    private Button button_Colse;
    private Transform tr_Gird;
    private GameObject prefab_RankItem;
    private GameObject tips;
    private AudioClip audioClip_OnClick;

    public Button Button_Colse { get => button_Colse; }
    public Transform Tr_Gird { get => tr_Gird; }
    public GameObject Prefab_RankItem { get => prefab_RankItem; }
    public GameObject Tips { get => tips; set => tips = value; }
    public AudioClip AudioClip_OnClick { get => audioClip_OnClick;  }

    void Awake()
    {
        button_Colse = transform.Find("BackGound/Close").GetComponent<Button>();
        tr_Gird = transform.Find("BackGound/Scroll/Grid").transform;
        prefab_RankItem = Resources.Load<GameObject>("RankItem");
        tips = transform.Find("BackGound/Tips").gameObject;
        audioClip_OnClick = Resources.Load<AudioClip>("GameSound/ButtonClickSound");

    }


}
