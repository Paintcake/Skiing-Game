using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    public TimeSpan timePlaying;
    public bool raceStarted = false;
    public int time;

    public void OnEnable()
    {
        GameEvents.OnRaceStart += StartTimer;
        GameEvents.OnRaceStop += StopTimer;
    }

    public void OnDisable()
    {
        GameEvents.OnRaceStart -= StartTimer;
        GameEvents.OnRaceStop -= StopTimer;
    }

    public void StartTimer()
    {
        time = 0;
        StartCoroutine("Timer");
        raceStarted = true;
    }

    public void StopTimer()
    {
        if (raceStarted)
        {
            StopCoroutine("Timer");
        }
    }
}
