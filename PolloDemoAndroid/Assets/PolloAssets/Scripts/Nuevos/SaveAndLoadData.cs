using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;


public class SaveAndLoadData
{

    //public SL_playersScoreList[] scores;
}

[Serializable]
public class SL_playersScoreList
{
    public string name;
    public int score;

    public SL_playersScoreList[] scores;

    public SL_playersScoreList(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public SL_playersScoreList()
    {
        this.name = string.Empty;
        this.score = 0;
    }
}
