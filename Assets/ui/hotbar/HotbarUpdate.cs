using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarUpdate : MonoBehaviour {
    public TowerBuildList list;
    public ApplyRTT prefab;
	// Use this for initialization
	void Start () {
		for(var i=0; i< list.items.Count; i++)
        {
            var o = Instantiate(prefab);
            o.gameObject.transform.parent = this.transform;
            o.index = i;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
