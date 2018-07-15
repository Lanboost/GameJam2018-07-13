using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class GameInfoUpdater : MonoBehaviour {
    public Text intotext;
    public Text wavetext;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var str = "";
        str += GameObject.FindObjectOfType<Game>().level.level + "\n\n";
        str += GameObject.FindObjectOfType<Game>().wave + "\n\n";

        var t = GameObject.FindObjectOfType<Game>().state == 1 ? GameObject.FindObjectOfType<Game>().time : 0;
        str += t + "\n\n";
        intotext.text = str;

        str = GameObject.FindObjectOfType<Game>().state == 0 ? "Next wave in: " + GameObject.FindObjectOfType<Game>().time : "Wave is underway!";
        wavetext.text = str;
    }
}
*/