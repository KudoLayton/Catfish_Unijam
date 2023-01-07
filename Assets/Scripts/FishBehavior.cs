using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private float maxForce;
    private JoystickController _joystickController;
    private Rigidbody _rigidbody;
    private const float EPSILON = 0.01f;

    void Start()
    {
        _joystickController = joystick.GetComponent<JoystickController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsInWater())
            MoveInWater();
        else
            MoveInAir();
    }

    private bool IsInWater()
    {
        // TODO
        return true;
    }

    private void MoveInWater()
    {
        _rigidbody.useGravity = false;
        Vector3 force = new Vector3(_joystickController.Horizontal, _joystickController.Vertical, 0) * maxForce;
        _rigidbody.AddForce(force);
        var velocity = _rigidbody.velocity;
        if (velocity.magnitude < EPSILON)
            transform.rotation = Quaternion.LookRotation(velocity);
    }

    private void MoveInAir()
    {
        _rigidbody.useGravity = true;
    }
}