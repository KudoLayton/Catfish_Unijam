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

    void Update()
    {
        _fishBehavior.Move(new Vector3(_joystickController.Horizontal, _joystickController.Vertical));
        // TODO: Add Jump

#if UNITY_EDITOR
        var horizontal = (Input.GetKey("right") ? 1 : 0) + (Input.GetKey("left") ? -1 : 0);
        var vertical = (Input.GetKey("up") ? 1 : 0) + (Input.GetKey("down") ? -1 : 0);
        var moveDirection = new Vector3(horizontal, vertical).normalized;

        var jump = Input.GetKey("space");

        _fishBehavior.Move(moveDirection);
        if (jump) _fishBehavior.Jump();
#endif
    }
}