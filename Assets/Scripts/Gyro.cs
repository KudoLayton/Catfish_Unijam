/*
 * 자이로센서로부터 "아래" 방향을 구하는 컴포넌트
 * 아무데나 달아놓고, 필요할 때마다 GetAngle() 함수를 호출하여 아래 방향을 얻는다
 */
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

    /**
     * 기기의 자이로센서와 동기화된 "아래" 방향을 구한다
     * @return 아래 방향의 각도
     */
    public float GetAngle()
    {
        return _angle;
    }
}
