using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreTower : Tower {

    public override void customDestroy()
    {
        var gamedata = GameObject.FindObjectOfType<GameData>();
        if(gamedata.bestscore < gamedata.score)
        {
            gamedata.bestscore = gamedata.score;
        }


        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
