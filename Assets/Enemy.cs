﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public float maxhealth;
    public Spawner owner;
    public float speed;

    private int currentCoord = 0;

	// Use this for initialization
	void Start () {
		
	}

    public void init(Spawner s)
    {
        this.owner = s;
        this.transform.position = new Vector3() + owner.pathCoords[0];
        currentCoord = 1;
    }
	
	// Update is called once per frame
	void Update () {
        var dir = owner.pathCoords[currentCoord] - this.transform.position;
        if(dir.magnitude < 0.2)
        {
            currentCoord++;
            if (owner.pathCoords.Count <= currentCoord)
            {
                Destroy(this.gameObject);
                //SHOULD LOSE LIFE
            }
        }
        dir.Normalize();
        this.transform.position += dir * speed;

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
