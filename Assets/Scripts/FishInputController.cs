using System.Collections;
using System.Collections.Generic;
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
    }
}
