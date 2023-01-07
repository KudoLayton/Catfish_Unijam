using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.zibra.liquid.Manipulators
{
    public class TestBall : MonoBehaviour
    {
        public void Water(ForceInteractionData force)
        {
            Vector3 forceData = force.Force;
            if (forceData.magnitude > 0.01) {
                Debug.Log("Hi");
            } else {
                Debug.Log("Not Hi");
            }
        }
    }
}

