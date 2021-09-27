using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class Sound
    {
        [Header("音频剪辑")]
        public AudioClip clip;
        [Header("音频音量")]
        public float volume;
        [Header("音频是否循环播放")]
        public bool isloop;
    }

    private static AudioManager instance;
    private AudioSource source;
    public List<Sound> sounds = new List<Sound>();//存储所有音频的信息
    private Dictionary<string, AudioSource> audioSourceDic = new Dictionary<string, AudioSource>();
    private int flag;
    private float defaultVolume = 1;

    public static AudioManager Instance { get => instance; set => Instance = value; }

    void Awake()
    {
        instance = this;
        
        foreach (var sound in sounds)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);

            source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.loop = sound.isloop;
            audioSourceDic.Add(sound.clip.name, source);
            //Debug.Log(sound.clip.name);
            
            
        }
        PlayStartPanelBGM();
    }

    public void PlayAudio(string name,int flag = 0,bool iswait = false )
    {
        
        if (!audioSourceDic.ContainsKey(name))
        {
            //不存在次音频
            Debug.LogError("不存在" + name + "音频");
            return;
        }
        if (iswait)
        {
            if (audioSourceDic[name].isPlaying)
            {
                //如果是等待的情况 不在播放
                audioSourceDic[name].Play();
            }
        }
        else
        {
            //直接播放
            this.flag = flag;
            audioSourceDic[name].volume = defaultVolume;
            audioSourceDic[name].Play();
        }
    }
    public void StopAudio(string name)
    {
        if (!audioSourceDic.ContainsKey(name))
        {
            //不存在次音频
            Debug.LogError("不存在" + name + "音频");
            return;
        }
        else
        {
            audioSourceDic[name].Stop();
            
        }
    }
    public void PausedBGM()
    {
        Debug.Log("flag:" + flag);
        if (flag == 1)
        {
            audioSourceDic["playGameBGM01"].Pause();
        }
        else
        {
            audioSourceDic["playGameBGM02"].Pause();
        }  
    }
    public void ContinueBGM()
    {
        if (flag == 1)
        {
            audioSourceDic["playGameBGM01"].Play();
        }
        else
        {
            audioSourceDic["playGameBGM02"].Play();
        }
    }
    public void StopBGM()
    {
        audioSourceDic["playGameBGM01"].Stop();
        audioSourceDic["playGameBGM02"].Stop();
    }
    public void PausedStartPanelBGM()
    {
        audioSourceDic["startGameUIBGM"].Pause();
    }
    public void PlayStartPanelBGM()
    {
        audioSourceDic["startGameUIBGM"].Play();
    }
    //public void ControlAudioVolume(string name,float value)
    public void ControlAudioVolume(float value)
    {
        //if (!audioSourceDic.ContainsKey(name))
        //{
        //    //不存在次音频
        //    Debug.LogError("不存在" + name + "音频");
        //    return;
        //}
        //else
        //{

        //}
        foreach (var item in audioSourceDic)
        {
            audioSourceDic[item.Key].volume = value;
        }
        defaultVolume = value;
    }
    public void CreateAudioClip(AudioClip audioClipName,Vector3 clipPos)
    {
        AudioSource.PlayClipAtPoint(audioClipName, clipPos, defaultVolume);
    }
    public void PlayFireAudio()
    {
        audioSourceDic["startGameUIBGM"].Play();
        
        audioSourceDic[name].spatialBlend = 1;

    }


}
