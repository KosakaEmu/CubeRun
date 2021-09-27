using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankItemController : MonoBehaviour
{
    private Text text_Score;
    private Text text_Time;
    private string Time1;
    private string Time2;
    void Awake()
    {
        text_Score = transform.Find("ScoreTip/Score").GetComponent<Text>();
        text_Time = transform.Find("TimeTip/Time").GetComponent<Text>();
        List<int> a = new List<int>();
        
    }
    public void Init(int score,string time)
    {
        //Debug.Log(score);
        text_Score.text = score.ToString();
        //Debug.Log(time);

        //Debug.Log(time.Length);
        Time1 = time.Substring(0, time.Length - 6);
        Time2 = time.Substring(time.Length - 5);
        //Debug.Log(Time1);

        //Time1 = time.Substring(time.Length - 6, time.Length-1);
        
        //Debug.Log(Time2);

        text_Time.text = Time1+"\n"+ "  " + Time2;
    }

}
