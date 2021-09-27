using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataItem 
{
    private int playerStep;
    private string endTime;

    
    public int PlayerStep { get => playerStep; set => playerStep = value; }
    public string EndTime { get => endTime; set => endTime = value; }

    public GameDataItem(){ }
    public GameDataItem(int playerStep, string endTime)
    {
        this.playerStep = playerStep;
        this.endTime = endTime;
    }
    public override string ToString()
    {
        return string.Format("step:{0}-time:{1}", playerStep, endTime);
    }
}
