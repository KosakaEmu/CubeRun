using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private ParticleSystem m_ParticleSystem;
    private AudioClip clip_Fire;
    private static bool isPlayNotFound = false;

    public static bool IsPlayNotFound { get => isPlayNotFound; set => isPlayNotFound = value; }

    void Start()
    {
        m_ParticleSystem = transform.Find("FireThrower").GetComponent<ParticleSystem>();
        clip_Fire = Resources.Load<AudioClip>("GameSound/FlameJetSound");
        if (isPlayNotFound)
        {
            StartPlayFire();

        }
        else
        {
            StartPlayFire2();
        }
    }

    void Update()
    {
        
    }
    public void StartPlayFire()
    {
        StartCoroutine("PlayEffect");
    }
    public void StopPlayFire()
    {
        StopCoroutine("PlayEffect");
    }
    private IEnumerator PlayEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            m_ParticleSystem.Play();
            //AudioSource.PlayClipAtPoint(clip_Fire,transform.position,0.3f);
            AudioManager.Instance.CreateAudioClip(clip_Fire, transform.position);

        }
    }
    public void StartPlayFire2()
    {
        StartCoroutine("PlayEffectNotAudio");
    }
    public void StopPlayFire2()
    {
        StopCoroutine("PlayEffectNotAudio");
    }
    private IEnumerator PlayEffectNotAudio()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            m_ParticleSystem.Play(); 
        }
    }
}
