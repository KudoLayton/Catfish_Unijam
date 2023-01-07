using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public string color;

    public void SetColor(string color)
    {
        this.color = color;
    }
    public void EnterJellyfish()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void LeaveJellyfish()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}