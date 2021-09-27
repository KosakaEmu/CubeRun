using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem 
{
    private int rankScore;
    private string rankTime;

    public int RankScore { get => rankScore; set => rankScore = value; }
    public string RankTime { get => rankTime; set => rankTime = value; }
    public RankItem(){ }

    public RankItem(int rankScore,string rankTime)
    {
        this.rankScore = rankScore;
        this.rankTime = rankTime;

    }

    
}
