using UnityEngine;
using UnityEngine.Events;


using com.zibra.liquid.Manipulators;
public class FishBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float waterDrag;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private GameObject objectParent;
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

    public void SetMovement(Vector3 normalizedVelocity, bool jumping)
    {
        if (IsInWater())
        {
            var velocity = normalizedVelocity == Vector3.zero ? _rigidbody.velocity : normalizedVelocity * moveSpeed;
            if (jumping) velocity.y = jumpSpeed;
            _rigidbody.velocity = velocity;
        }
    }

    private bool IsInWater()
    {
        bool temp = objectParent.GetComponent<ObjectForceInfo>().IsTouchWater();
        return temp;
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
        //Vector3 fixAxis = transform.rotation.eulerAngles;
        //fixAxis.y = 0;
        //fixAxis.z = 0;
        //transform.rotation = Quaternion.Euler(fixAxis);
    }
}