using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public int count = 10;
    public GameObject prefab;

    public List<GameObject> spawned = new List<GameObject>();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(var i= spawned.Count-1; i>=0;i--)
        {
            if(spawned[i] == null)
            {
                spawned.RemoveAt(i);
            }
        }


        if(spawned.Count < count)
        {
            var g = Instantiate(prefab);
            g.transform.position = new Vector3() + this.transform.position;
            spawned.Add(g);
        }
	}
}
