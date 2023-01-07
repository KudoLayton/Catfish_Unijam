using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObject : MonoBehaviour
{

    [SerializeField] float force = 1.0f;
    [SerializeField] GameObject fish;


    private float ForceCalc(float radius, float force)
    {
        return force * Mathf.Pow(radius, -2);
    }


    public Vector3 ForceField(Vector3 start, Vector3 end, Vector3 position)
    {  
        Vector3 normal = gameObject.transform.rotation * Vector3.up;
        Vector3 lVec = end - start;
        Vector3 norm = normal.normalized;
        Vector3 lProj = Vector3.ProjectOnPlane(lVec, norm);
        Vector3 xProj = Vector3.ProjectOnPlane(position - start, norm);
        Vector3 lNorm = lProj.normalized;
        Vector3 naiveDir = xProj - lNorm * Vector3.Dot(xProj, lNorm);

        Vector3 dir = naiveDir.normalized;
        float radius = naiveDir.magnitude;

        float determination = Vector3.Dot(position - start, lProj);
        float limits = lVec.magnitude;

        if (0 <= determination && determination <= limits) return dir * ForceCalc(radius, force);
    
        return Vector3.zero;
    }

    void FixedUpdate()
    {
        Vector3 fishVec = fish.transform.position;
        Transform here = gameObject.transform;
        for (int i = 0; i < here.childCount; i++)
        {
            Transform field = here.GetChild(i).transform;
            Vector3 startVec = field.Find("Start").position;
            Vector3 endVec = field.Find("End").position;
            fish.GetComponent<Rigidbody>().AddForce(ForceField(startVec, endVec, fishVec));
        }
    }

}
