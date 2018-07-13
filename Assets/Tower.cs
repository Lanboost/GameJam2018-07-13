using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public float cd = 1f;

    public float range = 1f;
    private float last = 0;


    public Projectile projectile;

	// Use this for initialization
	void Start () {
		
	}

    void applyCd()
    {
        last = cd;
    }

    void attack(Enemy enemy)
    {
        Projectile proj = Instantiate(projectile);
        proj.gameObject.transform.position = new Vector3()+(this.transform.position);

        var dir = enemy.transform.position - proj.gameObject.transform.position;
        dir.Normalize();

        proj.direction = dir;

    }

    void tryAttack()
    {
        var objs = GameObject.FindObjectsOfType<Enemy>();

        for(var i=0; i<objs.Length; i++)
        {
            var dist = (objs[i].transform.position - this.transform.position).magnitude;

            if (dist < range)
            {
                attack(objs[i]);
                applyCd();
                break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(last <= 0)
        {
            tryAttack();
        }
        else
        {
            last -= Time.deltaTime;
        }


	}
}
