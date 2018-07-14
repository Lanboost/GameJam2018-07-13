using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationScript : MonoBehaviour {
    Animator anim;
    Enemy enemy;
	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        enemy = GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {        
        anim.SetFloat("Speed", enemy.speed);
	}
}
