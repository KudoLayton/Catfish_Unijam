using System.Collections;
using UnityEngine;

public class MarimoController : MonoBehaviour
{
    [SerializeField] private MarimoGenerator generator;
    private void OnTriggerEnter(Collider other)
    {
        var fishBehavior = other.GetComponent<FishBehavior>();
        if (fishBehavior)
        {
            GetComponent<Collider>().enabled = false;
            other.enabled = false;
            fishBehavior.EatMarimo();
            generator.SpawnMarimo();
            gameObject.SetActive(false);
        }
    }
}
