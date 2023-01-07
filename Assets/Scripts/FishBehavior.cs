using UnityEngine;
using UnityEngine.Events;

public class FishBehavior : MonoBehaviour
{
    [SerializeField] private float maxForce;
    [SerializeField] private float waterDrag;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;
    private Rigidbody _rigidbody;
    private const float EPSILON = 0.01f;
    private bool _prevInWater;

    public UnityEvent onEnterWater;
    public UnityEvent onLeaveWater;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        bool currInWater = IsInWater();

        if (_prevInWater && !currInWater) LeaveWater();
        else if (!_prevInWater && currInWater) EnterWater();
        
        Rotate();
        
        _prevInWater = currInWater;
    }
    
    public void Move(Vector3 direction)
    {
        if (IsInWater()) _rigidbody.AddForce(direction * maxForce);
    }

    public void Jump()
    {
        if (IsInWater())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce);
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
        }
    }

    private bool IsInWater()
    {
        // TODO
        return transform.position.y < 0;
    }

    private void LeaveWater()
    {
        _rigidbody.useGravity = true;
        _rigidbody.drag = 0;
        onLeaveWater.Invoke();
    }

    private void EnterWater()
    {
        _rigidbody.useGravity = false;
        _rigidbody.drag = waterDrag;
        onEnterWater.Invoke();
    }

    private void Rotate()
    {
        var velocity = _rigidbody.velocity;
        if (velocity.sqrMagnitude >= EPSILON)
            transform.rotation = Quaternion.LookRotation(velocity);
        
    }
}