using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

    private float time;
    public float lifetime;

	// Use this for initialization
	void Start () {
        lifetime = 5.0f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (time >= lifetime)
            Destroy(gameObject);
        else
            time += Time.fixedDeltaTime;
    }
}
