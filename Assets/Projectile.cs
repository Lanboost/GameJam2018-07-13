using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector3 direction = new Vector3();
    public float timeAlive = 3f;
    public float damage = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += direction;
        timeAlive -= Time.deltaTime;
        if(timeAlive <0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        var en = other.gameObject.GetComponent<Enemy>();

        if(en != null)
        {
            en.applyDamage(this.damage);
        }

        Destroy(this.gameObject);
        
    }
}
