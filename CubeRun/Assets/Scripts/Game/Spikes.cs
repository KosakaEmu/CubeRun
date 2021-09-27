using UnityEngine;
using UnityEditor;
using System.Collections;
public class Spikes : MonoBehaviour
{
    private Animator m_Animator;
    private Transform m_Transform;
    private AnimationClip spikesAnimaClip;
    private float delayTime;

    private Vector3 tempPos;
    void Start()
    {
        spikesAnimaClip = Resources.Load<AnimationClip>("AnimationClip/spikesAnim");
        m_Transform = transform;
        m_Animator = gameObject.transform.Find("moving_spikes_b").GetComponent<Animator>();
        tempPos = m_Transform.position;
        StopAnim();
        StartCoroutine("DelayAnimPlayTime");

    }
    private IEnumerator DelayAnimPlayTime()
    {
        delayTime=Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(delayTime);
        m_Animator.Play(spikesAnimaClip.name, 0);
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
            m_Animator.Play(spikesAnimaClip.name, 0);
            m_Animator.speed = 0;
        }
         

    }
    //public void StartSpikesShake()
    //{
    //    StartCoroutine("SpikesShake");
    //}
    //public void StopSpikesShake()
    //{
    //    StopCoroutine("SpikesShake");
    //    m_Transform.position = tempPos;
    //}
    //private IEnumerator SpikesShake()
    //{
    //    int i = 0;

    //    while (true)
    //    {

    //        if (i % 2 == 0)
    //        {

    //            m_Transform.position = new Vector3(m_Transform.position.x + Random.Range(0f, 0.02f), m_Transform.position.y + Random.Range(0f, 0.02f), m_Transform.position.z + Random.Range(0f, 0.02f));

    //        }
    //        else
    //        {
    //            m_Transform.position = new Vector3(m_Transform.position.x - Random.Range(0f, 0.02f), m_Transform.position.y - Random.Range(0f, 0.02f), m_Transform.position.z - Random.Range(0f, 0.02f));

    //        }
    //        i++;
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //}
}
