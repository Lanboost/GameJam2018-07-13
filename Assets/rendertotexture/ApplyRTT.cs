using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyRTT : MonoBehaviour {
    public PrefabSprite data;
    public int index = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(data.items[index].rt != null)
        {
            this.GetComponent<RawImage>().texture = data.items[index].rt;
        }
	}
}
