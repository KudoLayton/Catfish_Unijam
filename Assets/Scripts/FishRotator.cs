using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRotator : MonoBehaviour
{
    private Vector3 InitialForward = Vector3.forward;
    private Rigidbody _rigidbody;
    private GameObject plane;
    void Start()
    {
        _rigidbody = transform.parent.GetComponent<Rigidbody>();
        plane = GameObject.FindWithTag("Plane");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Assert(plane);
        Vector3 planeNormal = plane.transform.localRotation * Vector3.up;
        Vector3 lookFor = Vector3.ProjectOnPlane(_rigidbody.velocity, planeNormal);
        lookFor = Vector3.Normalize(lookFor);
        transform.rotation = Quaternion.LookRotation(lookFor, planeNormal);
    }
}
