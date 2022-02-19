using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    //Özelliklerimi çekiyorum.
    public Sounds[] sounds;

    public static AudioManagement audioM;

    void Awake()
    {
        if (audioM == null)
        {
            audioM = this;
        }

        //DontDestroyOnLoad(gameObject);

        foreach(Sounds s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.audioClip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.is_Loop;
        }
    }



    void Start()
    {
        //Arka plan müziği ekleyebilirsin.
    }

    //Çalıştıracağımız ses efektinin ismi ile audio ismi aynı olmalı.
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.sound_name == name);

        if (s == null)
        {
            Debug.Log("Bu isimli ses bulunmamaktadır.");
            return;
        }

        s.audioSource.Play();
    }

}
