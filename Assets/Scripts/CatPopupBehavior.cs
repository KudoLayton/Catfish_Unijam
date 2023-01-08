using UnityEngine;

public class CatPopupBehavior : MonoBehaviour
{
    private float _startTime;
    private float _duration;

    private void Start()
    {
        _startTime = Time.time;
    }

    public void SetDuration(float duration)
    {
        _duration = duration;
    }

    void Update()
    {
        var progress = (Time.time - _startTime) / _duration;
        if (progress < 0.3f)
        {
            float parameter = progress / 0.3f;
            parameter *= parameter;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, parameter);
        }
        else if (progress < 0.7f)
        {
            transform.localScale = Vector3.one;
        } 
        else if (progress < 1)
        {
            // become transparent
            var color = GetComponent<Renderer>().material.color;
            color.a = 1 - (progress - 0.7f) / 0.3f;
            GetComponent<Renderer>().material.color = color;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
