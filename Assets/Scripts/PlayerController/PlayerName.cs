using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerName : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player_name;

    void Start()
    {
        player_name.text= PlayerPrefs.GetString("input");
    }

}
