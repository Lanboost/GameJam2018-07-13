using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public SpawnWaves spawnwaves;

    public List<Vector3> pathCoords = new List<Vector3>();

    private int time = 0;

    private int waveid = 0;
    private bool running = false;
    public bool wavedone = false;

    // Use this for initialization
    void Start () {
		
	}

    public void startNextWave()
    {
        waveid++;
        
        if (waveid < spawnwaves.waves.Count)
        {
            wavedone = false;
            running = true;
            time = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (running)
        {
            var maxtime = 0;
            var before = time;
            time += (int)(Time.deltaTime * 1000);

            foreach (var wave in spawnwaves.waves[waveid].waves)
            {
                for (var i = 0; i < wave.count; i++)
                {
                    var shouldHaveSpawned = wave.delay + wave.interval * i;
                    if(maxtime < shouldHaveSpawned)
                    {
                        maxtime = shouldHaveSpawned;
                    }

                    //Debug.Log("Before: "+before+" , ShouldHaveSpawned:" + shouldHaveSpawned+ " time: "+time);
                    if (before <= shouldHaveSpawned && shouldHaveSpawned <= time)
                    {
                        var g = Instantiate(wave.monster);
                        g.GetComponent<Enemy>().init(this);
                    }
                }
            }

            if(maxtime < time)
            {
                wavedone = true;
            }

        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (pathCoords.Count >= 2)
        {
            var last = pathCoords[0];
            foreach (var v in pathCoords.GetRange(0, pathCoords.Count))
            {

                Gizmos.DrawLine(last, v);
                last = v;
            }
        }
    }
}
