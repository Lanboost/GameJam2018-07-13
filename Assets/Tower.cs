using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour {

    public float cd = 1f;

    public float range = 1f;
    private float last = 0;
    public bool fire = false;

    public float health = 10;
    public float maxhealth = 10;

    public Canvas healthBar;
    public Image healthImage;

    public float size = 1;

    public Projectile projectile;

    public float regencd = 1;
    public float regentick = 0;
    public float regenvalue = 1;

    void updateHealth()
    {
        healthImage.fillAmount = this.health / this.maxhealth;
    }

	// Use this for initialization
	void Start () {
        updateHealth();
        healthBar.gameObject.SetActive(false);
    }

    public virtual void customDestroy()
    {

    }

    public void doDamage(float dmg)
    {
        healthBar.gameObject.SetActive(true);
        this.health -= dmg;
        updateHealth();
        if (this.health < 0)
        {
            var game = GameObject.FindObjectOfType<EndlessGame>();
            game.map.destroy((int)this.transform.position.x - 200, (int)this.transform.position.z - 200);
            Destroy(this.gameObject);
            customDestroy();
        }
    }

    void applyCd()
    {
        last = cd;
    }

    void attack(Enemy enemy)
    {
        Projectile proj = Instantiate(projectile);
        proj.gameObject.transform.position = new Vector3()+(this.transform.position);

        var dir = (enemy.transform.position + new Vector3(0, 0.5f)) - proj.gameObject.transform.position;
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
        if (fire)
        {
            if (last <= 0)
            {
                tryAttack();
            }
            else
            {
                last -= Time.deltaTime;
            }
        }

        if(regencd > 0 && this.health < this.maxhealth)
        {
            this.regentick -= Time.deltaTime;
            if(this.regentick < 0)
            {
                this.regentick += regencd;
                this.health += regenvalue;
                if(this.health >= this.maxhealth)
                {
                    this.health = this.maxhealth;
                    this.healthBar.gameObject.SetActive(false);
                }
                updateHealth();
                
            }
        }

	}
}
