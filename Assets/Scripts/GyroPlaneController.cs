using UnityEngine;

public class GyroPlaneController : MonoBehaviour
{
    private float _angle;
    private float _prevAngle;
    private Vector3 _gravity;

    void Start()
    {
#if !UNITY_EDITOR
        Input.gyro.enabled = true;
#endif
    }

    void Update()
    {
        UpdateAngle();
    }

    private void UpdateAngle()
    {
#if UNITY_EDITOR
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _gravity.x += horizontal * Time.deltaTime;
        _gravity.z += vertical * Time.deltaTime;
        _gravity = Vector3.ClampMagnitude(_gravity, 1);
        _gravity.y = Mathf.Sqrt(1 - _gravity.x * _gravity.x - _gravity.z * _gravity.z);
#else
        _gravity = new Vector3(Input.gyro.gravity.x, -Input.gyro.gravity.z, Input.gyro.gravity.y);
#endif
        var planeRotation = Quaternion.FromToRotation(Vector3.up, _gravity);
        transform.rotation = planeRotation;
    }
}