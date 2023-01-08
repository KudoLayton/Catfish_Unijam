using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundExcuter : MonoBehaviour
{
    [SerializeField] UnityEvent OnCollisionExitHandle;
    [SerializeField] UnityEvent OnTriggerEnterHandle;

    private void OnCollisionExit(Collision collision)
    {
        OnCollisionExitHandle.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterHandle.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
