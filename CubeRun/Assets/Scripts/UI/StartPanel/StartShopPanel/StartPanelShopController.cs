using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using LitJson;
using System.IO;
public class StartPanelShopController : MonoBehaviour
{
    private static StartPanelShopController instance;
    private StartPanelShopView m_StartPanelShopView;
    private StartPanelModel m_StartPanelModel;

    private Dictionary<string, GameObject> dic_ShopItem;
    private int current_UseBtnIndex=-1;


    private int current_SelectIndex;
    private int current_BuyAfterIndex=0;
    private List<GameObject> list_ShopPanel;
    private List<GameObject> list_ShopItemBuyBefore;
    private List<GameObject> list_ShopItemBuyAfter;
    private List<GameObject> tempList;
    private List<GameObject> originalList_ShopPanel;

    private List<ShopItem> tempShopItemList;

    private List<GameObject> list_ShopItemPrefas;
    private List<GameObject> list_ShopItemPrefasBefore;
    private List<GameObject> list_ShopItemPrefasAfter;
    private List<GameObject> originalList_ShopItemPrefas;
    private List<GameObject> tempList2;

    private List<GameObject> list_PromptMessage;
    
    //private List<int> list_MaskIndex;
    private int buy_Index = -1;
    private GameObject shopItemPanel;
    private bool test0000 = true;
    //private Sequence mySequence ;
    private bool isBuy;
    private bool isUse;

    private GameObject go_PromptMessage;
    //private string jsonPath;
    private string jsonFileName;
    
    public static StartPanelShopController Instance { get => instance;  }
    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        m_StartPanelModel = gameObject.transform.parent.GetComponent<StartPanelModel>(); 
        m_StartPanelShopView = gameObject.GetComponent<StartPanelShopView>();

        list_ShopPanel = new List<GameObject>();
        list_ShopItemBuyAfter = new List<GameObject>();
        tempList = new List<GameObject>();
        tempList2 = new List<GameObject>();
        tempShopItemList = new List<ShopItem>();
        //list_MaskIndex = new List<int>();
        list_ShopItemPrefas = new List<GameObject>();

        list_PromptMessage = new List<GameObject>();
        list_ShopItemPrefasAfter = new List<GameObject>();

        //ͨ����Դ�ļ�������������Ҫ��������Ʒ�������ֵ���
        dic_ShopItem = new Dictionary<string, GameObject>();
        dic_ShopItem = ResourceTool.LoadFolderAssets("ShopItemPrefabs", dic_ShopItem);
        //RestAllUseButtonShow(0);
        m_StartPanelShopView.Button_Left.onClick.AddListener(LeftButtonEvent);
        m_StartPanelShopView.Button_Right.onClick.AddListener(RightButtonEvent);
        m_StartPanelShopView.Button_Close.onClick.AddListener(CloseShopPanel);
        //mySequence = DOTween.Sequence();

        m_StartPanelShopView.Button_Left.gameObject.SetActive(false);
        //jsonPath = Application.dataPath + @"\Resources\Json\GameShopData2.txt";
        jsonFileName =  "GameShopData2.txt";
        //JsonTool.DeletePersistentData(jsonFileName);
        LoadShopItem(m_StartPanelModel.GetShopData(jsonFileName));
        //Your code here  
        //mySequence.Append(transform.DOMove(Vector3.right, 1).SetLoops(2, LoopType.Yoyo))
        //.Append(transform.DOMove(Vector3.up, 1).SetLoops(2, LoopType.Yoyo))
        //.OnComplete(() => {
        //    Debug.Log("Done");
        //});
        gameObject.SetActive(false);

    }
    /// <summary>
    /// �����̵�������еġ�ʹ�á���ťΪĬ��״̬ 
    /// ��ShopItemController�ű��е�UseOnClick��������
    /// </summary>
    private void RestAllUseButtonShow(int index)
    {
        if (current_UseBtnIndex == index) return;
        for (int i = 0; i < list_ShopPanel.Count; i++)
        {
            list_ShopPanel[i].GetComponent<ShopItemController>().NormalShow();
            if(originalList_ShopPanel[index]== list_ShopPanel[i])
            {
                list_ShopPanel[i].GetComponent<ShopItemController>().AtiveShow();
            }
        }
        current_UseBtnIndex = index;
    }
    
    void Update()
    {

    }
    /// <summary>
    /// ������Ʒ��Ʒ�ľ�������
    /// �����������ƷԤ���������ק��ת�Ĺ��� ���԰���Ʒ���ֳ�������Ԥ����
    /// һ���� ��Ʒ������� һ������Ʒ����Ԥ����
    /// </summary>
    /// <param name="list_ShopItem">����shopItem������</param>
    public void LoadShopItem(List<ShopItem> list_ShopItem)
    {
        tempShopItemList = list_ShopItem;
        int offset = 0; //�̵���Ʒ����λ��ƫ����
        float offset2 = 0;
        for (int i = 0; i < list_ShopItem.Count; i++)
        {
            //�������������Ʒ���Ԥ����
            GameObject tempGameObject = Resources.Load<GameObject>("ShopItem");
            shopItemPanel= Instantiate<GameObject>(tempGameObject, transform);
            
            //�������������λ��
            shopItemPanel.GetComponent<RectTransform>().localPosition = new Vector3(10.95f + offset, -69.5f, -152);
            list_ShopPanel.Add(shopItemPanel);

            list_ShopItemBuyBefore = new List<GameObject>();
            originalList_ShopPanel = new List<GameObject>();

            list_ShopItemPrefasBefore = new List<GameObject>();
            originalList_ShopItemPrefas = new List<GameObject>();

            foreach (var item in list_ShopPanel)
            {
                list_ShopItemBuyBefore.Add(item);
                originalList_ShopPanel.Add(item);
            }
            //ͨ��ResourceTool������ʹ���ֵ���� �����Ʒ����Ԥ����
            
            GameObject shopItemPrefabs= ResourceTool.GetAssetByName(list_ShopItem[i].ShopName, dic_ShopItem);

            GameObject tempPrefabs = Instantiate<GameObject>(shopItemPrefabs, m_StartPanelShopView.Tr_ShopItemM);
            tempPrefabs.name=tempPrefabs.name.Substring(0, tempPrefabs.name.Length - 7);
            
            tempPrefabs.transform.localPosition = new Vector3(0+offset2, 0, 0);
            tempPrefabs.transform.localRotation = Quaternion.Euler(new Vector3(-50, 150, 45));
            
            list_ShopItemPrefas.Add(tempPrefabs);
            foreach (var item in list_ShopItemPrefas)
            {
                list_ShopItemPrefasBefore.Add(item);
                originalList_ShopItemPrefas.Add(item);
            }
            shopItemPanel.GetComponent<ShopItemController>().Init(list_ShopItem[i].ShopName,list_ShopItem[i].ShopPrice, list_ShopItem[i].IsBuy, list_ShopItem[i].IsUse,i);
            
            if (offset < 890)
            {
                offset += 890;
            }
            if (offset2 < 0.8)
            {
                offset2 += 0.8f;
            }
            m_StartPanelShopView.Button_Left.gameObject.SetActive(false);//ÿ�μ�����������İ���
        }
    }
    /// <summary>
    /// �ر���Ʒ��� 
    /// ���¹������Ʒ˳��(���������Ʒ������˳���������)
    /// �����������
    /// ������ĺ����Ʒ״̬��json�ļ�
    /// </summary>
    private void CloseShopPanel()
    {
        //AudioSource.PlayClipAtPoint(m_StartPanelShopView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_StartPanelShopView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        //�����������
        current_SelectIndex = 0;
        m_StartPanelShopView.Button_Left.interactable = true;
        m_StartPanelShopView.Button_Right.interactable = true;
        m_StartPanelShopView.Button_Right.gameObject.SetActive(true);
        StopCoroutine("DelayLeftAndRightButton");
        DOTween.KillAll();
        DOTween.Clear(true);
        int offset = 0;
        float offset2 = 0;
        tempList.Clear();
        tempList2.Clear();
        tempShopItemList.Clear();


        for (int i = 0; i < list_ShopItemBuyAfter.Count; i++)
        {
            Debug.Log(list_ShopItemBuyAfter[i].name);
            tempList.Add(list_ShopItemBuyAfter[i]);
            tempList2.Add(list_ShopItemPrefasAfter[i]);
            Debug.Log(tempList2[i].name);
        }
        for (int i = 0; i < list_ShopItemBuyBefore.Count; i++)
        {
            Debug.Log(list_ShopItemBuyBefore[i]);

            tempList.Add(list_ShopItemBuyBefore[i]);
            tempList2.Add(list_ShopItemPrefasBefore[i]);
            Debug.Log(tempList2[i].name);

        }
        for (int i = 0; i < list_ShopPanel.Count; i++)
        {
            list_ShopPanel[i] = tempList[i];
            list_ShopItemPrefas[i] = tempList2[i];
            Debug.Log(tempList2[i].name);

            //list_ShopPanel[i].GetComponent<RectTransform>().localPosition = new Vector3(10.95f + offset, -69.5f, -152);

            list_ShopPanel[i].GetComponent<RectTransform>().localPosition = new Vector3(10.95f+ offset, -69.5f, -152);
            Debug.Log("list_ShopItemPrefas[i]:" + list_ShopItemPrefas[i]);
            list_ShopItemPrefas[i].transform.localPosition = new Vector3(0+ offset2, 0, 0);
            //Debug.Log("list_ShopItemPrefas[i]" + list_ShopItemPrefas[i].transform.localPosition);
            //Debug.Log("list_ShopItemPrefas[i].name" + list_ShopItemPrefas[i].name);
            //list_ShopPanel[i].GetComponent<RectTransform>().localPosition = new Vector3( offset, -69.5f, -152);
            ///Debug.Log("list_ShopPanel[i].possss:" + list_ShopPanel[i].transform.localPosition);
            list_ShopItemPrefas[i].transform.localRotation = Quaternion.Euler(new Vector3(-50, 150, 45));
            if (offset < 890)
            {
                offset += 890; 
            }
            if (offset2 < 0.8f)
            {
                offset2 += 0.8f;
            }
            //���޸Ĺ������ݱ��������ʵ����
            string name= list_ShopPanel[i].GetComponent<ShopItemController>().PrefabName;
            int price= list_ShopPanel[i].GetComponent<ShopItemController>().ShopPrice;
            bool isbuy= list_ShopPanel[i].GetComponent<ShopItemController>().IsBuy;
            bool isuse= list_ShopPanel[i].GetComponent<ShopItemController>().IsUse;
            ShopItem si = new ShopItem(name, price, isbuy, isuse);
            tempShopItemList.Add(si);
            
        }
        //������json�ļ���
        JsonTool.ObjectToJson(jsonFileName, tempShopItemList);

        gameObject.SetActive(false);
        
    }
    /// <summary>
    /// �̵���������л���Ʒ��ť�߼�
    /// dotween�����ı���Ʒ������ƷԤ�����λ��
    /// ���غ���ʾ���Ҽ�
    /// </summary>
    private void LeftButtonEvent()
    {
        //AudioSource.PlayClipAtPoint(m_StartPanelShopView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_StartPanelShopView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        //������ƷԤ֧�����ת�Ƕ�
        list_ShopItemPrefas[current_SelectIndex].transform.localRotation = Quaternion.Euler(new Vector3(-50, 150, 45));
        StartCoroutine("DelayLeftAndRightButton");

        m_StartPanelShopView.Button_Right.gameObject.SetActive(true);
        
        list_ShopPanel[current_SelectIndex].transform.DOLocalMoveX(1000, 0.75f);
        list_ShopItemPrefas[current_SelectIndex].transform.DOLocalMoveX(0.8f, 0.75f);

        list_ShopPanel[current_SelectIndex - 1].transform.DOLocalMoveX(10, 0.75f);
        list_ShopItemPrefas[current_SelectIndex - 1].transform.DOLocalMoveX(0f, 0.75f);

        current_SelectIndex--;

        if (current_SelectIndex == list_ShopPanel.Count - 1)
        {
            m_StartPanelShopView.Button_Right.gameObject.SetActive(true);
        }
        else if (current_SelectIndex == 0)
        {
            m_StartPanelShopView.Button_Left.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// �̵���������л���Ʒ��ť�߼�
    /// dotween�����ı���Ʒ������ƷԤ�����λ��
    /// ���غ���ʾ���Ҽ�
    /// </summary>
    private void RightButtonEvent()
    {
        //AudioSource.PlayClipAtPoint(m_StartPanelShopView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(m_StartPanelShopView.AudioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        list_ShopItemPrefas[current_SelectIndex].transform.localRotation = Quaternion.Euler(new Vector3(-50, 150, 45));
        StartCoroutine("DelayLeftAndRightButton");

        m_StartPanelShopView.Button_Left.gameObject.SetActive(true);
        current_SelectIndex++;
        if (current_SelectIndex == 0)
        {
            m_StartPanelShopView.Button_Left.gameObject.SetActive(true);
        }
        else if (current_SelectIndex == list_ShopPanel.Count - 1)
        {
            m_StartPanelShopView.Button_Right.gameObject.SetActive(false);
        }

        list_ShopPanel[current_SelectIndex - 1].transform.DOLocalMoveX(-950, 0.75f);
        list_ShopItemPrefas[current_SelectIndex - 1].transform.DOLocalMoveX(-0.8f, 0.75f);

        list_ShopPanel[current_SelectIndex].transform.DOLocalMoveX(10, 0.75f);
        list_ShopItemPrefas[current_SelectIndex].transform.DOLocalMoveX(0f, 0.75f);
    }
    private void SavaShopBuyInfo(int index)
    {
        buy_Index = index;
        //��ȡ��������ʹ�ú������ʵ���������
        tempShopItemList[buy_Index].IsBuy = originalList_ShopPanel[buy_Index].GetComponent<ShopItemController>().IsBuy;
        tempShopItemList[buy_Index].IsUse = originalList_ShopPanel[buy_Index].GetComponent<ShopItemController>().IsUse;

        ChangeShopListInfo();
        //list_MaskIndex.Add(index);
        //Debug.Log(str);

        //���޸ĺ������ʵ����List�洢��json�ļ�
        JsonTool.ObjectToJson(jsonFileName, tempShopItemList);
        //File.Delete(jsonFileName);
        //string str =JsonMapper.ToJson(tempShopItemList);
        //StreamWriter sw = new StreamWriter(jsonFileName, true);
        //sw.Write(str);
        //sw.Close();
        //StreamWriter sw = File.AppendText(jsonPath);
        //for (int i = 0; i < list_ShopPanel.Count; i++)
        //{
        //    Debug.Log("list_ShopPanel[i].name:" + list_ShopPanel[i].name);
        //}  
        //GameObject tempShopPanel = list_ShopPanel[index];
        //for (int i = 0; i < index; i++)
        //{
        //    list_ShopPanel[index - i] = list_ShopPanel[index - i - 1];
        //    if (index - i - 1 == current_BuyAfterIndex)
        //    {
        //        list_ShopPanel[current_BuyAfterIndex] = tempShopPanel;
        //        current_BuyAfterIndex++;
        //    }
        //}
        // Debug.Log("list_ShopPanel.name:" + list_ShopPanel[index].name);




        //for (int i = 0; i < list_ShopPanel.Count; i++)
        //{

        //    Debug.Log("list_ShopPanel[i].name:" + list_ShopPanel[i].name);
        //}
        //Debug.Log("buy_Index:" + buy_Index);
    }
    /// <summary>
    /// Ϊ���޸Ĺ�����λ����Ʒλ�����и���list�ڵ���Ϣ
    /// </summary>
    private void ChangeShopListInfo()
    {
        Debug.Log("list_ShopItemBuyBefore.Count =?" + list_ShopItemBuyBefore.Count);
        //Debug.Log(buy_Index);
        if (list_ShopItemBuyAfter.Count == 0)
        {
            list_ShopItemBuyAfter.Add(originalList_ShopPanel[0]);
            list_ShopItemPrefasAfter.Add(originalList_ShopItemPrefas[0]);
        }
        list_ShopItemBuyAfter.Add(originalList_ShopPanel[buy_Index]);
        list_ShopItemPrefasAfter.Add(originalList_ShopItemPrefas[buy_Index]);
        //list_ShopItemBuyAfter.Add(list_ShopItemBuyBefore[buy_Index]);
        for (int j = 0; j < list_ShopItemBuyBefore.Count; j++)
        {
            if (originalList_ShopPanel[buy_Index] == list_ShopItemBuyBefore[j])
            {
                if (list_ShopItemBuyBefore.Count == 5)
                {
                    Debug.Log("ִ������");
                    list_ShopItemBuyBefore.Remove(list_ShopItemBuyBefore[0]);
                    list_ShopItemPrefasBefore.Remove(list_ShopItemPrefasBefore[0]);
                    list_ShopItemBuyBefore.Remove(list_ShopItemBuyBefore[j-1]);
                    list_ShopItemPrefasBefore.Remove(list_ShopItemPrefasBefore[j-1]);

                }
                else
                {
                    list_ShopItemBuyBefore.Remove(list_ShopItemBuyBefore[j]);
                    list_ShopItemPrefasBefore.Remove(list_ShopItemPrefasBefore[j]);
                }
                
            }
            
        }
        for (int j = 0; j < list_ShopItemBuyBefore.Count; j++)
        {
            Debug.Log("list_ShopItemBuyBefore:" + list_ShopItemBuyBefore[j]);
            Debug.Log("list_ShopItemPrefasBefore:" + list_ShopItemPrefasBefore[j]);
        }
    }
    /// <summary>
    /// �ӳ����Ұ�ť��ʹ�� ʹ��Ʒ�ƶ�����������
    /// </summary>
    private IEnumerator DelayLeftAndRightButton()
    {
        m_StartPanelShopView.Button_Right.interactable = false;
        m_StartPanelShopView.Button_Left.interactable = false;

        yield return new WaitForSecondsRealtime(0.75f);
        m_StartPanelShopView.Button_Right.interactable = true;
        m_StartPanelShopView.Button_Left.interactable = true;
    }


    /// <summary>
    /// ������Ʒʱ��Ҳ������ʾ��Ϣ����
    /// </summary>
    int offset = 0;
    int index = 0;
    private void PromptMessageToBuy()
    {
        go_PromptMessage= Instantiate<GameObject>(m_StartPanelShopView.Prefabs_PromptMessage, transform.Find("BackGound"));
        go_PromptMessage.transform.localPosition = new Vector3(0, 160, 0);
        go_PromptMessage.name = index.ToString();
        list_PromptMessage.Add(go_PromptMessage);
        //list_PromptMessage[0].GetComponent<Image>().DOFade(0, 1);
        Debug.Log("offset:" + offset);
        
        if (index > 0)
        {
            for (int i = 0; i < list_PromptMessage.Count-1; i++)
            {
                list_PromptMessage[i].transform.localPosition = new Vector3(0, list_PromptMessage[i].transform.localPosition.y + 40, 0);

            }
            //list_PromptMessage[index-1].transform.localPosition = new Vector3(0, 160 + offset, 0);
            //list_PromptMessage[index - 1].GetComponent<Image>().DOFade(0, 1);
        }
        if (list_PromptMessage.Count > 3)
        {
            Destroy(list_PromptMessage[0]);
            list_PromptMessage.RemoveAt(0);
        }
        Debug.Log("index:"+index);
        index++;
        offset += 40;
        //Invoke("MessageDelayFade", 1f);

    }
    public void InitShopPanelInOpening()
    {
        Debug.Log(m_StartPanelShopView.Button_Left);
        if (m_StartPanelShopView.Button_Left != null)
        {
            m_StartPanelShopView.Button_Left.gameObject.SetActive(false);

        }

    }

}
