using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpawnWave
{
    public GameObject monster;
    public int count;
    public int delay;
    public int interval;
}

[System.Serializable]
public class Wave
{
    public List<SpawnWave> waves;
}