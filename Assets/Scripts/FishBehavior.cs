using System;
using UnityEngine;


public class FishBehavior : MonoBehaviour
{
    public string color;
    private bool _isExiting;
    [SerializeField] private float exitSpeed;
    [SerializeField] private float exitDistance;

    public void SetColor(string color)
    {
        this.color = color;
    }

    private void Start()
    {
        _isExiting = false;
    }

    private void Update()
    {
        if (_isExiting && Mathf.Abs(transform.position.x) > exitDistance)
            gameObject.SetActive(false);
    }

    public void EnterJellyfish()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void LeaveJellyfish()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void EatMarimo()
    {
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.useGravity = false;
        GetComponent<Collider>().enabled = false;
        if (transform.position.x < 0)
            rigidBody.velocity = new Vector3(-exitSpeed, 0, 0);
        else
            rigidBody.velocity = new Vector3(exitSpeed, 0, 0);
        _isExiting = true;
    }
}