using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class test22 : MonoBehaviour
{

    [Serializable]
    public class AA
    {
        [Header("��Ƶ����")]
        public float no1;
        [Header("��Ƶ����")]
        public AudioClip clip;
    }
    AudioSource source;
    public List<AA> aaaa = new List<AA>();//�洢������Ƶ����Ϣ
    void Start()
    {
        Debug.Log("aaaa:" + aaaa);
        foreach (var sound in aaaa)
        {
            Debug.Log("testbbbbbbbbbbbbbbb");
            GameObject obj = new GameObject();

            source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.volume = sound.no1;

        }
        source.Play();



    }
}
