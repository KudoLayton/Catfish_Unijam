using System.Collections;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{

    public void EnterJellyfish()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void LeaveJellyfish()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}