using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager2 : MonoBehaviour
{
    //��Ƶ������ �洢���е���Ƶ���ҿ��Բ��ź�ֹͣ

    [Serializable]
    public class Sound
    {
        [Header("��Ƶ����")]
        public AudioClip clip;
        
        [Header("��Ƶ����")]
        [Range(0, 1)]
        public float volume;

        [Header("��Ƶ�Ƿ�������")]
        public bool PlayOnAwake;

        [Header("��Ƶ�Ƿ�Ҫѭ������")]
        public bool loop;
    }

    public List<Sound> aaaa;//�洢������Ƶ����Ϣ

    public List<string> bbbb;

    private Dictionary<string, AudioSource> audioDic;//ÿһ����Ƶ���������

    private static AudioManager2 instance;

    public static AudioManager2 Instance { get => instance; set => instance = value; }




    private void Awake()
    {
        Debug.Log("ss" + aaaa);

        Debug.Log("testbcccccccccccccccccccccccccc");
        instance = this;
        
        audioDic = new Dictionary<string, AudioSource>();
        

    }
    private void Start()
    {
        aaaa = new List<Sound>();
        bbbb = new List<string>();
        bbbb.Add("dwad");
        bbbb.Add("dfwdq");
        Debug.Log("testaaaaaaaaaaaaaaaaaaaaaaaaa");
        Sound s = new Sound();

        Debug.Log("s"+s);
        aaaa.Add(s);
        foreach (var item in bbbb)
        {
            Debug.Log(item);
        }
        foreach (var sound in aaaa)
        {
            Debug.Log("testbbbbbbbbbbbbbbb");
            GameObject obj = new GameObject();


            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.playOnAwake = sound.PlayOnAwake;
            source.loop = sound.loop;
            


            if (sound.PlayOnAwake)
            {
                source.Play();
            }
            Debug.Log("sound.clip:" + sound.clip);
            Debug.Log("sound.clip.name:" + sound.clip.name);
            Debug.Log("source:" + source);
            audioDic.Add(sound.clip.name, source);
        }
    }


    //����ĳ����Ƶ�ķ��� iswaitΪ�Ƿ�ȴ�
    public static void PlayAudio(string name, bool iswait = false)
    {
        if (!instance.audioDic.ContainsKey(name))
        {
            //�����ڴ���Ƶ
            Debug.LogError("������" + name + "��Ƶ");
            return;
        }
        if (iswait)
        {
            if (!instance.audioDic[name].isPlaying)
            {
                //����ǵȴ������ ���ڲ���
                instance.audioDic[name].Play();
            }
        }
        else
        {
            //ֱ�Ӳ���
            instance.audioDic[name].Play();
        }
    }


    //ֹͣ��Ƶ�Ĳ���
    public static void StopMute(string name)
    {

        if (!instance.audioDic.ContainsKey(name))
        {
            //�����ڴ���Ƶ
            Debug.LogError("������" + name + "��Ƶ");
            return;
        }
        else
        {
            instance.audioDic[name].Stop();

        }
    }

}
