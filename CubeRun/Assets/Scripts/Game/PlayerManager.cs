using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    private GameObject playerPrefabs;
    private Transform m_Transform;
    private List<ShopItem> list_ShopItem;
    //private string jsonPath;
    private string jsonFileName;


    GameObject player;
    public static PlayerManager Instance { get => instance; set => instance = value; }

    void Awake()
    {
        instance = this;
        //Debug.Log("playerPrefabs"+playerPrefabs.name);
        m_Transform = gameObject.transform;
        //jsonPath = Application.dataPath + @"\Resources\Json\GameShopData2.txt";
        jsonFileName =  "GameShopData2.txt";


        //list_ShopItem = JsonTool.JsonToObject<ShopItem>(jsonPath);
        //JsonTool.DeletePersistentData(jsonFileName);
        list_ShopItem = JsonTool.JsonToObjectAndroid<ShopItem>(jsonFileName);
        for (int i = 0; i < list_ShopItem.Count; i++)
        {
            if (list_ShopItem[i].IsUse)
            {
                playerPrefabs = Resources.Load<GameObject>("ShopItemPrefabs/"+ list_ShopItem[i].ShopName);
            }
        }
        //CreatePlayer();
    }

    void Update()
    {
        
    }
    public void LoadUsingPlayer(string prefabName)
    {
        playerPrefabs = Resources.Load<GameObject>("ShopItemPrefabs/"+prefabName);
        
    }
    public void CreatePlayer()
    {
        player= GameObject.Instantiate<GameObject>(playerPrefabs,m_Transform);
        player.name = player.name.Substring(0, player.name.Length - 7);
        player.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);

        StartCoroutine(InitPlayerPrefabsRot(player.name, player));
        player.name = "Player";
        player.AddComponent<Player>();
        player.AddComponent<Rigidbody>();
        player.tag = "Player";
        player.layer = 6;
        
        StopCoroutine(InitPlayerPrefabsRot(player.name, player));
        //player.transform.localScale = new Vector3(0.80f, 0.80f, 0.80f);
    }
    private IEnumerator InitPlayerPrefabsRot(string prefasName,GameObject player)
    {
        yield return null;

        if (prefasName.Contains("cube_watermelon"))
        {
            Debug.Log("Ö´ÐÐ½Ç¶ÈÁËÂð");
            player.transform.localRotation = Quaternion.Euler(-90f, -135f, 180f);
            player.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);

            Debug.Log("playername:" + player.name);
        }
        else if(prefasName.Contains("cube_books"))
        {
            
            player.transform.localRotation = Quaternion.Euler(-90f, -180f, 45f);
            player.transform.localScale = new Vector3(0.82f, 0.87f, 0.87f);

        }
        else if (prefasName.Contains("cube_dice"))
        {
            
            player.transform.localRotation = Quaternion.Euler(-90f, -180f, 45f);
            player.transform.localScale = new Vector3(0.90f, 0.90f, 0.90f);

        }
    }
    
}
