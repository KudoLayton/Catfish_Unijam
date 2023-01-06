using UnityEngine;
using UnityEngine.UI;

public class ArrowBehavior : MonoBehaviour
{
    private Gyro _gyro;
    [SerializeField] private Text text;
    private void Start()
    {
        _gyro = GetComponent<Gyro>();
    }

    private void Update()
    {
        var angle = _gyro.GetAngle();
        transform.rotation = Quaternion.Euler(0, 0, angle);
        text.text = "Angle: " + angle;
    }
}