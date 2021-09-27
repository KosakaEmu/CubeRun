//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AuidoManager : MonoBehaviour
//{
//    private static AuidoManager instance;
//    private AudioSource[] m_AudioSource;
//    private AudioClip[] m_AudioClip;
//    private AudioClip spikesClip;

//    public static AuidoManager Instance { get => instance; set => instance = value; }

//    void Awake()
//    {
//        instance = this;
//    }
//    void Start()
//    {
//        m_AudioSource = gameObject.GetComponents<AudioSource>();
//        m_AudioClip = Resources.LoadAll<AudioClip>("BGM");
//        spikesClip = Resources.Load<AudioClip>("GameSound/spikesClip");
//        RandomAudioPlayBGM();
//        m_AudioSource[1].playOnAwake = false;

//    }

//    void Update()
//    {
//        if (GameObject.Find("PlayerManager/Player").GetComponent<Player>().PlayerPos == null)
//        {
//            return;
//        }
//        transform.position = GameObject.Find("PlayerManager/Player").GetComponent<Player>().PlayerPos.position;
//    }
//    private void RandomAudioPlayBGM()
//    {
//        m_AudioSource[0].playOnAwake = false;
//        int f = Random.Range(0, 2);
//        if (f == 0)
//        {
//            m_AudioSource[0].clip = m_AudioClip[f];
//            //m_AudioSource[0].volume = 0.2f;
//            m_AudioSource[0].volume = 0.2f;
//            m_AudioSource[0].Play();
            
//        }
//        m_AudioSource[0].clip = m_AudioClip[f];
//        m_AudioSource[0].Play();
//    }
//    private void BGMStart()
//    {
//        m_AudioSource[0].Play();
//    }
//    private void BGMStop()
//    {
//        m_AudioSource[0].Stop();
//    }
//    private void SpikesTriggerAudio()
//    {
//        Debug.Log("游戏音效没有触发1");


//        m_AudioSource[1].PlayOneShot(spikesClip,1f);
//        //m_AudioSource[1].clip = spikesClip;
//        //m_AudioSource[1].Play();
//        Debug.Log("游戏音效没有触发2");

//    }
//}
