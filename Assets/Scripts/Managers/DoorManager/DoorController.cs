using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour,IDoTweenControl
{
    [SerializeField] private int id;


    //6.8 y old_y 4.8
    //Kapı açılma efektini DoTween ile yapalım.
    public void DoDoTween(float y, float duration)
    {
        transform.DOMoveY(y, duration);
    }

    void Start()
    {
        GameEvents.gameEvents.onDoorTrigger += OnDoorOpen;
        GameEvents.gameEvents.offDoorTrigger += OnDoorClose;

    }

    private void OnDoorOpen(int id)
    {
        if (id == this.id)
        {
            DoDoTween(11.8f, .5f);
            //Ses efekti olabilir.
        }
    }

    private void OnDoorClose(int id)
    {
        if (id == this.id)
        {
            DoDoTween(6.54f, .5f);
        }
    }

    private void OnDestroy()
    {
        GameEvents.gameEvents.onDoorTrigger -= OnDoorOpen;
        GameEvents.gameEvents.offDoorTrigger -= OnDoorClose;

    }


}
