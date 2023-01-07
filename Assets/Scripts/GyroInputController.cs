using UnityEngine;

public class GyroInputController : MonoBehaviour
{
    private float _angle;
    [SerializeField] private float MinimumTilt;

    void Start()
    {
#if !UNITY_EDITOR
        Input.gyro.enabled = true;
#endif
    }

    void Update()
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
        Debug.Log(_angle);
    }

    public float GetAngle()
    {
        return _angle;
    }
}