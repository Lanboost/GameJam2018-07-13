using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuupdater : MonoBehaviour {
    public Button startbutton;
    public Slider music;

    public Text score;
    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.None;
        startbutton.onClick.AddListener(delegate {
            SceneManager.LoadScene("Endless", LoadSceneMode.Single);
        });

        music.onValueChanged.AddListener(delegate
        {
            GameObject.FindObjectOfType<GameData>().music = music.value;
        });


        if(!GameData.created)
        {
            GameObject o = new GameObject("GameData");
            o.AddComponent<GameData>();
        }

    }
	
	// Update is called once per frame
	void Update () {

        var data = GameObject.FindObjectOfType<GameData>();
        score.text = "Best score: " + data.bestscore + "\nLast score: " + data.score;



    }
}
