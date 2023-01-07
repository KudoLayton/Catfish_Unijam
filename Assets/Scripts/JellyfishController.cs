using System.Collections;
using UnityEngine;


public class JellyfishController : MonoBehaviour
{
    [SerializeField] private float delay;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        var fishBehavior = other.gameObject.GetComponent<FishBehavior>();
        if (!fishBehavior) return;
        GetComponent<Collider>().enabled = false;
        fishBehavior.EnterJellyfish();
        StartCoroutine(WaitAndRelease(fishBehavior));
    }

    private IEnumerator WaitAndRelease(FishBehavior fishBehavior)
    {
        yield return new WaitForSeconds(delay);
        fishBehavior.LeaveJellyfish();
    }

}