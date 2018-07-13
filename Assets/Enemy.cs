using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public float maxhealth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void applyDamage(float dmg)
    {
        this.health -= dmg;
        //Debug.Log(this.health);
        if(this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
