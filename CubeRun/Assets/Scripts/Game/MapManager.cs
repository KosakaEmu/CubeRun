using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;

    private ObjectPool m_ObjectPool;
    private Transform m_Transform;

    private GameObject tile_Prefabs;    //��ͼ�ذ�ģ��Ԥ����
    private GameObject wall_Prefabs;    //��ͼǽ��ģ��Ԥ����
    private GameObject spikes_Prefabs;  //�������Ԥ����
    private GameObject skyspikes_Prefabs;//��ռ������Ԥ����
    private GameObject hole_Prefabs;    //�Ӷ�����Ԥ����
    private GameObject gold_Prefabs;    //�𷽿����Ԥ����
    private GameObject black_Prefabs;   //�ڷ���(��ͣʱ��)����Ԥ����
    private GameObject turret_Prefabs;  //���������Ԥ����

    private float bottomLength;         //�ذ�ױ߳���

    private Color tileOne;              //�ذ���ɫ��
    private Color tileTwo;              //�ذ���ɫ��
    private Color wallColor;            //ǽ����ɫ
    private Color spikesColor;          //�������ԭ������ɫ
    private Color skySpikesColor;       //�������ԭ������ɫ


    private List<List<GameObject>> mapList;//��ͼ��ԴǶ�״�ŵ��б�
    [Header("�������")]
    [SerializeField] private float hole_PR = 0;              //���ɿӶ��ĸ���
    [SerializeField] private float spikes_PR = 0;            //���ɼ������ĸ���
    [SerializeField] private float skyspikes_PR = 0;         //������ռ������ĸ���
    int phaseNum = 0;                                        //�����������ɵĽ׶��� �����һ�����ɵ�ͼ ���ܳ�������
    [Header("�ذ�����ʱ����")]
    [SerializeField] private float floorDownTime;           //�ذ����ݵ�ʱ����
    //[SerializeField] private bool isDownStart;
    private bool isDownStart = true;
    private bool isDown;                                    //���Ƶذ����ݱ�־λ

    private int index_Wall = 0;                             //���ƶ��������������ɸ���
    private int index_Hole = 0;
    private int index_Spikes = 0;
    private int index_SkySpikes = 0;
    private int index_Tile = 0;
    private int index_Gold = 0;
    private int index_Black = 0;
    private int index_Turret = 0;
    private int index_least_Black = 0;
    private int index_TileDown = 0;

    private int mask_CreatGold = 0;
    Rigidbody rb = null;
    public List<List<GameObject>> MapList { get => mapList; set => mapList = value; }
    public float BottomLength { get => bottomLength; set => bottomLength = value; }
    public bool IsDown { get => isDown; set => isDown = value; }
    public static MapManager Instance { get => instance; set => instance = value; }
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
        
        //StartCoroutine("MapFloorDown");
    }
    private void OnBecameVisible()
    {
        
    }
    void Update()
    {
        //if (isDown==false)
        //{
        //    StopCoroutine("MapFloorDown");
        //}
        if (Input.GetKeyDown(KeyCode.U))
        {
            TestList();
        }

    }
    private void FindInit()
    {
        m_Transform = gameObject.transform;

        tile_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/tile_white");
        wall_Prefabs= Resources.Load<GameObject>("GameMapPrefabs/wall2");
        spikes_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/moving_spikes");
        skyspikes_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/smashing_spikes");
        hole_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/hole");

        gold_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/tile_Gold");
        black_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/tile_Black");
        turret_Prefabs = Resources.Load<GameObject>("GameMapPrefabs/Turret"); 


         BottomLength = Mathf.Sqrt(2) * 0.254f;

        tileOne = new Color(134 / 255f, 113 / 255f, 173 / 255f);
        tileTwo = new Color(146 / 255f, 120 / 255f, 180 / 255f);
        wallColor=new Color(115 / 255f, 86 / 255f, 139 / 255f);

        m_ObjectPool = gameObject.GetComponent<ObjectPool>();
        mapList = new List<List<GameObject>>();

        //TestList();
        floorDownTime = 0.40f;

        isDown = true;
    }
    /// <summary>
    /// ���ɵ�ͼ��Դ
    /// </summary>
    /// <param name="�ڶ��μ��Ժ󴴽��µ�ͼ��������ƫ����"></param>
    /// <param name="�����������ɵĽ׶��� "></param>
    public void CreateMapTile(float offsetZ)
    {
        //int randCreate= Random.Range(0, 10);
        bool isCreateTurret = true;
        for (int i = 0; i < 10; i++)    //������20�ŵذ� ����������ִ��
        {
            List<GameObject> tile_List = new List<GameObject>();//���ڴ洢��һ��ÿһ����ͼ�ذ���Դ���б�
            mask_CreatGold++;
            int colorNum;               //��־�ذ��ǽ����ɫ;
            //GameObject tile=null;
            //GameObject temp;
            GameObject temp;
            for (int j = 0; j < 6; j++)//���ɵ�ͼ��һ�ŵذ壨��ǽ�ڣ�
            {
                Vector3 pos = new Vector3(j * BottomLength, 0, i * BottomLength+ offsetZ);
                Quaternion rot = Quaternion.Euler(-90f, 45f, 0);
                if (j == 0 || j == 5)   //����ǽ��
                {
                    colorNum = 1;
                    //Debug.Log("nima bִ���� ��");
                    //Debug.Log(ObjectPool.Instance.Data("Wall"));
                    //Debug.Log(ObjectPool.Instance.a);
                    
                    //int randomLeftOrRight = Random.Range(0, 2);
                    int randomCreateTurret = Random.Range(1, 51);
                    //if (randomLeftOrRight == 1) { randomLeftOrRight += 4; }
                    //randCreate == i&& 
                    if (isCreateTurret && phaseNum > 5 && randomCreateTurret  == 50)
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
                        temp = ControllObjectPool("Wall", pos, rot, colorNum, index_Wall, 140, wall_Prefabs);
                        if (index_Wall < 140)
                        {
                            index_Wall++;
                        }

                        //if (ObjectPool.Instance.Data("Wall") && index_Wall == 80)
                        //{
                        //    temp = ObjectPool.Instance.GetObject("Wall");
                        //    //Debug.Log("����ִ������111");

                        //    temp.SetActive(true);
                        //    //Debug.Log("�׶�����"+"temp.name"+ temp.name);


                        //    temp.transform.position = pos;
                        //    //Debug.Log("tempPos:"+temp.transform.position);
                        //    temp.transform.rotation = rot;
                        //    ObjectPool.Instance.AddObject("Wall", temp);


                        //}
                        //else
                        //{

                        //    temp = CreateAllTileWallAndSetColor(wall_Prefabs, pos, rot, colorNum);
                        //    temp.name = i + "--" + j;


                        //    //Debug.Log("����ִ������2222");
                        //    //Debug.Log(temp.transform.position);
                        //}
                        //if (index_Wall < 80)
                        //{
                        //    ObjectPool.Instance.AddObject("Wall", temp);
                        //    index_Wall++;
                        //    Debug.Log("wall:" + index_Wall);
                        //}
                    }
                }
                else                    //����ǽ��֮��ĵذ�
                {
                    colorNum = 2;
                    
                    temp = JudgeTileKindAndCreate(pos, rot, colorNum, phaseNum);
                    //temp.name = i + "--" + j;

                }
                tile_List.Add(temp);    //ÿ����һ����ͼ��Դ��������б�
                //tile_List[j].name = i + "--" + j;
                //Debug.Log(tile_List[j]+"--"+ tile_List[j].transform.position);
            }

            mapList.Add(tile_List);     //ÿ����һ�ŵ�ͼ��Դ��������б�
            List<GameObject> tile_List2 = new List<GameObject>();//���ڴ洢�ڶ���ÿһ����ͼ�ذ���Դ���б�
            
            for (int j = 0; j < 5; j++) //���ɵ�ͼ�ڶ��ŵذ�
            {
                Vector3 pos = new Vector3(j * BottomLength+ BottomLength/2, 0, i * BottomLength + BottomLength / 2 + offsetZ);
                Quaternion rot = Quaternion.Euler(-90f, 45f, 0);
                colorNum = 3;

                temp = JudgeTileKindAndCreate(pos, rot, colorNum, phaseNum);
                //temp.name = i + "--" + j;
                tile_List2.Add(temp);
            }
            mapList.Add(tile_List2);
        }
        
        if (floorDownTime >= 0.26f)
        {
            floorDownTime -= 0.02f;
        }

        AddPR();
        phaseNum++;


        //Debug.Log(floorDownTime);
        //Debug.Log("phaseNum:"+phaseNum);
        //Debug.Log(spikes_PR);
        //Debug.Log(skyspikes_PR);
    }
    
    private void TestList()
    {
        string str=null;
        for (int i = 0; i < MapList.Count; i++)
        {
            for (int j = 0; j < MapList[i].Count; j++)
            {
                
                MapList[i][j].name = i + "--" + j+" ";
                str += MapList[i][j].name;
            }
            str += "\n";
            
        }
        Debug.Log(str);
    }
    public void StartFloorDownCoroutine()
    {
        if (isDownStart)
        {
            StartCoroutine("MapFloorDown");
        }
    }
    public void StopFloorDownCoroutine()
    {
        StopCoroutine("MapFloorDown");
        StopCoroutine("FloorDownSetActive");
    }
    /// <summary>
    /// ʵ�ֵ������ݹ���
    /// </summary>
    /// <returns></returns>
    private IEnumerator MapFloorDown()
    {
        for (int i = index_TileDown; i < mapList.Count; i++, index_TileDown++)
        {
            //Debug.Log("i1:" + i);
            //Debug.Log("index_TileDown" + index_TileDown);

            for (int j = 0; j < mapList[i].Count; j++)
            {
                float minZ = mapList[i][j].transform.position.z - bottomLength / 2;
                float maxZ = mapList[i][j].transform.position.z + bottomLength / 2;

                //�ж��������һ������ʱ ��ɫ�Ƿ������һ����
                if (minZ < Player.Instance.transform.position.z && Player.Instance.transform.position.z < maxZ)
                {

                    //Debug.Log("ִ������");
                    //Debug.Log("ִ������" + Player.Instance.transform.position);
                    Player.Instance.IsControl = false;                     //ȡ����ɫ����Ȩ
                    Player.Instance.Invoke("PlayerDown", 1);               //ִ�н�ɫ���䷽��
                }
                if (mapList[i][j].GetComponent<Rigidbody>() == null)
                {
                    rb = mapList[i][j].AddComponent<Rigidbody>();//��Ӹ���ʹ��ʵ������

                    //Debug.Log("3sssssssssss4444444444");
                }
                rb.angularVelocity = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)) * Random.Range(10, 30);
                //rb.GetComponent<Rigidbody>().

                //              //1s���������ݵĵذ�
                //�����ݵĵذ����һ��������ٶ� ������ϷЧ��

                //yield return new WaitForSeconds(1f);
                StartCoroutine(FloorDownSetActive(rb));
                //Debug.Log("bbbbbbbbbbbbbbbbbb");


                //Destroy(mapList[i][j].GetComponent<Rigidbody>(), 1f);

            }
            //if (i < index_TileDown)
            //{
            //    yield return new WaitForSecondsRealtime(0);
            //    Debug.Log("ִ��if����");

            //}
            //else
            //{
            //    yield return new WaitForSecondsRealtime(floorDownTime);
            //    index_TileDown = i;
            //    Debug.Log("ִ��else����");

            //}
            yield return new WaitForSeconds(floorDownTime);

            //Debug.Log("i2:" + i);

            //Debug.Log("index_TileDown" + index_TileDown);

        }

    }
    private IEnumerator FloorDownSetActive(Rigidbody rb)      
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < mapList.Count; i++)
        {
            for (int j = 0; j < mapList[i].Count; j++)
            {
                if (mapList[i][j].transform.position.y <= -1)
                {
                    //Debug.Log("bbbbbbbbbbbbbbbbbb");
                    mapList[i][j].SetActive(false);
                    Destroy(rb);
                    if (i % 2 == 0)
                    {
                        ResetTileColor(mapList, i, j, tileOne);

                    }
                    else
                    {
                        ResetTileColor(mapList, i, j, tileTwo);
                    }
                }


            }
            yield return new WaitForSeconds(0.001f);
        }

    }
    /// <summary>
    /// �����������ɵĸ��� ���ݷ���ֵ���ɶ�Ӧ����
    /// </summary>
    /// <returns></returns>
    private int CalcPR()
    {
        int pr = Random.Range(1, 101);
        int pr_2 = Random.Range(1, 201);
        //Debug.Log("pr:"+pr);
        if (pr <= hole_PR)//�Ӷ��������ɵĸ������6%
        {
            return 1;
        }
        else if (pr >= 11 && pr <= spikes_PR + 10)//����������ɵĸ������4%
        {
            return 2;
        }
        else if (pr >= 21 && pr <= skyspikes_PR + 20)//����������ɵĸ������4%
        {
            return 3;
        }
        else if (pr / 100 == 1)//������ɵĸ������1%
        {
            return 4;
        }
        
        //else if (pr_2 >= 150 && pr_2 <=  200)
        else if (pr_2 / 200 == 1)//ʱ����ͣ�������ɵĸ������0.5%
        {
            return 5;
        }
        else                    //����ȫ��������ͨ�ذ�
        {
            return 0;
        }
    }
    /// <summary>
    /// �����������ɵĸ��� 
    /// 1.�Ӷ����� ���Ϊ5 ÿ������һ�ε�ͼ+1
    /// 2.��̸��� ���Ϊ5 ÿ������һ�ε�ͼ+0.5
    /// 3.��ռ�̸��� ���Ϊ5 ÿ������һ�ε�ͼ+0.5
    /// </summary>
    private void AddPR()
    {

        if (hole_PR >= 5)
        {
            hole_PR = 6;  
        }
        else
        {
            hole_PR += 1;
        }
        if (spikes_PR >= 5)
        {
            spikes_PR = 4;
        }
        else
        {
            spikes_PR += 0.5f;
        }
        if (skyspikes_PR >= 5)
        {
            skyspikes_PR = 4;
        }
        else
        {
            skyspikes_PR += 0.5f;
        }

    }
    /// <summary>
    /// �ж�ÿһ��ذ���Դ������ �������ͺ�����׶�����������ͼ
    /// </summary>
    /// <param name="�ذ���Դλ��"></param>
    /// <param name="�ذ���Դ��ת�Ƕ�"></param>
    /// <param name="�ذ���Դ��ɫ��־"></param>
    /// <param name="����׶���"></param>
    /// <returns>���ص�ͼ��Դ</returns>
    private GameObject JudgeTileKindAndCreate(Vector3 pos, Quaternion rot, int colorNum, int phaseNum)
    {
        int prNum = CalcPR();
        
        GameObject tile = null;
        if (prNum == 1&& phaseNum>0)            //���ɿӶ�����
        {
            colorNum = 0;
            tile = ControllObjectPool("Hole", pos, rot, colorNum, index_Hole, 40, hole_Prefabs);
            if (index_Hole < 20)
            {
                index_Hole++;
            }
            //if (ObjectPool.Instance.Data("Hole") && index_Hole == 40)
            //{
            //    tile = ObjectPool.Instance.GetObject("Hole");
            //    tile.SetActive(true);
            //    tile.transform.position = pos;
            //    tile.transform.rotation = rot;
            //    ObjectPool.Instance.AddObject("Hole", tile);

            //}
            //else
            //{

            //    tile = CreateAllTileWallAndSetColor(hole_Prefabs, pos, rot, colorNum);

            //}
            //if (index_Hole < 40)
            //{
            //    ObjectPool.Instance.AddObject("Hole", tile);
            //    index_Hole++;
            //}

        }
        else if (prNum == 2 && phaseNum > 1)    //���ɼ������
        {
            colorNum = 0;
            tile = ControllObjectPool("Spikes", pos, rot, colorNum, index_Spikes, 40, spikes_Prefabs);
            if (index_Spikes < 20)
            {
                index_Spikes++;
            }
        }
        else if (prNum == 3 && phaseNum > 3)    //������ռ������
        {
            colorNum = 0;
            tile = ControllObjectPool("SkySpikes", pos, rot, colorNum, index_SkySpikes, 40, skyspikes_Prefabs);
            if (index_SkySpikes < 20)
            {
                index_SkySpikes++;
            }
        }
        else if (prNum == 4 && mask_CreatGold>2)    
        {
            tile = ControllObjectPool("Gold", pos, rot, colorNum, index_Gold, 15, gold_Prefabs);
            if (index_Gold < 10)
            {
                index_Gold++;
            }

        }
        else if (prNum == 5&& index_least_Black>=200)
        {
            tile = ControllObjectPool("Black", pos, rot, colorNum, index_Black, 5, black_Prefabs);
            index_least_Black = 0;
            if (index_Black < 5)
            {
                index_Black++;
            }
        }
        else                                   //������ͨ�ذ�
        {
            tile=ControllObjectPool("Tile", pos, rot, colorNum, index_Tile, 630, tile_Prefabs);
            if (index_Tile < 630)
            {
                index_Tile++;
            }
            index_least_Black++;
        }
        return tile;
    }
    /// <summary>
    /// ѭ��������ͼ��Դ��������ɫ
    /// </summary>
    /// <param name="��ͼ�ذ���ԴԤ����"></param>
    /// <param name="Ԥ����ڷ�λ��"></param>
    /// <param name="Ԥ����ڷŽǶ�"></param>
    /// <param name="��ͼ��Դ��ɫ��־λ"></param>
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
    private void ResetTileColor(List<List<GameObject>> mapList,int i,int j,Color tileColor)
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
    private GameObject ControllObjectPool(string ObjcetPoolName, Vector3 pos, Quaternion rot, int colorNum,int index_GameObject,int index,GameObject prefabs)
    {
        GameObject tile;
        if (m_ObjectPool.Data(ObjcetPoolName) && index_GameObject == index)
        {
            tile = m_ObjectPool.GetObject(ObjcetPoolName);
            tile.SetActive(true);
            
            tile.transform.position = pos;
            tile.transform.rotation = rot;
            m_ObjectPool.AddObject(ObjcetPoolName, tile);
            if (ObjcetPoolName.Contains("Turret"))
            {
                tile.GetComponent<Turret>().StartPlayFire();
            }
            if (colorNum == 2)
            {
                tile.transform.Find("normal_a2").GetComponent<MeshRenderer>().material.color = tileOne;
            }
            else if(colorNum == 3)
            {
                tile.transform.Find("normal_a2").GetComponent<MeshRenderer>().material.color = tileTwo;
            }
            
            //Debug.Log("����ִ������");
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
    private void ResetMap()
    {
        mapList.Clear();
        
    }
   
}
