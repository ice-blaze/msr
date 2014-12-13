using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowManager : MonoBehaviour {

    List<Transform> checkpoints = new List<Transform>();
    GameObject arrow = null;

	// Use this for initialization
	void Start () {
        arrow = GameObject.Find("Arrow");


        GameObject checkpoint = GameObject.Find("CheckPoints");
        foreach(Transform g in checkpoint.transform)
        {
            checkpoints.Add(g);
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = checkpoints[0].transform.position;
        v.y = transform.position.y;
        arrow.transform.LookAt(v);
    }

    public void RemoveCheckPoint(Collider other)
    {
        if (checkpoints.Count>0 && other.transform.Equals(checkpoints[0]))
        {
            checkpoints.RemoveAt(0);
        }
    }
}
