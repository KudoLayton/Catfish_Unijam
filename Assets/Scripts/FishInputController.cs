using UnityEngine;

public class FishInputController : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    private JoystickController _joystickController;
    private FishBehavior _fishBehavior;

    void Start()
    {
        _fishBehavior = GetComponent<FishBehavior>();
        _joystickController = joystick.GetComponent<JoystickController>();
    }

    void FixedUpdate()
    {
        var velocity = new Vector3(_joystickController.Horizontal, _joystickController.Vertical);
        var jump = false; // TODO

#if UNITY_EDITOR
        if (velocity == Vector3.zero)
        {
            velocity = new Vector3(
                (Input.GetKey("right") ? 1 : 0) + (Input.GetKey("left") ? -1 : 0),
                (Input.GetKey("up") ? 1 : 0) + (Input.GetKey("down") ? -1 : 0)
            );
        }

        jump = Input.GetKey("space");
#endif

        _fishBehavior.SetMovement(velocity, jump);
    }
}