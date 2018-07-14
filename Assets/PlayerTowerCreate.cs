using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerCreate : MonoBehaviour {

    public List<GameObject> towerprefabs;

    public GameObject building;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for(var i=0; i<towerprefabs.Count ; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1+i) && building == null)
            {
                building = Instantiate(towerprefabs[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(building != null)
            {
                Destroy(building.gameObject);
            }
            building = null;
        }

        if(building != null && Input.GetMouseButtonDown(0))
        {
            building.GetComponent<Tower>().fire = true;
            building = null;
        }


        if(building != null)
        {
            var dmin = 3;
            var dmax = 6;
            var cam = Camera.main.transform;
            bool didHit = false;
            building.transform.position = doRaycast(out didHit, cam.position, cam.forward)+new Vector3(0,0.5f);
            if(!didHit)
            {
                building.SetActive(false);
            }
            else
            {
                building.SetActive(true);
                var heading = (building.transform.position - this.transform.position);
                var dist = heading.magnitude;
                var dot = Vector3.Dot(heading, this.transform.forward);
                if (dist < dmin || dot <= 0)
                {
                    building.transform.position = doRaycast(out didHit, this.transform.position + cam.forward * dmin + Vector3.up * 3, Vector3.down) + new Vector3(0, 0.5f);
                }
                else if (dist > dmax)
                {
                    building.transform.position = doRaycast(out didHit, this.transform.position+cam.forward * dmax + Vector3.up*3, Vector3.down) + new Vector3(0, 0.5f);
                }
            }
        }
	}

    Vector3 doRaycast(out bool didHit, Vector3 start, Vector3 dir)
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 9;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(start, dir, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            didHit = true;
        }
        else
        {
            didHit = false;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }



        return hit.point;
    }
}
