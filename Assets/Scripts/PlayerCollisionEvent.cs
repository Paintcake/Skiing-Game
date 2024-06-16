using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollisionEvent : MonoBehaviour
{
    public static event Action OnPlayerCollision;

    public static void TriggerPlayerCollision()
    {
        OnPlayerCollision?.Invoke();
    }
}

