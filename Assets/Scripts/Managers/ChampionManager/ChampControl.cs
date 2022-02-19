using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampControl : MonoBehaviour
{
    void Start()
    {
        GameEvents.gameEvents.onChamp += Champ_Annonunce;
    }

    private void Champ_Annonunce()
    {
        //Debug.Log("Şampiyon Belirlendi");
        AudioManagement.audioM.Play("success");
        //Particle Effects!
    }

    void OnDestroy()
    {
        GameEvents.gameEvents.onChamp -= Champ_Annonunce;
    }
}
