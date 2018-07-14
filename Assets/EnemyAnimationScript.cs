using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationScript : MonoBehaviour {
    Animator anim;
    NavMeshAgent nav;
	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", nav.speed);
	}
}
