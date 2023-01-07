using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COMConfig : MonoBehaviour
{
    [SerializeField] private Vector3 CenterOfMass;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = CenterOfMass;
    }
}
