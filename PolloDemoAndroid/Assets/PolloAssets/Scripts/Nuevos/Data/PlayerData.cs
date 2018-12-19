using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 

{
    public int lastLevel;
    public float[] lastPosition;

    public PlayerData()
    {
        lastLevel = 0;
        lastPosition = new float[3];
    }

    public PlayerData(int level, Vector3 pos)
    {
        lastLevel = level;
        lastPosition = new float[3];
        lastPosition[0] = pos.x;
        lastPosition[1] = pos.y;
        lastPosition[2] = pos.z;
    }
}
