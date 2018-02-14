using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
namespace SteeringNamespace
{

    public class FileWriter : MonoBehaviour
    {

        private string fileName = "C:\\Users\\u1069212\\Desktop\\Output.txt";
        private StreamWriter sw;
        private Kinematic k;
        private float time;

        // Use this for initialization
        void Start()
        {
            k = GetComponent<Kinematic>();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                Debug.Log(fileName + " already exists. The old file has been deleted.");
            }
            sw = File.CreateText(fileName);
            time = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            if (sw != null)
            {
                if (time <= 10.0f)
                {
                    Vector3 realVel = k.getVelocity();
                    realVel.y = 0.0f;
                    sw.WriteLine("{0}\t{1}", time, realVel.magnitude);
                    time += Time.fixedDeltaTime;
                }
                else
                {
                    //Debug.Log("Write complete.");
                    sw.Close();
                }
            }
        }
    }
}
