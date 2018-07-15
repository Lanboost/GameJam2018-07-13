using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderToTexture : MonoBehaviour {
    public TowerBuildList data;
    public Camera cam; 
	// Use this for initialization
	void Start () {
        for (var i = 0; i < data.items.Count; i++)
        {
            var o = Instantiate(data.items[i].prefab);
            o.transform.position = new Vector3();
            o.GetComponent<Tower>().healthBar.gameObject.SetActive(false);
            var rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
            rt.Create();
            data.items[i].rt = rt;
            //

            cam.targetTexture = rt;
            cam.Render();
            DestroyImmediate(o);
        }
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
