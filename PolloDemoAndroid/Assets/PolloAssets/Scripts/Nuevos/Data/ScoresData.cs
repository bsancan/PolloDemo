using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoresData
{

    public string name;
    public int score;

    public ScoresData[] scores;

    public ScoresData(ScoresData sd)
    {
        this.name = sd.name;
        this.score = sd.score;
    }

    public ScoresData(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public void SetDefaulScoreData()
    {

    }

    public ScoresData()
    {
        name = string.Empty;
        score = 0;
        scores = new ScoresData[10];
    }

}
