using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringNamespace
{
    public class BehaviourController : MonoBehaviour
    {
        private BehaviourList.BehaviorAction action;
        public BehaviourList.BehaviorType type;
        public DynoSteering dyno;
        private WanderSteering wander;
        private Kinematic char_kinematic;
        private bool isRayHit;

        // Use this for initialization
        void Start()
        {
            action = BehaviourList.BehaviorAction.Wander;
            dyno = new DynoSteering();
            wander = GetComponent<WanderSteering>();
            char_kinematic = GetComponent<Kinematic>();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(action);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 position = transform.position;
            Vector3 rayPosition = position + new Vector3(0, 0, 0.9f);
            RaycastHit hit;
            isRayHit = Physics.Raycast(rayPosition, forward, out hit);
            if (!isRayHit)
            {
                action = BehaviourList.BehaviorAction.Wander;
            }
            else
            {
                Vector3 targetForward = hit.transform.TransformDirection(Vector3.forward);
                Vector3 targetPosition = hit.transform.position;
                if (hit.distance <= 0.5)
                {
                    action = BehaviourList.BehaviorAction.Bounce;
                }
                else
                {
                    action = BehaviourList.BehaviorAction.Wander;
                }
            }

            if (action.Equals(BehaviourList.BehaviorAction.Wander))
            {
                //Debug.Log("Wandering");
                dyno.force = wander.getSteering();
                Vector2 rot = new Vector2(char_kinematic.getVelocity().x, char_kinematic.getVelocity().z);
                transform.rotation = wander.getRotation(rot);
            }
            else
            {

            }
        }
    }
}
