using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounceSteering : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
    }

    void getSteering()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
    }
}
