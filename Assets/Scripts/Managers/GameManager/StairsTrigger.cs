using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    [SerializeField] private int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEvents.gameEvents.is_blue = true;
            GameEvents.gameEvents.BrickPositionChanged(id);
        }

        else
        {
            GameEvents.gameEvents.is_red = true;
            GameEvents.gameEvents.is_green = true;
            GameEvents.gameEvents.BrickPositionChanged(id);
        }
    }
}
