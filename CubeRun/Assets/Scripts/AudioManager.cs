using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class Sound
    {
        [Header("��Ƶ����")]
        public AudioClip clip;
        [Header("��Ƶ����")]
        public float volume;
        [Header("��Ƶ�Ƿ�ѭ������")]
        public bool isloop;
    }

    private static AudioManager instance;
    private AudioSource source;
    public List<Sound> sounds = new List<Sound>();//�洢������Ƶ����Ϣ
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
            //�����ڴ���Ƶ
            Debug.LogError("������" + name + "��Ƶ");
            return;
        }
        if (iswait)
        {
            if (audioSourceDic[name].isPlaying)
            {
                //����ǵȴ������ ���ڲ���
                audioSourceDic[name].Play();
            }
        }
        else
        {
            //ֱ�Ӳ���
            this.flag = flag;
            audioSourceDic[name].volume = defaultVolume;
            audioSourceDic[name].Play();
        }
    }
    public void StopAudio(string name)
    {
        if (!audioSourceDic.ContainsKey(name))
        {
            //�����ڴ���Ƶ
            Debug.LogError("������" + name + "��Ƶ");
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
        //    //�����ڴ���Ƶ
        //    Debug.LogError("������" + name + "��Ƶ");
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
