using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemController : MonoBehaviour
{
    private Button button_Buy;
    private Button button_Use;
    private Button button_Using;
    private Text text_ShopPrice;

    private bool isBuy=false;
    private bool isUse=false;
    private string prefabName;
    private int shopPrice;
    private int index;

    private AudioClip audioClip_OnClick;


    public bool IsBuy { get => isBuy; set => isBuy = value; }
    public bool IsUse { get => isUse; set => isUse = value; }
    public string PrefabName { get => prefabName; set => prefabName = value; }
    public int ShopPrice { get => shopPrice; set => shopPrice = value; }
    public AudioClip AudioClip_OnClick { get => audioClip_OnClick;  }

    void Awake()
    {
        //GameObject tempGameObject = Resources.Load<GameObject>(list_ShopItem[i].ShopName);
        //Instantiate<GameObject>(tempGameObject, transform);
        audioClip_OnClick = Resources.Load<AudioClip>("GameSound/ButtonClickSound");


        button_Use = transform.Find("Use").GetComponent<Button>();
        button_Buy = transform.Find("Buy").GetComponent<Button>();
        button_Using = transform.Find("Using").GetComponent<Button>();
        text_ShopPrice = transform.Find("Gold/GoldNum").GetComponent<Text>();
        button_Use.gameObject.SetActive(false);
        button_Using.gameObject.SetActive(false);

        button_Buy.onClick.AddListener(BuyOnClick);
        button_Use.onClick.AddListener(UseOnClick);
        
    }
    public void Init(string prefabName,int shopPrice,bool isBuy, bool isUse,int index)
    {
        this.prefabName = prefabName;
        this.shopPrice = shopPrice;
        this.isBuy = isBuy;
        this.isUse = isUse;
        this.index = index;
        gameObject.name = "ShopItem" + index;
        
        UpdateUIShow();
        


    }
    public void UpdateUIShow()
    {
        string tempStr = "Ыљаш:";
        //Debug.Log(shopPrice);
        //Debug.Log(text_ShopPrice);
        text_ShopPrice.text = tempStr + shopPrice.ToString();
        
        if (isBuy == false)
        {
            button_Use.gameObject.SetActive(false);
            button_Using.gameObject.SetActive(false);
        }
        else
        {
            
            button_Use.gameObject.SetActive(true);
            button_Buy.gameObject.SetActive(false);
            transform.Find("Gold").gameObject.SetActive(false);
            //Debug.Log(GameObject.Find("Canvas/StartPanel/ShopPanel"));
            //GameObject.Find("Canvas/StartPanel/ShopPanel").SendMessageUpwards("SavaShopBuyInfo", index);
            if (isUse == false)
            {
                button_Using.gameObject.SetActive(false);
                button_Using.interactable = true;
            }
            else
            {
                button_Use.gameObject.SetActive(false);
                button_Using.gameObject.SetActive(true);
                //GameObject.Find("Canvas/StartPanel/ShopPanel").SendMessageUpwards("RestAllUseButtonShow", index);
                PlayerManager.Instance.LoadUsingPlayer(prefabName);
                //Debug.Log("prefabName" + prefabName);
                button_Using.interactable = false;

            }
        }
        
    }

    private void BuyOnClick()
    {
        //AudioSource.PlayClipAtPoint(audioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(audioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        if (StartPanelController.Instance.CurrentGoldNum >= this.shopPrice)
        {
            isBuy = true;
            button_Use.gameObject.SetActive(true);
            button_Buy.gameObject.SetActive(false);
            transform.Find("Gold").gameObject.SetActive(false);
            GameObject.Find("Canvas/StartPanel/ShopPanel").SendMessageUpwards("SavaShopBuyInfo", index);
            GameObject.Find("Canvas/StartPanel").SendMessageUpwards("UpdataStartPanelGoldNum", this.shopPrice);
        }
        else
        {
            StartPanelShopController.Instance.gameObject.SendMessage("PromptMessageToBuy");
        }
        
        

    }
    private void UseOnClick()
    {
        Debug.Log(index);
        //AudioSource.PlayClipAtPoint(audioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(audioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        //button_Use.gameObject.SetActive(false);
        //button_Using.gameObject.SetActive(true);
        GameObject.Find("Canvas/StartPanel/ShopPanel").SendMessageUpwards("RestAllUseButtonShow", index);
        PlayerManager.Instance.LoadUsingPlayer(prefabName);

    }
    public void NormalShow()
    {
        if (isBuy)
        {
            isUse = false;
            button_Use.gameObject.SetActive(true);
            button_Using.gameObject.SetActive(false);
            button_Using.interactable = true;
            //transform.Find("Gold").gameObject.SetActive(true);

        }
    }
    public void AtiveShow()
    {
        isUse = true;

        button_Use.gameObject.SetActive(false);
        button_Using.gameObject.SetActive(true);
        button_Using.interactable = false;
    }
}
