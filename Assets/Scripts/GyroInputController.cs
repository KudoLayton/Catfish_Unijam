using UnityEngine;
using UnityEngine.UIElements;

public class GyroInputController : MonoBehaviour
{
    private float _angle;
    private float _prevAngle;
    [SerializeField] private float minimumTilt;

    void Start()
    {
#if !UNITY_EDITOR
        Input.gyro.enabled = true;
#endif
    }

    void Update()
    {
        UpdateAngle();
        var angleDiff = _angle - _prevAngle;
        if (angleDiff > 180) angleDiff -= 360;
        if (angleDiff < -180) angleDiff += 360;
        transform.Rotate(Vector3.right, angleDiff);
        Camera.main.transform.Rotate(Vector3.back, angleDiff * 0.5f);
        _prevAngle = _angle;
    }

    private void UpdateAngle()
    {
#if UNITY_EDITOR
        var angleDelta = (Input.GetKey("d") ? 1 : 0) - (Input.GetKey("a") ? 1 : 0);
        _angle += angleDelta * Time.deltaTime * 100;
#else
        var gravity = Input.gyro.gravity;

        var direction = new Vector2(gravity.x, gravity.y);
        if (direction.magnitude > MinimumTilt)
        {
            _angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }
#endif
    }
}