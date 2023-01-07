using UnityEngine;

public class GyroPlaneController : MonoBehaviour
{
    private float _angle;
    private float _prevAngle;
    private Vector3 _faceDirection;

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
        _faceDirection.x += horizontal * Time.deltaTime;
        _faceDirection.z += vertical * Time.deltaTime;
        _faceDirection = Vector3.ClampMagnitude(_faceDirection, 1);
        _faceDirection.y = Mathf.Sqrt(1 - _faceDirection.x * _faceDirection.x - _faceDirection.z * _faceDirection.z);
#else
        _faceDirection = new Vector3(Input.gyro.gravity.x, -Input.gyro.gravity.z, Input.gyro.gravity.y);
#endif
        if (_faceDirection.y < 0) return;
        var planeDirection = new Vector3(_faceDirection.x, _faceDirection.y, 0).normalized;
        planeDirection.z = -0.3f;
        planeDirection = planeDirection.normalized;
        var planeRotation = Quaternion.FromToRotation(Vector3.up, planeDirection);
        transform.rotation = planeRotation;
    }
}