using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoUIUpdate : MonoBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var game = GameObject.FindObjectOfType<EndlessGame>();
        var gamedata = GameObject.FindObjectOfType<GameData>();
        text.text = "Gold: " + game.gold;
        text.text += "\nScore: " + gamedata.score;
    }
}
