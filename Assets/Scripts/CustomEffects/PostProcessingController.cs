using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    public Volume ppVolume;

    private Darken darken;

    private ConcentrationLine ConcentrationLine;

    public float darkenAmount = 0f;

    public bool concentrationEnabled = true;
    
    // Start is called before the first frame update
    void Start()
    {
        ppVolume = GetComponent<Volume>();
        Darken tmp;
        ConcentrationLine tmp2;
        if (ppVolume.profile.TryGet<Darken>(out tmp))
        {
            darken = tmp;
        }

        if (ppVolume.profile.TryGet<ConcentrationLine>(out tmp2))
        {
            ConcentrationLine = tmp2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        darken.amount.value = darkenAmount;
        ConcentrationLine.IsEnable.value = concentrationEnabled;
    }
}
