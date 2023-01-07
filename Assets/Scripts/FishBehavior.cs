using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public string color;
    public int slot;

    public void SetColor(string color)
    {
        this.color = color;
    }

    public void SetSlot(int slot)
    {
        this.slot = slot;
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