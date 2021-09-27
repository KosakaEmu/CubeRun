using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelShopView : MonoBehaviour
{
    private Button button_Left;
    private Button button_Right;
    private Button button_Close;
    private Transform tr_ShopItemM;
    private GameObject prefabs_PromptMessage;
    private AudioClip audioClip_OnClick;

    public Button Button_Left { get => button_Left; }
    public Button Button_Right { get => button_Right;  }
    public Button Button_Close { get => button_Close;  }
    public Transform Tr_ShopItemM { get => tr_ShopItemM;  }
    public GameObject Prefabs_PromptMessage { get => prefabs_PromptMessage;  }
    public AudioClip AudioClip_OnClick { get => audioClip_OnClick;  }

    void Awake()
    {
        button_Left = transform.Find("BackGound/Left").GetComponent<Button>();
        button_Right = transform.Find("BackGound/Right").GetComponent<Button>();
        button_Close = transform.Find("BackGound/Close").GetComponent<Button>();
        tr_ShopItemM = GameObject.Find("UIShopPanelPrefabsM").transform;
        prefabs_PromptMessage = Resources.Load<GameObject>("UI/PromptMessage");
        audioClip_OnClick = Resources.Load<AudioClip>("GameSound/ButtonClickSound");

    }

    void Update()
    {
        
    }
}
