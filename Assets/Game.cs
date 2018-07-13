using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    public Level level;

    public int state = 0;
    public int time = 30000;
    public int wave = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(state == 0)
        {
            time -= (int)(Time.deltaTime * 1000);
            if(time < 0)
            {
                state = 1;
                time = 0;
                wave = 1;
                var spawners = FindObjectsOfType<Spawner>();
                foreach(var s in spawners)
                {
                    s.startNextWave();
                }
            }
        }
        else if (state == 1)
        {
            time += (int)(Time.deltaTime * 1000);

            var spawners = FindObjectsOfType<Spawner>();
            var alldone = true;
            foreach (var s in spawners)
            {
                if(!s.wavedone)
                {
                    alldone = false;
                }
            }

            if(alldone && FindObjectOfType<Enemy>() == null)
            {
                state = 0;
                time = 30000;
            }
        }
    }
}
