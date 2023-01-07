using UnityEngine;

public class MarimoController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var fishBehavior = other.GetComponent<FishBehavior>();
        if (fishBehavior)
        {
            Debug.Log("Fish entered marimo: " + fishBehavior.name);
            GetComponent<Collider>().enabled = false;
            other.enabled = false;
            fishBehavior.EatMarimo();
            gameObject.SetActive(false);
        }
    }
}
