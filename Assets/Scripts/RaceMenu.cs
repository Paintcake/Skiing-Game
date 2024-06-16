using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMenu : MonoBehaviour
{
    public GameObject popup;
    private void OnEnable()
    {
        GameEvents.OnRaceStop += ShowPopup;
    }


    void ShowPopup()
    {
        popup.SetActive(true);
    }
}
