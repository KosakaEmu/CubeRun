using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager2 : MonoBehaviour
{
    //音频管理器 存储所有的音频并且可以播放和停止

    [Serializable]
    public class Sound
    {
        [Header("音频剪辑")]
        public AudioClip clip;
        
        [Header("音频音量")]
        [Range(0, 1)]
        public float volume;

        [Header("音频是否自启动")]
        public bool PlayOnAwake;

        [Header("音频是否要循环播放")]
        public bool loop;
    }

    public List<Sound> aaaa;//存储所有音频的信息

    public List<string> bbbb;

    private Dictionary<string, AudioSource> audioDic;//每一个音频的名称组件

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


    //播放某个音频的方法 iswait为是否等待
    public static void PlayAudio(string name, bool iswait = false)
    {
        if (!instance.audioDic.ContainsKey(name))
        {
            //不存在次音频
            Debug.LogError("不存在" + name + "音频");
            return;
        }
        if (iswait)
        {
            if (!instance.audioDic[name].isPlaying)
            {
                //如果是等待的情况 不在播放
                instance.audioDic[name].Play();
            }
        }
        else
        {
            //直接播放
            instance.audioDic[name].Play();
        }
    }


    //停止音频的播放
    public static void StopMute(string name)
    {

        if (!instance.audioDic.ContainsKey(name))
        {
            //不存在次音频
            Debug.LogError("不存在" + name + "音频");
            return;
        }
        else
        {
            instance.audioDic[name].Stop();

        }
    }

}
