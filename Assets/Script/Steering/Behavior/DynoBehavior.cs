using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringNamespace
{
    public class DynoBehavior : MonoBehaviour
    {
        private Kinematic char_RigidBody;

        private KinematicSteeringOutput kso;
        private DynoArrive arrive;
        private DynoAlign align;

        private DynoSteering ds_force;
        private DynoSteering ds_torque;

        private BehaviourController controller;

        // Use this for initialization
        void Start()
        {
            char_RigidBody = GetComponent<Kinematic>();
            arrive = GetComponent<DynoArrive>();
            align = GetComponent<DynoAlign>();
            controller = GetComponent<BehaviourController>();
            controller.type = BehaviourList.BehaviorType.Dynamic;
        }

        // Update is called once per frame
        void Update()
        {
            // Decide on behavior
            //ds_force = arrive.getSteering();
            //ds_torque = align.getSteering();

            //controller.dyno.torque = ds_torque.torque;

            // Update Kinematic Steering
            kso = char_RigidBody.updateSteering(controller.dyno, Time.deltaTime);

            transform.position = new Vector3(kso.position.x, transform.position.y, kso.position.z);
        }
    }
}