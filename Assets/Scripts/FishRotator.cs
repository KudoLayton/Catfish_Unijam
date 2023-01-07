using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRotator : MonoBehaviour
{
    private Vector3 InitialForward = Vector3.forward;
    private Rigidbody rigidbody;
    [SerializeField] private GameObject plane;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Assert(plane);
        Vector3 planeNormal = plane.transform.localRotation * Vector3.up;
        Vector3 lookFor = Vector3.ProjectOnPlane(rigidbody.velocity, planeNormal);
        lookFor = Vector3.Normalize(lookFor);
        transform.rotation = Quaternion.LookRotation(lookFor, planeNormal);
    }
}
