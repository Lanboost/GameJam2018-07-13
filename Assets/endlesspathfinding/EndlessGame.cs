using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessGame : MonoBehaviour {
    float goldtick = 1;
    public int gold = 100;

    PathFinding map = new PathFinding();
	// Use this for initialization
	void Start () {
        map.runFirst(50, 50);

    }
	
	// Update is called once per frame
	void Update () {
        goldtick -= Time.deltaTime;
        if(goldtick < 0)
        {
            gold += 1;
            goldtick += 1;
        }
	}
}
