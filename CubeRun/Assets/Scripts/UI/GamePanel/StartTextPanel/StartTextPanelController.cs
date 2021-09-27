using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextPanelController : MonoBehaviour
{
    private static StartTextPanelController instance;

    private GameObject textEffect_f_p;
    private GameObject textEffect_1_p;
    private GameObject textEffect_2_p;
    private GameObject textEffect_3_p;

    private List<GameObject> effectPrafabs_List =null;


    public static StartTextPanelController Instance { get => instance;  }
    private void Awake()
    {
        instance = this;
        textEffect_f_p = Resources.Load<GameObject>("TextEffect/TextEffect_F");
        textEffect_1_p = Resources.Load<GameObject>("TextEffect/TextEffect_1");
        textEffect_2_p = Resources.Load<GameObject>("TextEffect/TextEffect_2");
        textEffect_3_p = Resources.Load<GameObject>("TextEffect/TextEffect_3");
        effectPrafabs_List = new List<GameObject>();
    }
    /// <summary>
    /// 播放开始游戏倒计时动画
    /// </summary>
    public void PlayTextEffect()
    {
        effectPrafabs_List.Add(textEffect_3_p);
        effectPrafabs_List.Add(textEffect_2_p);
        effectPrafabs_List.Add(textEffect_1_p);
        effectPrafabs_List.Add(textEffect_f_p);

        StartCoroutine("DelayCreatEffect");
        //Invoke("DelayStartGame", 4f);
        StartCoroutine("DelayStartGame");
        AudioManager.Instance.PlayAudio("321FightSound");
    }
    /// <summary>
    /// 创建倒计时特效动画
    /// </summary>
    /// <returns>1s执行一个特效</returns>
    private IEnumerator DelayCreatEffect()
    {
        for (int i = 0; i < effectPrafabs_List.Count; i++)
        {
            GameObject.Instantiate<GameObject>(effectPrafabs_List[i], transform);
            yield return new WaitForSecondsRealtime(1f);
        }
        

    }
    /// <summary>
    /// 游戏4s后正式开始
    /// </summary>
    private IEnumerator DelayStartGame()
    {
        yield return new WaitForSecondsRealtime(4f);
        GameM.Instance.StartGame();
    }
    /// <summary>
    /// 停止两个协程
    /// </summary>
    public void StopTwoCoroutine()
    {
        StopCoroutine("DelayCreatEffect");
        StopCoroutine("DelayStartGame");
    }

}
