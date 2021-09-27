using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class sdqwdq : MonoBehaviour
{
    private Text a;
    void Start()
    {
        DateTime time = DateTime.Now;
        a = gameObject.GetComponent<Text>();
        a.text = string.Format("{0}/{1}/{2}/{3}:{4}", time.Year, time.Month, time.Day, time.Hour, time.Minute);
    }

    void Update()
    {
        
    }
}
