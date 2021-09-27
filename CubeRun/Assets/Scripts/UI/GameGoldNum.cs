using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoldNum 
{
    private int goldNum;

    public int GoldNum { get => goldNum; set => goldNum = value; }
    public GameGoldNum()
    {
    }
    public GameGoldNum(int goldNum)
    {
        this.goldNum = goldNum;
    }
}

