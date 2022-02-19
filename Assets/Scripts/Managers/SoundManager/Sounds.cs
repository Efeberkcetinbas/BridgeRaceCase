using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    [HideInInspector]
    public AudioSource audioSource;

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.5f, 3f)]
    public float pitch;

    public string sound_name;

    public bool is_Loop;

}
