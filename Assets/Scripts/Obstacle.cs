using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool removeOnCollision = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPlayerCollision(collision);
        }
    }

    protected virtual void OnPlayerCollision(Collision collision)
    {
        Debug.Log("Player collided with obstacle: " + gameObject.name);
        PlayerCollisionEvent.TriggerPlayerCollision();

        if (removeOnCollision)
        {
            RemoveObstacle();
        }
    }

    private void RemoveObstacle()
    {
        Debug.Log("Removing obstacle: " + gameObject.name);
        Destroy(gameObject);
    }
}