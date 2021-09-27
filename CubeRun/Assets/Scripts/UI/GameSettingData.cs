using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingData 
{
    private double volumeValue;
    public double VolumeValue { get => volumeValue; set => volumeValue = value; }

    public GameSettingData(){ }
    public GameSettingData(double volumeValue) 
    {
        this.volumeValue = volumeValue;
    }


}
