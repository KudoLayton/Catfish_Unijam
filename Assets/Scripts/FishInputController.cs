using UnityEngine;

public class FishInputController : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private bool useJoystick;
    private JoystickController _joystickController;
    private FishBehavior _fishBehavior;
    void Start()
    {
        _fishBehavior = GetComponent<FishBehavior>();
        _joystickController = joystick.GetComponent<JoystickController>();
        Debug.Log("Joystick: " + useJoystick);
    }

    void Update()
    {
        if (useJoystick)
        {
            _fishBehavior.Move(new Vector3(_joystickController.Horizontal, _joystickController.Vertical));
        }
        else
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            _fishBehavior.Move(new Vector3(horizontal, vertical).normalized);
            if (Input.GetKey("space"))
            {
                _fishBehavior.Jump();
            }
        }
    }
}
