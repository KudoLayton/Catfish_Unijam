using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObject : MonoBehaviour
{
    [SerializeField] public float forceEff = 1.0f;
    [SerializeField] public Vector3 force = new Vector3(1.0f, 0.0f, 0.0f);
}
