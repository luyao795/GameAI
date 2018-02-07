using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceGenerator : MonoBehaviour {

    public GameObject prefab;
    private float period;

	// Use this for initialization
	void Start () {
        period = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (period >= 0.1f)
            period = 0.0f;
        if (period == 0.0f)
            Instantiate(prefab, transform.position, Quaternion.identity);
        period += Time.fixedDeltaTime;
    }
}
