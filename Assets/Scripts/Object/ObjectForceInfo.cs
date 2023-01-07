using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.zibra.liquid.Manipulators
{
    public class ObjectForceInfo : MonoBehaviour
    {

        Vector3 force = Vector3.zero;
        Vector3 torque = Vector3.zero;

        public void SetForceInfo(ForceInteractionData forceData)
        {
            force = forceData.Force;
            torque = forceData.Torque;
            float size = force.magnitude + torque.magnitude;
            Debug.Log(size);
        }

        public bool IsTouchWater()
        {
            float size = force.magnitude + torque.magnitude;
            return (size < 0.001 ? false : true);
        }
    
    }

}

