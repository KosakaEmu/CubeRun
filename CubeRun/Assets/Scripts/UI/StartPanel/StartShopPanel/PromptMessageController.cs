using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
public class PromptMessageController : MonoBehaviour
{

    void Start()
    {
        StartCoroutine("DelayFade");
    }


    void Update()
    {
        
    }
    private IEnumerator DelayFade()
    {
        yield return new WaitForSecondsRealtime(1f);
        gameObject.GetComponent<Image>().DOFade(0, 1);
    }
}
