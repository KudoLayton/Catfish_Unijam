using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    [SerializeField] private float maxForce;
    [SerializeField] private float waterDrag;
    [SerializeField] private float jumpForce;
    private Rigidbody _rigidbody;
    private const float EPSILON = 0.01f;
    private bool _prevInWater;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool currInWater = IsInWater();

        if (_prevInWater && !currInWater) StartInAir();
        else if (!_prevInWater && currInWater) StartInWater();
        
        Rotate();
        
        _prevInWater = currInWater;
    }
    
    public void Move(Vector3 direction)
    {
        if (IsInWater()) _rigidbody.AddForce(direction * maxForce);
    }

    public void Jump()
    {
        _rigidbody.AddForce(0, jumpForce, 0);
    }

    private bool IsInWater()
    {
        // TODO
        return transform.position.y < 0;
    }

    private void StartInAir()
    {
        _rigidbody.useGravity = true;
        _rigidbody.drag = 0;
    }

    private void StartInWater()
    {
        _rigidbody.useGravity = false;
        _rigidbody.drag = waterDrag;
    }

    private void Rotate()
    {
        var velocity = _rigidbody.velocity;
        if (velocity.sqrMagnitude >= EPSILON)
            transform.rotation = Quaternion.LookRotation(velocity);
        
    }
}