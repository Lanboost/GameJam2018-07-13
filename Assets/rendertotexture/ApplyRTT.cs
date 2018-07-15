using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyRTT : MonoBehaviour {
    public TowerBuildList data;
    public int index = 0;

    public Text text;
    public Text cost;
    public RawImage image;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(data.items[index].rt != null)
        {
            image.texture = data.items[index].rt;
        }
        text.text = ""+index + 1;
        cost.text = "$" + data.items[index].cost;
    }
}
