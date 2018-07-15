using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessGame : MonoBehaviour {
    float goldtick = 1;
    public int gold = 100;

    public PathFinding map = new PathFinding();

    public GameObject enemy;

    public float enemyTick = 5;

    public Tower test;

    void PlaceTower(int x, int z)
    {
        var o = Instantiate(test);
        o.transform.position = new Vector3(x+200, 0, z + 200);
        map.place(x, z, o);
    }

    // Use this for initialization
    void Start () {
        GameObject.FindObjectOfType<GameData>().score = 0;
        this.GetComponent<AudioSource>().volume = GameObject.FindObjectOfType<GameData>().music;


        map.runFirst(50, 50);
        //map.place(55, 55);
        for (int i = 0; i <= 10; i++)
        {
            PlaceTower(55, 45 + i);
            PlaceTower(45, 45 + i);
        }

        for (int i = 1; i <= 9; i++)
        {
            PlaceTower(45 + i, 55);
            PlaceTower(45 + i, 45);
        }
        //map.destroy(50, 55);


    }
	
	// Update is called once per frame
	void Update () {
        goldtick -= Time.deltaTime;
        if(goldtick < 0)
        {
            gold += 1;
            goldtick += 1;

            GameObject.FindObjectOfType<GameData>().score += 1;
        }

        enemyTick -= Time.deltaTime;
        if (enemyTick < 0)
        {
            var o = Instantiate(enemy);
            o.transform.position = new Vector3(280, 0, 280);
            enemyTick += 5;
        }

         
    }

    private void OnDrawGizmosSelected()
    {
        map.drawDebug();
    }
}
