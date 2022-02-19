using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerArea : MonoBehaviour
{
    [SerializeField] int id;

    private void OnTriggerEnter(Collider other)
    {
        GameEvents.gameEvents.DoorEnter(id);
    }

    /*private void OnTriggerExit(Collider other)
    {
        GameEvents.gameEvents.DoorExit(id);
    }*/
}
