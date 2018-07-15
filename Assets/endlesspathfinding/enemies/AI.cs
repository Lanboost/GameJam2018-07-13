using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AI
{
    public GameObject go;
    public virtual void run(float time)
    {
    }

    protected void moveTowards(Vector3 pos, float time, float speed)
    {
        var dir = pos - go.transform.position;
        var len = dir.normalized * speed * time;

        if (dir.magnitude < len.magnitude || dir.magnitude < 0.1f)
        {
            go.transform.position = pos;
        }
        else
        {
            go.transform.position += len;
        }
    }

    protected bool isInRange(float range, Tower target)
    {
        return ((go.transform.position - target.transform.position).magnitude - (target.size / 2)) < range;
    }
}

class AIGoblin: AI
{
    float cd = 1;
    float currentcd = 0;
    float dmg = 1;

    float speed = 5;
    float range = 1;

    void attack(float time, Tower target)
    {
        currentcd -= time;
        if(currentcd <0)
        {
            //do damage

            target.doDamage(dmg);
            currentcd += cd;
        }

    }

    //we always follow a path, kappa, until we find the core
    public override void run(float time)
    {
        var target = GameObject.Find("Core").GetComponent<Tower>();
        if (isInRange(range, target))
        {
            //attack
            attack(time, target);
        }
        else
        {

            var game = GameObject.FindObjectOfType<EndlessGame>();
            var x = (int)go.transform.position.x;
            var z = (int)go.transform.position.z;
            var dir = game.map.getPath(x - 200, z - 200);

            if (dir != -1)
            {
                var moveto = new Vector3(x, 0, z);
                if (dir == 0)
                {
                    moveto += new Vector3(1, 0, 0);
                }
                else if (dir == 1)
                {
                    moveto += new Vector3(0, 0, 1);
                }
                else if (dir == 2)
                {
                    moveto += new Vector3(-1, 0, 0);
                }
                else if (dir == 3)
                {
                    moveto += new Vector3(0, 0, -1);
                }
                moveTowards(moveto, time, speed);
            }
            else
            {
                var moveto = new Vector3(x, 0, z);

                if (target.transform.position.x < moveto.x)
                {
                    moveto.x -= 1;
                }
                else if (target.transform.position.x > moveto.x)
                {
                    moveto.x += 1;
                }
                else if (target.transform.position.z < moveto.z)
                {
                    moveto.z -= 1;
                }
                else if (target.transform.position.z > moveto.z)
                {
                    moveto.z += 1;
                }

                if (game.map.canWalk((int ) moveto.x - 200, (int) moveto.z - 200))
                {
                    moveTowards(moveto, time, speed);
                }
                else
                {
                    attack(time, game.map.getTower((int)moveto.x - 200, (int)moveto.z - 200));
                }
            }
        }

    }


}