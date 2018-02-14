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
        private DynoAlign align;
        private WanderSteering wander;
        private Kinematic char_kinematic;
        private bool isRayHit;

        private Vector3 forward;
        private Vector3 position;
        private Vector3 targetForward;
        private Vector3 targetPosition;

        public bool BounceActivated = true;

        // Use this for initialization
        void Start()
        {
            action = BehaviourList.BehaviorAction.Wander;
            dyno = new DynoSteering();
            align = GetComponent<DynoAlign>();
            wander = GetComponent<WanderSteering>();
            char_kinematic = GetComponent<Kinematic>();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(action);
            forward = transform.TransformDirection(Vector3.forward);
            position = transform.position;
            Vector3 rayPosition = position + forward.normalized;
            RaycastHit hit;
            isRayHit = Physics.Raycast(rayPosition, forward, out hit);
            if (!isRayHit)
            {
                action = BehaviourList.BehaviorAction.Wander;
            }
            else
            {
                targetForward = hit.transform.TransformDirection(Vector3.forward);
                targetPosition = hit.transform.position;
                if (hit.distance <= 5f)
                {
                    Debug.Log(hit.transform.gameObject.name);
                    if (BounceActivated)
                        action = BehaviourList.BehaviorAction.Bounce;
                    else
                        action = BehaviourList.BehaviorAction.Avoidance;
                }
                else
                {
                    action = BehaviourList.BehaviorAction.Wander;
                }
            }

            if (action.Equals(BehaviourList.BehaviorAction.Wander))
            {
                StartWandering();
            }
            else
            {
                if (action.Equals(BehaviourList.BehaviorAction.Bounce))
                {
                    StartBouncing();
                    action = BehaviourList.BehaviorAction.Wander;
                }
                else
                {
                    StartAvoiding();
                    if (hit.distance > 1.5f)
                    {
                        Debug.Log("Back to wander");
                        action = BehaviourList.BehaviorAction.Wander;
                    }
                }
            }
        }

        void StartWandering()
        {
            dyno.force = wander.getSteering();
            if (type.Equals(BehaviourList.BehaviorType.Dynamic))
                dyno.torque = align.getSteering().torque;
            Vector2 rot = new Vector2(char_kinematic.getVelocity().x, char_kinematic.getVelocity().z);
            transform.rotation = wander.getRotation(rot);
        }

        void StartBouncing()
        {
            dyno.force = wander.getSteering();
            dyno.force.x *= -1;
            dyno.force.z *= -1;
            if (type.Equals(BehaviourList.BehaviorType.Dynamic))
                dyno.torque = align.getSteering().torque;
            char_kinematic.setVelocity(new Vector3(-char_kinematic.getVelocity().x, char_kinematic.getVelocity().y, -char_kinematic.getVelocity().z));
            Vector2 rot = new Vector2(char_kinematic.getVelocity().x, char_kinematic.getVelocity().z);
            transform.rotation = wander.getRotation(rot);
        }

        void StartAvoiding()
        {
            float rotationAmount;
            //dyno.force = Vector3.zero;
            //dyno.torque = 0f;
            //char_kinematic.setVelocity(Vector3.zero);
            float angle = Vector3.Dot(forward, targetForward);
            rotationAmount = angle >= 0 ? 0.1f : -0.1f;
            char_kinematic.setRotation(char_kinematic.getRotation() + rotationAmount);
            Debug.Log(char_kinematic.getRotation());
        }
    }
}
