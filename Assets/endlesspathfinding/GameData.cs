using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    public int score = 0;
    public float music = 1;
    public int bestscore = 0;

    public static bool created = false;

    // Use this for initialization
    void Start () {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
