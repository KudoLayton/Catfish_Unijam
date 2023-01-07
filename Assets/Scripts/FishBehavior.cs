using System;
using UnityEngine;


public class FishBehavior : MonoBehaviour
{
    public string color;
    private bool _isExiting;
    private Vector3 _exitVelocity;
    private Vector3 _beforeExitVelocity;
    private float _exitTime;
    private Rigidbody _rigidbody;
    [SerializeField] private float exitSpeed;
    [SerializeField] private float exitDistance;
    [SerializeField] private float exitTurnTime;

    public void SetColor(string color)
    {
        this.color = color;
    }

    private void Start()
    {
        _isExiting = false;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isExiting)
        {
            var progress = (Time.time - _exitTime) / exitTurnTime;
            _rigidbody.velocity = progress > 1 ? _exitVelocity : Vector3.Lerp(_beforeExitVelocity, _exitVelocity, progress);
            if (Mathf.Abs(transform.position.x) > exitDistance)
                gameObject.SetActive(false);
        }
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
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = false;
        GetComponent<Collider>().enabled = false;
        if (transform.position.x < 0)
            _exitVelocity = new Vector3(-exitSpeed, 0, 0);
        else
            _exitVelocity = new Vector3(exitSpeed, 0, 0);
        _beforeExitVelocity = _rigidbody.velocity;
        _exitTime = Time.time;
        _isExiting = true;
    }
}