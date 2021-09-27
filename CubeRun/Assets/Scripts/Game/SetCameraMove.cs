using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SetCameraMove : MonoBehaviour
{
    private static SetCameraMove instance;
    private Transform m_Transform;
    float offsetZ;
    private bool isAutoMove=false;
    public static SetCameraMove Instance { get => instance;  }

    float speed = 0.008f;

    //什么时候开始运动
    float startTime = 0f;

    //起始X位置
    float startX = 0.0f;

    //结束X位置
    //结束X位置
    float endX = 100.0f;

    //float fDefaultRatio = 720.0f / 1280.0f;//预先设定屏幕大小1280*720
    float fDefaultRatio = 1080.0f / 1920.0f;//预先设定屏幕大小1280*720
    // Use this for initialization


    //void Update()
    //{


    //        float lerpValue = Mathf.Lerp(startX, endX, (Time.time - startTime) * speed);
    //        transform.position = new Vector3(0.9f, 2.279f, lerpValue);


    //    float targetPos = StartPanelMapManager.Instance.MapList[StartPanelMapManager.Instance.MapList.Count - 10][0].transform.position.z;

    //}
    private void Awake()
    {
        instance = this;
        m_Transform = gameObject.transform;
    }
    void Start()
    {
        float fRatio = (float)Screen.height / Screen.width;
        Camera.main.orthographicSize = Camera.main.orthographicSize * fRatio * fDefaultRatio;
        //Debug.Log("Screen.width:" + Screen.width);
        //Debug.Log("Screen.height:" + Screen.height);
        //Debug.Log("Camera.main.orthographicSize:"+ Camera.main.orthographicSize);
        StartCameraAutoMove();
    }
    //private void LateUpdate()
    //{
    //    //if (isAutoMove)
    //    //{
    //    //    transform.position = transform.position + new Vector3(0, 0, 0.002f);
    //    //}
    //}
    private void FixedUpdate()
    {
        if (isAutoMove)
        {
            transform.position = transform.position + new Vector3(0, 0, 0.0001f);
            //transform.position = new Vector3(transform.position.x, transform.position.y, float.Parse(transform.position.z.ToString("f2")));
        }

    }
    public void StartCameraShake()
    {
        StartCoroutine("CameraShake");
    }
    public void StopCameraShake()
    {
        StopCoroutine("CameraShake");
    }
    private IEnumerator CameraShake()
    {
        int i = 0;
        
        while (true)
        {

            if (i % 2 == 0)
            {

                m_Transform.position = new Vector3(m_Transform.position.x + Random.Range(0f, 0.02f), m_Transform.position.y + Random.Range(0f, 0.02f), m_Transform.position.z + Random.Range(0f, 0.02f));

            }
            else
            {
                m_Transform.position = new Vector3(m_Transform.position.x - Random.Range(0f, 0.02f), m_Transform.position.y - Random.Range(0f, 0.02f), m_Transform.position.z - Random.Range(0f, 0.02f));

            }
            i++;
            yield return new WaitForSecondsRealtime(0.07f);
        }
    }
    /// <summary>
    /// 摄像机跟随角色移动
    /// </summary>
    public void CameraMove()
    {
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        //Debug.Log(playerTransform.position);
        //Debug.Log(m_Transform.position);
        Vector3 target;
        target = new Vector3(0.9f, playerTransform.position.y + 1.748f, playerTransform.position.z -0.0555f);
        //Debug.Log(target);

        m_Transform.position= Vector3.Lerp(m_Transform.position, target, Time.deltaTime);
    }
    public void SetCameraPos()
    {
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        m_Transform.position= new Vector3(0.9f, playerTransform.position.y + 1.748f, playerTransform.position.z - 0.0555f);
    }
    public void StartCameraAutoMove()
    {
        StartCoroutine("CameraAutoMove");
    }
    public void StopCameraAutoMove()
    {
        StopCoroutine("CameraAutoMove");
        //isAutoMove = false;
    }
    private IEnumerator CameraAutoMove()
    {
        while (true)
        {
            transform.position = transform.position + new Vector3(0, 0, 0.01f);
            yield return new WaitForSecondsRealtime(0.005f);
        }
        
    }

}
