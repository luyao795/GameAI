using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringNamespace
{
    public class BehaviourList
    {
        public enum BehaviorAction
        {
            Wander = 0,
            Bounce = 1,
            Avoidance = 2,
        }

        public enum BehaviorType
        {
            Kinematic = 0,
            Dynamic = 1,
        }
    }
}
