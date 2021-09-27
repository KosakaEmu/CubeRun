using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTextPanel : MonoBehaviour
{
    private static CountDownTextPanel instance;

    private GameObject textEffect_cd_3;
    private GameObject textEffect_cd_2;
    private GameObject textEffect_cd_1;

    private List<GameObject> effectPrafabs_cd_List = null;

    public static CountDownTextPanel Instance { get => instance;  }


    private void Awake()
    {
        instance = this;
        
        textEffect_cd_3 = Resources.Load<GameObject>("TextEffect/CountDown/TextEffect_cd_3");
        textEffect_cd_2 = Resources.Load<GameObject>("TextEffect/CountDown/TextEffect_cd_2");
        textEffect_cd_1 = Resources.Load<GameObject>("TextEffect/CountDown/TextEffect_cd_1");

        effectPrafabs_cd_List = new List<GameObject>();
        gameObject.SetActive(false);

    }
    public void StartCountDown()
    {
        effectPrafabs_cd_List.Clear();
        effectPrafabs_cd_List.Add(textEffect_cd_3);
        effectPrafabs_cd_List.Add(textEffect_cd_2);
        effectPrafabs_cd_List.Add(textEffect_cd_1);
        StartCoroutine("DelayCreatEffect");
        
        AudioManager.Instance.PlayAudio("CountDownSound");
    }
    public void StopCountDown()
    {
        StopCoroutine("DelayCreatEffect");
        
        if (effectPrafabs_cd_List != null)
        {
            for (int i = 0; i < effectPrafabs_cd_List.Count; i++)
            {
                Destroy(transform.Find("TextEffect_cd_" + (3 - i)).gameObject);
                
            }
        }
        effectPrafabs_cd_List.Clear();

        AudioManager.Instance.StopAudio("CountDownSound");
        gameObject.SetActive(false);
    }
    private IEnumerator DelayCreatEffect()
    {
        for (int i = 0; i < effectPrafabs_cd_List.Count; i++)
        {
            GameObject temp = Instantiate<GameObject>(effectPrafabs_cd_List[i], transform);
            temp.name = "TextEffect_cd_" + (3 - i);
            yield return new WaitForSecondsRealtime(1f); 
        }
    }
}
