using UnityEngine;

public class Gyro : MonoBehaviour
{
    private float _angle;
    [SerializeField] private const float MinimumTilt = 0.01f;
    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        var gravity = Input.gyro.gravity;

        var direction = new Vector2(gravity.x, gravity.y);
        if (direction.magnitude > MinimumTilt)
        {
            _angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }
    }

    public float GetAngle()
    {
        return _angle;
    }
}
