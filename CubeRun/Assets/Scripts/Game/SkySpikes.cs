using UnityEngine;
using UnityEditor;
using System.Collections;
public class SkySpikes : MonoBehaviour
{
    private Animator m_Animator;
    private Transform m_Transform;
    private AnimationClip skySpikesClip;
    private float delayTime;

    private Vector3 tempPos;
    void Start()
    {
        skySpikesClip = Resources.Load<AnimationClip>("AnimationClip/skyspikesAnim");
        m_Transform = transform;
        m_Animator = gameObject.transform.Find("smashing_spikes_b").GetComponent<Animator>();
        tempPos = m_Transform.position;

        StopAnim();
        StartCoroutine("DelayAnimPlayTime");
    }
    private IEnumerator DelayAnimPlayTime()
    {
        delayTime = Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(delayTime);
        m_Animator.Play(skySpikesClip.name, 0);
        m_Animator.speed = 1;
    }
    public void StartAnimPlay()
    {
        StartCoroutine("DelayAnimPlayTime");
    }
    public void StopAnim()
    {
        if (m_Animator != null)
        {
            m_Animator.Play(skySpikesClip.name, 0);
            m_Animator.speed = 0;
        }
        
    }
   
}
