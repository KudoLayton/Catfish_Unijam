using UnityEngine;


public class FishBehavior : MonoBehaviour
{
    public string color;
    public int slot;
    private bool _isExiting;
    private Vector3 _exitVelocity;
    private Vector3 _beforeExitVelocity;
    private float _exitTime;
    private Rigidbody _rigidbody;
    private GameObject _quitEffectObject;
    [SerializeField] private float exitSpeed;
    [SerializeField] private float exitDistance;
    [SerializeField] private float exitTurnTime;

    public void SetColor(string color)
    {
        this.color = color;
    }

    public void SetSlot(int slot)
    {
        this.slot = slot;
    }


    private void Start()
    {
        _isExiting = false;
        _rigidbody = GetComponent<Rigidbody>();
        _quitEffectObject = transform.GetChild(1).gameObject;
        _quitEffectObject.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (_isExiting)
        {
            var progress = (Time.time - _exitTime) / exitTurnTime;
            if (progress < 0.5f) _quitEffectObject.transform.localScale = Vector3.one * (progress * 2);
            else if (progress < 1) _quitEffectObject.transform.localScale = Vector3.one * (2 - progress * 2);
            else _quitEffectObject.transform.localScale = Vector3.zero;
            _rigidbody.velocity = progress > 1 ? _exitVelocity : Vector3.Lerp(_beforeExitVelocity, _exitVelocity, progress);
            if (Mathf.Abs(transform.position.x) > exitDistance)
                gameObject.SetActive(false);
        }
    }

    public void EnterJellyfish()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void LeaveJellyfish()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void EatMarimo()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = false;
        GetComponent<Collider>().enabled = false;
        if (transform.position.x < 0)
            _exitVelocity = new Vector3(-exitSpeed, 0, 0);
        else
            _exitVelocity = new Vector3(exitSpeed, 0, 0);
        _beforeExitVelocity = _rigidbody.velocity;
        _exitTime = Time.time;
        _isExiting = true;
    }

    void OnTriggerStay(Collider field)
    {
        GameObject fieldObject = field.transform.gameObject;
        Vector3 colliderPoint = fieldObject.GetComponent<BoxCollider>().center - new Vector3(fieldObject.GetComponent<BoxCollider>().size.x, 0, 0);
        float radius = (gameObject.transform.position - colliderPoint).magnitude;
        float f = fieldObject.GetComponent<ForceObject>().forceEff;
        Vector3 finalForce = fieldObject.GetComponent<ForceObject>().force * f / (radius * radius);
        gameObject.GetComponent<Rigidbody>().AddForce(finalForce);
    }
}