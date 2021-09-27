using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;

    
    private GameObject[] m_Spikes;
    private GameObject[] m_SkySpikes;
    private GameObject[] m_Turret;
    private int z = 3;                   //玩家角色默认位置
    private int x = 2;

    private Color colorOne;             //玩家角色经过的地板痕迹颜色 浅
    private Color colorTwo;             //玩家角色经过的地板痕迹颜色 深

    private Transform playerPos;        //玩家所在的地图位置
    private Transform playerTransform;  //玩家所在的具体位置

    private bool isControl;             //玩家角色是否可以控制
    private bool isFindTrap;
    private bool isPlayerDown = false;
    //private AudioSource m_AudioSource;
    private AudioClip audioClip_Spikes;
    private int playerStep = 0;
    public static Player Instance { get => instance; set => instance = value; }
    public int Z { get => z; set => z = value; }
    public Transform PlayerPos { get => playerPos; set => playerPos = value; }
    public bool IsControl { get => isControl; set => isControl = value; }
    public int PlayerStep { get => playerStep;  }

    void Awake()
    {
        instance = this;

        
        colorOne = new Color(238 / 255f, 98 / 255f, 144 / 255f);
        colorTwo = new Color(227 / 255f, 92 / 255f, 137 / 255f);

        playerTransform = transform;
        audioClip_Spikes = Resources.Load<AudioClip>("GameSound/spikesClip");
        isControl = false;
        isFindTrap = false;
        //m_AudioSource = gameObject.GetComponent<AudioSource>();
        //SetPlayerPosition();


    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{

        //    MapManager.Instance.CreateMapTile(0);
        //    PlayerManager.Instance.CreatePlayer();
        //    SetPlayerPosition();
        //    isControl = true;
        //}
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            MapManager.Instance.StopFloorDownCoroutine();
            MapManager.Instance.Invoke("StartFloorDownCoroutine", 3f);
        }
        if (isControl)
        {
            ControlPlayer();
        }
        

        if (playerTransform.position.y <= -2&& isPlayerDown==false)
        {
            PlayerDown();
            isPlayerDown = true;


        }
        
    }
    /// <summary>
    /// 键盘A/D控制角色移动 计算和设置角色位置
    /// </summary>
    private void ControlPlayer()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerStep += 1;
            if (z % 2 == 1 && x != 0)
            {
                z++;
            }
            else if (z % 2 == 0)
            {
                z++;
                x--;
            }
            else
            {
                return;
            }
            SetPlayerPosition();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerStep += 1;

            if (z % 2 == 1 && x != 4)
            {
                z++;
                x++;
            }
            else if (z % 2 == 0)
            {
                z++;

            }
            SetPlayerPosition();
        }
        if (isFindTrap)
        {
            m_Spikes = GameObject.FindGameObjectsWithTag("Spikes");
            m_SkySpikes = GameObject.FindGameObjectsWithTag("SkySpikes");
            m_Turret = GameObject.FindGameObjectsWithTag("Turret");
            for (int i = 0; i < m_Spikes.Length; i++)
            {
                Spikes spikes=m_Spikes[i].GetComponent<Spikes>();
                
                spikes.StopAnim();
            }
            for (int i = 0; i < m_SkySpikes.Length; i++)
            {
                SkySpikes skySpikes = m_SkySpikes[i].GetComponent<SkySpikes>();

                skySpikes.StopAnim();
            }
            for (int i = 0; i < m_Turret.Length; i++)
            {
                Turret turret = m_Turret[i].GetComponent<Turret>();
                turret.StopPlayFire();
            }
        }

    }
    public void LeftBtnControl()
    {
        if (isControl)
        {
            playerStep += 1;
            if (z % 2 == 1 && x != 0)
            {
                z++;
            }
            else if (z % 2 == 0)
            {
                z++;
                x--;
            }
            else
            {
                return;
            }
            SetPlayerPosition();
        }
        
    }
    public void RightBtnControl()
    {
        if (isControl)
        {
            playerStep += 1;

            if (z % 2 == 1 && x != 4)
            {
                z++;
                x++;
            }
            else if (z % 2 == 0)
            {
                z++;

            }
            SetPlayerPosition();
        }
    }
    /// <summary>
    /// 设置玩家角色位置并且生成新地图
    /// </summary>
    public void SetPlayerPosition()
    {
        playerPos = MapManager.Instance.MapList[z][x].transform;
        //gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0,45,0));
        playerTransform.position = MapManager.Instance.MapList[z][x].transform.position + new Vector3(0, 0.13f, 0);
        //Debug.Log("Test");

        if (MapManager.Instance.MapList.Count - z <= 15)        //在玩家到达指定位置时生成新地图
        {
            float offsetZ = MapManager.Instance.MapList[MapManager.Instance.MapList.Count - 1][0].transform.position.z + MapManager.Instance.BottomLength / 2;
            MapManager.Instance.CreateMapTile(offsetZ);
            //Debug.Log(MapManager.Instance.Hole_PR + "||" + MapManager.Instance.Skyspikes_PR + "||" + MapManager.Instance.Spikes_PR);
        }
        //Debug.Log("Test");
        GamePanelController.Instance.UpdateCurrentScore(playerStep);
        SetTileColor();
    }
    /// <summary>
    /// 为角色走过的路径设置颜色
    /// </summary>
    /// <param name="玩家所在地图地板位置"></param>
    private void SetTileColor()
    {
        if (z % 2 == 1)
        {
            JudgeTileKindAndSetColor(colorOne);

        }
        else
        {
            JudgeTileKindAndSetColor(colorTwo);

        }
    }
    /// <summary>
    /// 角色碰陷阱的碰撞检测
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpikesSon") || other.gameObject.CompareTag("SkySpikesSon"))
        {
            AudioManager.Instance.CreateAudioClip(audioClip_Spikes, GameObject.Find("Main Camera").transform.position);
            PlayerDie();

            isControl = false;                                  //取消角色控制权
            CountDownTextPanel.Instance.Invoke("StopCountDown", 0);
        }
        if (other.gameObject.CompareTag("Gold"))
        {
            AudioManager.Instance.PlayAudio("GetGoldSound");
            GamePanelController.Instance.UpdataGoldNumShow();

            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Black"))
        {
            GamePanelView m_GamePanelView = GameObject.Find("GamePanel").GetComponent<GamePanelView>();
            SetCameraMove.Instance.StartCameraShake();
            m_GamePanelView.Tr_CountDownTextPanel.gameObject.SetActive(true);
            CountDownTextPanel.Instance.StartCountDown();
            CountDownTextPanel.Instance.Invoke("StopCountDown", 3f);
            SetCameraMove.Instance.Invoke("StopCameraShake", 3f);
            MapManager.Instance.StopFloorDownCoroutine();
            MapManager.Instance.Invoke("StartFloorDownCoroutine", 3f);
            isFindTrap = true;
            Invoke("TriggerBlack", 3f);
            


            Destroy(other.gameObject);
        }
    }
    /// <summary>
    /// 角色从坑洞陷阱掉落
    /// </summary>
    private void PlayerDown()
    {
        PlayerDie();
        //CountDownTextPanel.Instance.Invoke("StopCountDown", 0);
    }
    /// <summary>
    /// 判断地面资源类型并设置颜色
    /// </summary>
    /// <param name="颜色种类"></param>
    private void JudgeTileKindAndSetColor(Color color)
    {
        //Debug.Log("Test");
        if (playerPos.CompareTag("Hole"))
        {
            isControl = false;
            Destroy(playerPos.GetComponent<BoxCollider>());
        }
        else if (playerPos.CompareTag("Tile"))
        {
            playerPos.Find("normal_a2").GetComponent<MeshRenderer>().material.color = color;

        }
        else if (playerPos.CompareTag("Spikes"))
        {
            playerPos.Find("moving_spikes_a2").GetComponent<MeshRenderer>().material.color = color;
        }
        else if (playerPos.CompareTag("SkySpikes"))
        {
            playerPos.Find("smashing_spikes_a2").GetComponent<MeshRenderer>().material.color = color;
        }
        else if (playerPos.CompareTag("GoldItem"))
        {
            playerPos.Find("normal_a2").GetComponent<MeshRenderer>().material.color = color;

        }
        else if (playerPos.CompareTag("BlackItem"))
        {
            playerPos.Find("normal_a2").GetComponent<MeshRenderer>().material.color = color;

        }

    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("aaaaaaaaaa");
        //AudioManager.Instance.PlayAudio("spikesClip");      //播放死亡音效
        PlayerDie();


    }
    private void TriggerBlack()
    {
        isFindTrap = false;
        for (int i = 0; i < m_Spikes.Length; i++)
        {
            Spikes spikes = m_Spikes[i].GetComponent<Spikes>();
            spikes.StartAnimPlay();
        }
       
        for (int i = 0; i < m_SkySpikes.Length; i++)
        {
            SkySpikes skySpikes = m_SkySpikes[i].GetComponent<SkySpikes>();

            skySpikes.StartAnimPlay();
        }
        for (int i = 0; i < m_Turret.Length; i++)
        {
            Turret turret = m_Turret[i].GetComponent<Turret>();

            turret.StartPlayFire();
        }
    }
    private void PlayerDie()
    {
        AudioManager.Instance.StopBGM();                //停止背景音乐
        Time.timeScale = 0;                             //暂停游戏
        isControl = false;                              //取消角色控制权
        AudioManager.Instance.PlayAudio("GameOverSound");
        GamePanelController.Instance.StartGameOverAnimaPlay();
        //SendMessage("")
        z = 3;
        x = 2;
        

        
    }
    private void ResetPlayer()
    {
        z = 3;
        x = 2;
    }
    
}
