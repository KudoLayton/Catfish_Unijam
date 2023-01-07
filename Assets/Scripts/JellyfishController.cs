using System.Collections;
using UnityEngine;


public class JellyfishController : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float coolTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        var fishBehavior = other.gameObject.GetComponent<FishBehavior>();
        if (fishBehavior)
        {
            GetComponent<Collider>().enabled = false;
            fishBehavior.EnterJellyfish();
            yield return new WaitForSeconds(delay);
            fishBehavior.LeaveJellyfish();
            yield return new WaitForSeconds(coolTime);
            GetComponent<Collider>().enabled = true;
        }
    }

}