using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{

    private long tick;
    private bool timerSwitch = false;



    public void timerStart()
    {
        timerSwitch = true;
    }

    public long timerStop()
    {
        timerSwitch = false;
        return tick;
    }

    void FixedUpdate()
    {
        if (timerSwitch)
        {
            tick += 1;
        }
    }
}
