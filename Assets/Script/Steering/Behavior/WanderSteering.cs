using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderSteering : MonoBehaviour {

    public float wanderOffset = 1.5f;
    public float wanderRadius = 4.0f;
    public float wanderRate = 0.4f;
    public float wanderOrientation = 0.0f;
    public float turnSpeed = 20.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 getSteering()
    {
        float objOrientation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        wanderOrientation += randomBinomial() * wanderRate;
        float targetOrientation = wanderOrientation + objOrientation;
        Vector3 targetPosition = transform.position + (orientationToVector(objOrientation) * wanderOffset);
        targetPosition = targetPosition + (orientationToVector(targetOrientation) * wanderRadius);
        float maxAcceleration = 10.0f;


        Vector3 acceleration = targetPosition - transform.position;
        Debug.Log(acceleration);

        acceleration.y = 0.0f;
        acceleration.Normalize();
        acceleration *= maxAcceleration;
        return acceleration;
    }

    public Quaternion getRotation(Vector2 direction)
    {
        direction.Normalize();

        Quaternion outRotation;
        // If we have a non-zero direction then look towards that direciton otherwise do nothing
        if (direction.sqrMagnitude > 0.001f)
        {
            float toRotation = (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.y, toRotation, Time.deltaTime * turnSpeed);

            outRotation = Quaternion.Euler(0, rotation, 0);
        }
        else
            outRotation = Quaternion.identity;
        return outRotation;
    }

    /* Returns a random number between -1 and 1. Values around zero are more likely. */
    float randomBinomial()
    {
        return Random.value - Random.value;
    }

    /* Returns the orientation as a unit vector */
    Vector3 orientationToVector(float orientation)
    {
        return new Vector3(Mathf.Cos(orientation), 0, Mathf.Sin(orientation));
    }
}
