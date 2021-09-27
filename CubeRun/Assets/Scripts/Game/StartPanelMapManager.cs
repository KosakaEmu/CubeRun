using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartPanelMapManager : MonoBehaviour
{
    private static StartPanelMapManager instance;
    private ObjectPool m_ObjectPool;
    private Transform m_Transform;

    private GameObject tile_Prefabs;    //地图地板模型预制体
    private GameObject wall_Prefabs;    //地图墙壁模型预制体
    private GameObject spikes_Prefabs;  //尖刺陷阱预制体
    private GameObject skyspikes_Prefabs;//天空尖刺陷阱预制体
    private GameObject hole_Prefabs;    //坑洞陷阱预制体
    private GameObject gold_Prefabs;    //金方块道具预制体
    private GameObject black_Prefabs;   //黑方块(暂停时间)道具预制体
    private GameObject turret_Prefabs;  //喷火器陷阱预制体

    private float bottomLength;         //地板底边长度

    private Color tileOne;              //地板颜色①
    private Color tileTwo;              //地板颜色②
    private Color wallColor;            //墙壁颜色
    private Color spikesColor;          //尖刺陷阱原来的颜色
    private Color skySpikesColor;       //天空陷阱原来的颜色


    private List<List<GameObject>> mapList;//地图资源嵌套存放的列表
    [Header("陷阱概率")]
    [SerializeField] private float hole_PR = 0;              //生成坑洞的概率
    [SerializeField] private float spikes_PR = 0;            //生成尖刺陷阱的概率
    [SerializeField] private float skyspikes_PR = 0;         //生成天空尖刺陷阱的概率
    int phaseNum = 0;                                        //控制陷阱生成的阶段数 例如第一次生成地图 不能出现陷阱

    private bool isDown;                                    //控制地板塌陷标志位

    private int index_Wall = 0;                             //控制对象池内物体的生成个数
    private int index_Hole = 0;
    private int index_Spikes = 0;
    private int index_SkySpikes = 0;
    private int index_Tile = 0;
    private int index_Gold = 0;
    private int index_Black = 0;
    private int index_Turret = 0;
    private int index_least_Black = 0;
    
    
    private float offsetZ = 0;
    public List<List<GameObject>> MapList { get => mapList; set => mapList = value; }
    public float BottomLength { get => bottomLength; set => bottomLength = value; }
    public bool IsDown { get => isDown; set => isDown = value; }
    public static StartPanelMapManager Instance { get => instance; set => instance = value; }
    public float Hole_PR { get => hole_PR; set => hole_PR = value; }
    public float Spikes_PR { get => spikes_PR; set => spikes_PR = value; }
    public float Skyspikes_PR { get => skyspikes_PR; set => skyspikes_PR = value; }

    void Awake()
    {
        instance = this;

        FindInit();


    }
    void Start()
    {
        CreateMapTile(offsetZ);
    }
    void Update()
    {
        
        if (mapList.Count>0)
        {
            if (mapList[mapList.Count - 13][0].transform.position.z - SetCameraMove.Instance.transform.position.z <= 1)
            {
                offsetZ = mapList[mapList.Count - 1][0].transform.position.z + bottomLength / 2;
                CreateMapTile(offsetZ);

            }
        }
        
    }
    private void FindInit()
    {
        m_Transform = gameObject.transform;

        tile_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/tile_white");
        wall_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/wall2");
        spikes_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/moving_spikes");
        skyspikes_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/smashing_spikes");
        hole_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/hole");
        gold_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/tile_Gold");
        black_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/tile_Black");
        turret_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/Turret");


        BottomLength = Mathf.Sqrt(2) * 0.254f;

        tileOne = new Color(134 / 255f, 113 / 255f, 173 / 255f);
        tileTwo = new Color(146 / 255f, 120 / 255f, 180 / 255f);
        wallColor = new Color(115 / 255f, 86 / 255f, 139 / 255f);

        m_ObjectPool = gameObject.GetComponent<ObjectPool>();
        mapList = new List<List<GameObject>>();


    }
    private IEnumerator AutoCreateMap()
    {
        float offsetZ = 0;
        bool isDalay = true;
        while (true)
        {
            CreateMapTile(offsetZ);
            if (isDalay)
            {
                yield return new WaitForSecondsRealtime(0);
                isDalay = false;
            }
            else
            {
                yield return new WaitForSecondsRealtime(6f);

            }

            offsetZ = mapList[mapList.Count - 1][0].transform.position.z + bottomLength / 2;
            
        }
    }
    /// <summary>
    /// 生成地图资源
    /// </summary>
    /// <param name="第二次及以后创建新地图传过来的偏移量"></param>
    /// <param name="控制陷阱生成的阶段数 "></param>
    public void CreateMapTile(float offsetZ)
    {
        int randCreate = Random.Range(0, 10);
        bool isCreateTurret = true;
        for (int i = 0; i < 10; i++)    //共生成20排地板 分两种条件执行
        {
            List<GameObject> tile_List = new List<GameObject>();//用于存储第一排每一个地图地板资源的列表
            int colorNum;               //标志地板或墙壁颜色;
            //GameObject tile=null;
            //GameObject temp;
            GameObject temp;
            for (int j = 0; j < 6; j++)//生成地图第一排地板（有墙壁）
            {
                Vector3 pos = new Vector3(j * BottomLength, 0, i * BottomLength + offsetZ);
                Quaternion rot = Quaternion.Euler(-90f, 45f, 0);
                if (j == 0 || j == 5)   //生成墙壁
                {
                    colorNum = 1;

                    
                    int randomLeftOrRight = Random.Range(0, 2);
                    int randomCreateTurret = Random.Range(1, 101);
                    if (randomLeftOrRight == 1) { randomLeftOrRight += 4; }
                    if (randCreate == i && isCreateTurret && j == randomLeftOrRight && phaseNum > 5 && randomCreateTurret / 100 == 1)
                    {
                        if (j == 0)
                        {

                            temp = ControllObjectPool("Turret", pos, Quaternion.Euler(0, 45f, 0), 0, index_Turret, 5, turret_Prefabs);
                            if (index_Turret < 5)
                            {
                                index_Turret++;
                            }
                            //temp = GameObject.Instantiate<GameObject>(turret_Prefabs, pos, Quaternion.Euler(0, 45f, 0), m_Transform);
                            isCreateTurret = false;
                        }
                        else
                        {
                            temp = ControllObjectPool("Turret", pos, Quaternion.Euler(0, -135f, 0), 0, index_Turret, 5, turret_Prefabs);
                            if (index_Turret < 5)
                            {
                                index_Turret++;
                            }
                            //temp = GameObject.Instantiate<GameObject>(turret_Prefabs, pos, Quaternion.Euler(0, -135f, 0), m_Transform);
                            isCreateTurret = false;
                        }

                    }
                    else
                    {
                        temp = ControllObjectPool("Wall", pos, rot, colorNum, index_Wall, 80, wall_Prefabs);
                        if (index_Wall < 80)
                        {
                            index_Wall++;
                        }
                    }
                }
                else                    //生成墙壁之间的地板
                {
                    colorNum = 2;
                    temp = JudgeTileKindAndCreate(pos, rot, colorNum, phaseNum);
                    temp.name = i + "--" + j;

                }
                tile_List.Add(temp);    //每生成一个地图资源都添加至列表
            }

            mapList.Add(tile_List);     //每生成一排地图资源都添加至列表
            List<GameObject> tile_List2 = new List<GameObject>();//用于存储第二排每一个地图地板资源的列表

            for (int j = 0; j < 5; j++) //生成地图第二排地板
            {
                Vector3 pos = new Vector3(j * BottomLength + BottomLength / 2, 0, i * BottomLength + BottomLength / 2 + offsetZ);
                Quaternion rot = Quaternion.Euler(-90f, 45f, 0);
                colorNum = 3;

                temp = JudgeTileKindAndCreate(pos, rot, colorNum, phaseNum);
                temp.name = i + "--" + j;
                tile_List2.Add(temp);
            }
            mapList.Add(tile_List2);
        }


        AddPR();
        phaseNum++;


        //Debug.Log(floorDownTime);
        //Debug.Log("phaseNum:"+phaseNum);
        //Debug.Log(spikes_PR);
        //Debug.Log(skyspikes_PR);
    }
    /// <summary>
    /// 计算陷阱生成的概率 根据返回值生成对应陷阱
    /// </summary>
    /// <returns></returns>
    private int CalcPR()
    {
        int pr = Random.Range(1, 101);
        int pr_2 = Random.Range(1, 201);
        //Debug.Log("pr:"+pr);
        if (pr <= hole_PR)
        {
            return 1;
        }
        else if (pr >= 11 && pr <= spikes_PR + 10)
        {
            return 2;
        }
        else if (pr >= 21 && pr <= skyspikes_PR + 20)
        {
            return 3;
        }
        else if (pr / 100 == 1)
        {
            return 4;
        }
        else if (pr_2 / 200 == 1)
        //else if (pr_2 >= 150 && pr_2 <= 200)
        {
            return 5;
        }
        else
        {
            return 0;
        }
    }
    /// <summary>
    /// 增加陷阱生成的概率 
    /// 1.坑洞概率 最高为7 每新生成一次地图+1
    /// 2.尖刺概率 最高为4 每新生成一次地图+0.5
    /// 3.天空尖刺概率 最高为4 每新生成一次地图+0.5
    /// </summary>
    private void AddPR()
    {

        if (hole_PR >= 6)
        {
            hole_PR = 6;
        }
        else
        {
            hole_PR += 0.5f;
        }
        if (spikes_PR >= 4)
        {
            spikes_PR = 4;
        }
        else
        {
            spikes_PR += 0.25f;
        }
        if (skyspikes_PR >= 4)
        {
            skyspikes_PR = 4;

        }
        else
        {
            skyspikes_PR += 0.25f;
        }

    }
    /// <summary>
    /// 判断每一块地板资源的类型 根据类型和陷阱阶段数来创建地图
    /// </summary>
    /// <param name="地板资源位置"></param>
    /// <param name="地板资源旋转角度"></param>
    /// <param name="地板资源颜色标志"></param>
    /// <param name="陷阱阶段数"></param>
    /// <returns>返回地图资源</returns>
    private GameObject JudgeTileKindAndCreate(Vector3 pos, Quaternion rot, int colorNum, int phaseNum)
    {
        int prNum = CalcPR();

        GameObject tile = null;
        if (prNum == 1 && phaseNum > 0)            //生成坑洞陷阱
        {
            colorNum = 0;
            tile = ControllObjectPool("Hole", pos, rot, colorNum, index_Hole, 25, hole_Prefabs);
            if (index_Hole < 40)
            {
                index_Hole++;
            }
        }
        else if (prNum == 2 && phaseNum > 1)    //生成尖刺陷阱
        {
            colorNum = 0;
            tile = ControllObjectPool("Spikes", pos, rot, colorNum, index_Spikes, 15, spikes_Prefabs);
            if (index_Spikes < 15)
            {
                index_Spikes++;
            }
        }
        else if (prNum == 3 && phaseNum > 3)    //生成天空尖刺陷阱
        {
            colorNum = 0;
            tile = ControllObjectPool("SkySpikes", pos, rot, colorNum, index_SkySpikes, 15, skyspikes_Prefabs);
            if (index_SkySpikes < 15)
            {
                index_SkySpikes++;
            }
        }
        else if (prNum == 4)
        {
            tile = ControllObjectPool("Gold", pos, rot, colorNum, index_Gold, 10, gold_Prefabs);
            if (index_Gold < 10)
            {
                index_Gold++;
            }

        }
        else if (prNum == 5 && index_least_Black >= 200)
        {
            tile = ControllObjectPool("Black", pos, rot, colorNum, index_Black, 5, black_Prefabs);
            if (index_Black < 5)
            {
                index_Black++;
            }
            index_least_Black = 0;
        }
        else                                   //生成普通地板
        {
            tile = ControllObjectPool("Tile", pos, rot, colorNum, index_Tile, 360, tile_Prefabs);
            if (index_Tile < 360)
            {
                index_Tile++;
            }
            index_least_Black++;
        }
        return tile;
    }
    /// <summary>
    /// 循环创建地图资源和设置颜色
    /// </summary>
    /// <param name="地图地板资源预制体"></param>
    /// <param name="预制体摆放位置"></param>
    /// <param name="预制体摆放角度"></param>
    /// <param name="地图资源颜色标志位"></param>
    private GameObject CreateAllTileWallAndSetColor(GameObject prefabs, Vector3 pos, Quaternion rot, int tileColor)
    {
        GameObject tile = GameObject.Instantiate<GameObject>(prefabs, pos, rot, m_Transform);

        if (tileColor == 1)
        {
            tile.transform.GetComponent<MeshRenderer>().material.color = wallColor;
        }
        else if (tileColor == 2)
        {
            tile.transform.Find("normal_a2").GetComponent<MeshRenderer>().material.color = tileOne;
        }
        else if (tileColor == 3)
        {
            tile.transform.Find("normal_a2").GetComponent<MeshRenderer>().material.color = tileTwo;
        }
        else
        {
            return tile;
        }
        return tile;
    }
    private void ResetTileColor(List<List<GameObject>> mapList, int i, int j, Color tileColor)
    {
        if (mapList[i][j].CompareTag("Tile"))
        {
            mapList[i][j].transform.Find("normal_a2").GetComponent<MeshRenderer>().material.color = tileColor;

        }
        else if (mapList[i][j].CompareTag("Spikes"))
        {
            mapList[i][j].transform.Find("moving_spikes_a2").GetComponent<MeshRenderer>().material.color = spikesColor;

        }
        else if (mapList[i][j].CompareTag("SkySpikes"))
        {
            mapList[i][j].transform.Find("smashing_spikes_a2").GetComponent<MeshRenderer>().material.color = skySpikesColor;
        }
    }
    private GameObject ControllObjectPool(string ObjcetPoolName, Vector3 pos, Quaternion rot, int colorNum, int index_GameObject, int index, GameObject prefabs)
    {
        GameObject tile;
        if (m_ObjectPool.Data(ObjcetPoolName) && index_GameObject == index)
        {
            tile = m_ObjectPool.GetObject(ObjcetPoolName);
            tile.SetActive(true);

            tile.transform.position = pos;
            tile.transform.rotation = rot;
            m_ObjectPool.AddObject(ObjcetPoolName, tile);
            //Debug.Log("这里执行了吗？");
        }
        else
        {
            tile = CreateAllTileWallAndSetColor(prefabs, pos, rot, colorNum);
            //Debug.Log("tile:"+tile.transform.position);
        }
        if (index_GameObject < index)
        {
            m_ObjectPool.AddObject(ObjcetPoolName, tile);
            
            //Debug.Log("tile index:" + index_Tile);

        }
        return tile;
    }
    public void GameStart()
    {
        //StopCoroutine("AutoCreateMap");
        m_ObjectPool.CleanObjectPool();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
    }
  
}
