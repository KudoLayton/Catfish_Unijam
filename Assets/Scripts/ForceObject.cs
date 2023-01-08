using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObject : MonoBehaviour
{
    [SerializeField] public float forceEff = 1.0f;
    [SerializeField] public Vector3 force = new Vector3(1.0f, 0.0f, 0.0f);
    [SerializeField] private float coolTime = 0f;
    [SerializeField] private float ActiveTime = 0f;
    [SerializeField] private float initialDelayTime = 0f;
    private bool _isBlowing = false;
    public bool isBlowing {
        get
        {
            return _isBlowing;
        }
    }
    private ParticleSystem particle;

    public void Start()
    {
        particle = GetComponent<ParticleSystem>();
        if (coolTime > 0)
        {
            StartCoroutine(coolTimeCycle());
        }
        else
        {
            _isBlowing = true;
            particle.Play();
        }
    }

    public IEnumerator coolTimeCycle()
    {
        yield return new WaitForSeconds(initialDelayTime);
        while (true)
        {
            particle.Play();
            _isBlowing = true;
            yield return new WaitForSeconds(ActiveTime);
            particle.Stop();
            _isBlowing = false;
            yield return new WaitForSeconds(coolTime);
        }
    }
}
