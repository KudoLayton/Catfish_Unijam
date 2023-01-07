using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.zibra.liquid.Manipulators
{
    public class ObejctForceInfo : MonoBehaviour
    {

        Vector3 force = Vector3.zero;
        Vector3 torque = Vector3.zero;

        public void SetForceInfo(ForceInteractionData forceData)
        {
            force = forceData.Force;
            torque = forceData.Torque;
        }

        public bool IsTouchWater()
        {
            float size = force.magnitude + torque.magnitude;
            return (size < 0.1 ? false : true);
        }
    
    }

}

